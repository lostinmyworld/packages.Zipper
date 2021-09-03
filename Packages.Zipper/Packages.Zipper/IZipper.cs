using Packages.Zipper.Models;
using System.Collections.Generic;

namespace Packages.Zipper
{
    public interface IZipper
    {
        FileContent Compress(FileContent file, string zipName);
        FileContent Compress(IEnumerable<FileContent> files, string zipName);
    }
}
