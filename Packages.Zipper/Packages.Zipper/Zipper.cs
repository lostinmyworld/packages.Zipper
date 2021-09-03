using Packages.Zipper.Models;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Packages.Zipper
{
    public class Zipper : IZipper
    {
        public FileContent Compress(FileContent file, string zipName = "responseFile.zip")
        {
            var multipleFiles = new List<FileContent>
            {
                file
            };

            return Compress(multipleFiles, zipName);
        }

        public FileContent Compress(IEnumerable<FileContent> files, string zipName = "responseFile.zip")
        {
            using var memoryStream = new MemoryStream();
            using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                foreach (var file in files)
                {
                    var zipItem = zipArchive.CreateEntry(file.FileName);

                    using (var originalFileStream = new MemoryStream(file.Content))
                    using (var entryStream = zipItem.Open())
                    {
                        originalFileStream.CopyTo(entryStream);
                    }
                }
            }

            return new FileContent
            {
                Content = memoryStream.ToArray(),
                FileName = zipName,
                Extension = "zip"
            };
        }
    }
}
