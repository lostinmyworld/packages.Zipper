using Common;
using Common.Models;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Packages.Zipper.DotNetZip
{
    public class DotNetZipZipper : IDotNetZipZipper
    {
        public FileContent Compress(IEnumerable<FileContent> files, string zipName = "responseFile.zip")
        {
            if (files == null || !files.Any())
            {
                throw new ArgumentException("No file was found to compress.");
            }

            using var memoryStream = new MemoryStream();
            using (var zip = new ZipFile())
            {
                foreach (var file in files)
                {
                    zip.AddEntry(file.FileName, file.Content);
                }
                zip.Save(memoryStream);
            }

            return memoryStream.ConvertToZipFile(zipName, "zip");
        }

        public IEnumerable<FileContent> Decompress(FileContent compressedFile)
        {
            var extractedFiles = new List<FileContent>();

            using (var inMemoryStream = new MemoryStream(compressedFile.Content))
            using (var zip = ZipFile.Read(inMemoryStream))
            {
                foreach (var entry in zip)
                {
                    var tempMemoryStream = new MemoryStream();
                    entry.Extract(tempMemoryStream);

                    extractedFiles.Add(tempMemoryStream.ConvertToZipFile(entry.FileName, ""));
                }
            }

            return extractedFiles;
        }
    }
}
