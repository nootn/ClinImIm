using System.Collections.Generic;
using System.IO;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.ViewModels;

namespace ClinImIm.Applications.Test.Mocks
{
    public class FileEnumeratorHasMoreThanMaxNumberOfFiles : IFileEnumerator
    {
        public IEnumerable<FileInfo> EnumerateFiles(DirectoryInfo dir)
        {
            var items = new List<FileInfo> {new FileInfo(string.Concat(dir.FullName, @"\test.jpg"))};
            return items;
        }

        public IEnumerable<DirectoryInfo> EnumerateDirectories(DirectoryInfo dir)
        {
            var items = new List<DirectoryInfo>();
            int currDirNumber;
            int.TryParse(dir.Name, out currDirNumber);

            if (currDirNumber <= SelectDriveViewModel.MaximumNumberOfFiles + 1)
            {
                items.Add(new DirectoryInfo(string.Concat(dir.FullName, @"\", currDirNumber + 1)));
            }

            return items;
        }
    }
}
