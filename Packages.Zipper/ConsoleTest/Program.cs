using Packages.Zipper;
using Packages.Zipper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleTest
{
    internal static class Program
    {
        private const string DIRECTORY_FILES = "Files";

        private static void Main()
        {
            Console.WriteLine("Start compression...");

            var files = ReadFiles();
            Console.WriteLine($"Read #{files.Count()} files.");

            var zipper = new Zipper();
            var compressedFile = zipper.Compress(files);
            Console.WriteLine("Compression done!");
            Console.WriteLine($"Compressed file name: {compressedFile.FileName}, bytes: #{compressedFile.Content.Length}");

            WriteFile(compressedFile);

            Console.WriteLine("Export to compressed file done...");
        }

        private static IEnumerable<FileContent> ReadFiles()
        {
            if (!Directory.Exists(DIRECTORY_FILES))
            {
                throw new ArgumentException("Could not find directory to read files from.");
            }

            var directoryInfo = new DirectoryInfo("Files");
            var files = directoryInfo.GetFiles();
            if (files == null || !files.Any())
            {
                throw new ArgumentException("No files found to compress.");
            }

            return files.Select(f => new FileContent
            {
                FileName = f.Name,
                Content = File.ReadAllBytes(f.FullName),
                Extension = f.Extension
            });
        }

        private static void WriteFile(FileContent file)
        {
            using (var fileStream = new FileStream(file.FileName, FileMode.Create))
            {
                fileStream.Write(file.Content, 0, file.Content.Length);
            }
        }
    }
}
