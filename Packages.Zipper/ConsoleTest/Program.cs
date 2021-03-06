using Common;
using Packages.Zipper;
using System;
using System.Linq;

namespace ConsoleTest
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Start compression...");

            var files = Extensions.ReadFiles();
            Console.WriteLine($"Read #{files.Count()} files.");

            var zipper = new Zipper();
            var compressedFile = zipper.Compress(files);
            Console.WriteLine("Compression done!");
            Console.WriteLine($"Compressed file name: {compressedFile.FileName}, bytes: #{compressedFile.Content.Length}");

            compressedFile.WriteFile();

            Console.WriteLine("Export to compressed file done...");
        }
    }
}
