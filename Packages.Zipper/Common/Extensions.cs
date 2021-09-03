using Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Common
{
    public static class Extensions
    {
        private const string DIRECTORY_FILES = "Files";

        public static FileContent ConvertToZipFile(this MemoryStream memoryStream, string fileName, string extension)
        {
            return new FileContent
            {
                Content = memoryStream.ToArray(),
                FileName = fileName,
                Extension = extension
            };
        }

        public static void WriteFiles(this IEnumerable<FileContent> files)
        {
            foreach (var file in files)
            {
                file.FileName = $"new_{file.FileName}";
                WriteFile(file);
            }
        }

        public static void WriteFile(this FileContent file)
        {
            using (var fileStream = new FileStream(file.FileName, FileMode.Create))
            {
                fileStream.Write(file.Content, 0, file.Content.Length);
            }
        }

        public static IEnumerable<FileContent> ReadFiles()
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

            return files.Select(Map);
        }

        private static FileContent Map(FileInfo file)
        {
            return new FileContent
            {
                FileName = file.Name,
                Content = File.ReadAllBytes(file.FullName),
                Extension = file.Extension
            };
        }
    }
}
