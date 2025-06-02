using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

#if NET6_0_OR_GREATER
using System.Runtime.Loader;
using System.Text;
#endif

namespace WzComparerR2.CLI
{
    class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AllocConsole();
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                PrintUsage();
                return;
            }
            else
            {
                switch (args[0])
                {
                    case "patch":
                        CliPatcherSession(args);
                        return;
                    default:
                        PrintUsage();
                        break;
                }
                Console.WriteLine("Hello, World!");
            }
        }

        private static void CliPatcherSession(string[] args)
        {
            CliPatcher patcher = new CliPatcher();
            switch (args[1])
            {
                case "find":
                    if (args.Length < 4)
                    {
                        PrintUsage("find");
                        return;
                    }
                    else
                    {
                        if (args[2].ToUpper().Contains("MINOR"))
                        {
                            if (args.Length < 5)
                            {
                                PrintUsage("find");
                                return;
                            }
                            patcher.GameRegion = args[2].ToUpper();
                            patcher.BaseVersion = int.Parse(args[3]);
                            patcher.OldVersion = int.Parse(args[4]);
                            patcher.NewVersion = int.Parse(args[5]);
                        }
                        else
                        {
                            patcher.GameRegion = args[2].ToUpper();
                            patcher.OldVersion = int.Parse(args[3]);
                            patcher.NewVersion = int.Parse(args[4]);
                        }
                        try
                        {
                            patcher.TryGetPatch();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                            return;
                        }
                        return;
                    }
                case "apply":
                    if (args.Length < 4)
                    {
                        PrintUsage("apply");
                        return;
                    }
                    else
                    {
                        bool immediatePatch = args.Contains("--immediate");
                        bool verbose = args.Contains("--verbose");
                        string patchFile = args[2];
                        string gameDirectory = args[3];
                        if (!File.Exists(patchFile))
                        {
                            Console.WriteLine("Error: Patch file or game directory does not exist, or the path is invalid.");
                            return;
                        }
                        if (!File.Exists(Path.Combine(gameDirectory, "MapleStory.exe")) && !File.Exists(Path.Combine(gameDirectory, "MapleStoryT.exe")))
                        {
                            Console.WriteLine("Warning: The specified game directory seems not a valid MapleStory directory.");
                            Console.WriteLine("If you'd like to proceed anyway, press Y.");
                            Console.WriteLine("Pressing any other keys will cancel operation.");
                            ConsoleKeyInfo cki = Console.ReadKey();
                            if (cki.Key != ConsoleKey.Y) return;
                        }
                        if (!HasWritePermission(gameDirectory))
                        {
                            Console.WriteLine("Error: You do not have write permission to the specified game directory.");
                            Console.WriteLine("To proceed, please run WCR2CLI with Administrator privilege.");
                            return;
                        }
                        try
                        {
                            patcher.ApplyPatch(patchFile, gameDirectory, immediatePatch, verbose);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                            return;
                        }
                    }
                    break;
                default:
                    PrintUsage();
                    return;
            }
        }

        static bool HasWritePermission(string directoryPath)
        {
            try
            {
                string testFilePath = Path.Combine(directoryPath, "test.tmp");
                using (FileStream fs = File.Create(testFilePath, 1, FileOptions.DeleteOnClose)) { }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void PrintUsage(string subarg="")
        {
            Console.WriteLine("Invalid argument");
        }
    }
}
