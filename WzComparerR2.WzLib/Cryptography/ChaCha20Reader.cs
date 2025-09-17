#if NET8_0_OR_GREATER
using CSChaCha20;
using System;
using System.Buffers.Binary;
using System.IO;
using System.Text;

namespace WzComparerR2.WzLib.Cryptography
{
    internal class ChaCha20Reader
    {
        private readonly Stream _stream;
        private readonly byte[] _key;
        private readonly byte[] _nonce;

        private byte[]? _buffer;
        private int _readed;
        private UInt32 _counter;


        public ChaCha20Reader(Stream stream, byte[] key, byte[] nonce)
        {
            _stream = stream;
            _key = key;
            _nonce = nonce;
            _buffer = null;
            _readed = 0;
        }

        public int ReadBytes(byte[] buffer, int count, int index = 0)
        {
            if (_buffer == null)
            {
                _buffer = new byte[ChaCha20.processBytesAtTime];
                _stream.Read(_buffer, 0, ChaCha20.processBytesAtTime);
                using var chacha20 = new ChaCha20(_key, _nonce, _counter++);
                chacha20.DecryptBytes(_buffer, _buffer, ChaCha20.processBytesAtTime);
                _readed = 0;
            }

            if (ChaCha20.processBytesAtTime - _readed >= count)
            {
                Buffer.BlockCopy(_buffer, _readed, buffer, index, count);
                _readed += count;
            }
            else
            {
                int remaining = ChaCha20.processBytesAtTime - _readed;
                Buffer.BlockCopy(_buffer, _readed, buffer, index, remaining);

                _buffer = new byte[ChaCha20.processBytesAtTime];
                _stream.Read(_buffer, 0, ChaCha20.processBytesAtTime);
                using var chacha20 = new ChaCha20(_key, _nonce, _counter++);
                chacha20.DecryptBytes(_buffer, _buffer, ChaCha20.processBytesAtTime);
                _readed = 0;

                ReadBytes(buffer, count - remaining, index + remaining);
            }

            if (_readed >= ChaCha20.processBytesAtTime)
            {
                _buffer = null;
                _counter = 0;
            }
            return 0;
        }

        public Int32 ReadInt32()
        {
            byte[] temp = new byte[4];
            ReadBytes(temp, 4);
            return BinaryPrimitives.ReadInt32LittleEndian(temp.AsSpan());
        }

        public string ReadUnicodeString()
        {
            var char_count = ReadInt32();
            byte[] char_bytes = new byte[char_count * 2];
            ReadBytes(char_bytes, char_count * 2);
            return Encoding.Unicode.GetString(char_bytes);
        }
    }
}
#endif