using Common;
using Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Packages.Zipper
{
    public class Zipper : IZipper
    {
        public FileContent Compress(IEnumerable<FileContent> files, string zipName = "responseFile.zip")
        {
            if (files == null || !files.Any())
            {
                throw new ArgumentException("No file was found to compress.");
            }

            using var memoryStream = new MemoryStream();
            using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                foreach (var file in files)
                {
                    var zipItem = zipArchive.CreateEntry(file.FileName);

                    using var originalFileStream = new MemoryStream(file.Content);
                    using var entryStream = zipItem.Open();

                    originalFileStream.CopyTo(entryStream);
                }
            }

            return memoryStream.ConvertToZipFile(zipName, "zip");
        }
    }
}
