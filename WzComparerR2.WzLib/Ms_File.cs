using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using CSChaCha20;
using WzComparerR2.WzLib.Cryptography;

namespace WzComparerR2.WzLib
{
    public class Ms_File : IMapleStoryFile, IDisposable
    {
        public Ms_File(string fileName, Wz_Structure wzs)
        {
            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            this.Init(fileStream, fileName, wzs, false);
        }

        public Ms_File(Stream baseStream, string originalFileName, Wz_Structure wzs, bool leaveOpen = false)
        {
            this.Init(baseStream, originalFileName, wzs, leaveOpen);
        }

        private void Init(Stream baseStream, string originalFileName, Wz_Structure wzs, bool leaveOpen)
        {
            if (baseStream == null)
            {
                throw new ArgumentNullException(nameof(baseStream));
            }
            if (originalFileName == null)
            {
                throw new ArgumentNullException(nameof(originalFileName));
            }

            this.BaseStream = baseStream;
            this.WzStructure = wzs;
            this.leaveOpen = leaveOpen;
            this.ReadHeader(originalFileName);
            this.Entries = new List<Ms_Entry>(0);
        }

        public Stream BaseStream { get; private set; }
        public Wz_Structure WzStructure { get; private set; }
        public Ms_Header Header { get; private set; }
        public List<Ms_Entry> Entries { get; private set; }

        Stream IMapleStoryFile.FileStream => this.BaseStream;
        object IMapleStoryFile.ReadLock => this.BaseStream;

        private bool leaveOpen;

        private void ReadHeader(string fullFileName)
        {
            string fileName = Path.GetFileName(fullFileName).ToLower();
            this.BaseStream.Position = 0;
            using var bReader = new BinaryReader(this.BaseStream, Encoding.ASCII, true);

            // 1. random bytes
            int randByteCount = fileName.Sum(c => (int)c) % 312 + 30;
            byte[] randBytes = bReader.ReadBytes(randByteCount);
            byte[] testRandBytes = randBytes;
            for (int i = 0; i < testRandBytes.Length; ++i)
            {
                testRandBytes[i] = (byte)((sbyte)testRandBytes[i] >> 1);
            }

            var pack_type = bReader.ReadByte() ^ testRandBytes[0];

            if (pack_type != 4)
            {
                return;
            }

            // 2. encrypted snowKeySalt
            int hashedSaltLen = bReader.ReadInt32();
            int saltLen = (byte)hashedSaltLen ^ randBytes[0];
            byte[] saltBytes = bReader.ReadBytes(saltLen * 2);
            char[] saltChars = new char[saltLen];
            char[] saltChars2 = new char[saltLen];
            for (int i = 0; i < saltLen; i++)
            {
                saltChars[i] = (char)(randBytes[i] ^ saltBytes[i * 2]);
                int a = randBytes[i] ^ saltBytes[i * 2];
                int b = a | 0x4B;
                b = b << 1;
                b = b - a;
                b = b - 75;
                saltChars2[i] = (char)b;
            }
            string saltStr = new string(saltChars);
            string saltStr2 = new string(saltChars2);

            byte[] chacha20key_xor = { 0x7B, 0x2F, 0x35, 0x48, 0x43, 0x95, 0x02, 0xB9, 0xAE, 0x91, 0xA6, 0xE1, 0xD8, 0xD6, 0x24, 0xB4, 0x33, 0x10, 0x1D, 0x3D, 0xC1, 0xBB, 0xC6, 0xF4, 0xA5, 0xFE, 0xB3, 0x69, 0x6B, 0x56, 0xE4, 0x75 };

            // 3. decrypt 9 bytes header with snow cipher
            // generate snow key based on filename+keySalt
            string fileNameWithSalt = fileName + saltStr;
            byte[] chacha20_key1 = new byte[32];
            Span<byte> snowCipherKey = stackalloc byte[16]; // should be 128 but we only use front 16 bytes
            for (int i = 0; i < 32; ++i)
            {
                chacha20_key1[i] = (byte)(fileNameWithSalt[i % fileNameWithSalt.Length] + i);
                chacha20_key1[i] ^= chacha20key_xor[i];
            }
            var empty_nonce = new byte[ChaCha20.allowedNonceLength];
            for (int i = 0; i < snowCipherKey.Length; i++)
            {
                snowCipherKey[i] = (byte)(fileNameWithSalt[i % fileNameWithSalt.Length] + i);
            }
            long headerStartPos = this.BaseStream.Position;

            UInt32 chacha20_hash = 0;
            UInt32 chacha20_entry_count = 0;

            using (var chacha20 = new ChaCha20(chacha20_key1, empty_nonce, 0))
            {
                var ciphertext = br.ReadBytes(ChaCha20.processBytesAtTime);
                var plaintext = new byte[ciphertext.Length];
                chacha20.DecryptBytes(plaintext, ciphertext);
                chacha20_hash = BinaryPrimitives.ReadUInt32LittleEndian(plaintext.AsSpan());
                chacha20_entry_count = BinaryPrimitives.ReadUInt32LittleEndian(plaintext.AsSpan(4));
            }

            var snowCipher = new Snow2CryptoTransform(snowCipherKey, null, false);
            var snowDecoderStream = new CryptoStream(this.BaseStream, snowCipher, CryptoStreamMode.Read);
            var snowReader = new BinaryReader(snowDecoderStream);
            int hash = snowReader.ReadInt32();
            byte version = snowReader.ReadByte();
            int entryCount = snowReader.ReadInt32();

            // verify version and hash
            const int supportedVersion = 2;
            if (version != supportedVersion)
                throw new Exception($"Version check failed. (expected: {supportedVersion}, actual {version})");
            int actualHash = hashedSaltLen + version + entryCount;
            ReadOnlySpan<ushort> u16SaltBytes = MemoryMarshal.Cast<byte, ushort>(saltBytes);
            for (int i = 0; i < u16SaltBytes.Length; i++)
            {
                actualHash += u16SaltBytes[i];
            }
            if (hash != actualHash)
            {
                throw new Exception($"Hash check failed. (expected: {hash}, actual: {actualHash})");
            }

            // 4. skip random bytes
            long entryStartPos = headerStartPos + 9 + fileName.Select(v => (int)v * 3).Sum() % 212 + 33;
            var header = new Ms_Header(fullFileName, saltStr, fileNameWithSalt, hash, version, entryCount, headerStartPos, entryStartPos);
            this.Header = header;
        }

