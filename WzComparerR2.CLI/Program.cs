using System;
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
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                PrintUsage();
                return;
            }
            else
            {
                foreach (string arg in args)
                {
                    Console.WriteLine(arg);
                }
                Console.WriteLine("Hello, World!");
            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Invalid argument");
        }
    }
}
