using Common.Models;
using System.Collections.Generic;

namespace Packages.Zipper
{
    public interface IZipper
    {
        FileContent Compress(IEnumerable<FileContent> files, string zipName);
    }
}
