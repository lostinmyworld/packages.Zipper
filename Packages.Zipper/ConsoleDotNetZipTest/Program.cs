using Common;
using Packages.Zipper.DotNetZip;
using System;
using System.Linq;

namespace ConsoleDotNetZipTest
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Start compression...");

            var files = Extensions.ReadFiles();
            Console.WriteLine($"Read #{files.Count()} files.");

            var zipper = new DotNetZipZipper();
            var compressedFile = zipper.Compress(files);
            Console.WriteLine("Compression done!");
            Console.WriteLine($"Compressed file name: {compressedFile.FileName}, bytes: #{compressedFile.Content.Length}");

            compressedFile.WriteFile();

            Console.WriteLine("Export to compressed file done...");

            var decompressedFiles = zipper.Decompress(compressedFile);
            Console.WriteLine("Extracted to decompressed files done...");

            decompressedFiles.WriteFiles();
        }

    }
}
