using Common.Models;
using System.Collections.Generic;

namespace Packages.Zipper.DotNetZip
{
    public interface IDotNetZipZipper
    {
        FileContent Compress(IEnumerable<FileContent> files, string zipName);
        IEnumerable<FileContent> Decompress(FileContent compressedFile);
    }
}