        public void ReadEntriesSnow()
        {
            if (this.Header == null || this.Header.EntryCount == 0 || this.Header.EntryCount == this.Entries.Count)
            {
                return;
            }
            this.Entries.Clear();
            int entryCount = this.Header.EntryCount;
            if (this.Entries.Capacity < entryCount)
            {
                this.Entries.Capacity = entryCount;
            }

            // decrypt with another snow key
            string fileNameWithSalt = this.Header.FileNameWithSalt;
            Span<byte> snowCipherKey2 = stackalloc byte[16];
            for (int i = 0; i < snowCipherKey2.Length; i++)
            {
                snowCipherKey2[i] = (byte)(i + (i % 3 + 2) * fileNameWithSalt[fileNameWithSalt.Length - 1 - i % fileNameWithSalt.Length]);
            }
            var snowCipher = new Snow2CryptoTransform(snowCipherKey2, null, false);
            this.BaseStream.Position = this.Header.EntryStartPosition;
            var snowDecoderStream = new CryptoStream(this.BaseStream, snowCipher, CryptoStreamMode.Read);
            var snowReader = new BinaryReader(snowDecoderStream, Encoding.Unicode, true);

            for (int i = 0; i < entryCount; i++)
            {
                int entryNameLen = snowReader.ReadInt32();
                string entryName = new string(snowReader.ReadChars(entryNameLen));
                int checkSum = snowReader.ReadInt32();
                int flags = snowReader.ReadInt32();
                int startPos = snowReader.ReadInt32();
                int size = snowReader.ReadInt32();
                int sizeAligned = snowReader.ReadInt32();
                int unk1 = snowReader.ReadInt32();
                int unk2 = snowReader.ReadInt32();
                byte[] entryKey = snowReader.ReadBytes(16);

                var entry = new Ms_Entry(entryName, checkSum, flags, startPos, size, sizeAligned, unk1, unk2, entryKey);
                entry.CalculatedCheckSum = flags + startPos + size + sizeAligned + unk1 + entryKey.Sum(b => (int)b);
                this.Entries.Add(entry);
            }

            long dataStartPos = this.BaseStream.Position;
            // align to 1024 bytes
            if ((dataStartPos & 0x3ff) != 0)
            {
                dataStartPos = dataStartPos - (dataStartPos & 0x3ff) + 0x400;
            }
            this.Header.DataStartPosition = dataStartPos;
            // recalculate startPos
            foreach(var entry in this.Entries)
            {
                entry.StartPos = dataStartPos + entry.StartPos * 1024;
            }
        }

        public void ReadEntriesChaCha20()
        {

        }

        public void GetDirTree(Wz_Node parent)
        {
            foreach (var entry in this.Entries)
            {
                Wz_Node root = parent;
                string[] fullPath = entry.Name.Split('/');
                foreach (var segment in fullPath)
                {
                    root = root.Nodes[segment] ?? root.Nodes.Add(segment);
                }

                // always override existing value if already exists?
                //if (root.Value == null)
                {
                    var msImage = new Ms_Image(fullPath[fullPath.Length - 1], entry, this);
                    root.Value = msImage;
                    msImage.OwnerNode = root;
                }
            }
        }

        public void Close()
        {
            if (this.BaseStream != null)
            {
                if (!this.leaveOpen)
                {
                    this.BaseStream.Dispose();
                }
                this.BaseStream = null;
            }
        }

        void IDisposable.Dispose()
        {
            this.Close();
        }
    }
}
