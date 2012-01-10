using System.Collections.Generic;
using System.ComponentModel.Composition;
using ClinImIm.Applications.Core;

namespace ClinImIm.Presentation.Infrastructure
{
    [Export(typeof(IFileEnumerator))]
    public class SystemFileEnumerator : IFileEnumerator
    {
        public IEnumerable<System.IO.FileInfo> EnumerateFiles(System.IO.DirectoryInfo dir)
        {
            return dir.EnumerateFiles();
        }


        public IEnumerable<System.IO.DirectoryInfo> EnumerateDirectories(System.IO.DirectoryInfo dir)
        {
            return dir.EnumerateDirectories();
        }
    }
}
