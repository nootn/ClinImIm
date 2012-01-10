using System.Collections.Generic;
using System.IO;
using ClinImIm.Applications.Core;

namespace ClinImIm.Applications.Test.Mocks
{
    public class FileEnumeratorNoFiles : IFileEnumerator
    {
        public IEnumerable<FileInfo> EnumerateFiles(DirectoryInfo dir)
        {
            return new List<FileInfo>();
        }

        public IEnumerable<DirectoryInfo> EnumerateDirectories(DirectoryInfo dir)
        {
            return new List<DirectoryInfo>();
        }
    }
}
