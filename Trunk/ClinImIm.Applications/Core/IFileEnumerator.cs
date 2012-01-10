using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ClinImIm.Applications.Core
{
    public interface IFileEnumerator
    {
        IEnumerable<FileInfo> EnumerateFiles(DirectoryInfo dir);
        IEnumerable<DirectoryInfo> EnumerateDirectories(DirectoryInfo dir);
    }
}
