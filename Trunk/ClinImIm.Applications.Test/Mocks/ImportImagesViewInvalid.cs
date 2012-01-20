using System.Collections.Generic;
using ClinImIm.Applications.Views;

namespace ClinImIm.Applications.Test.Mocks
{
    public class ImportImagesViewInvalid : IImportImagesView
    {
        public System.Collections.Generic.IEnumerable<string> TryImport()
        {
            TryImportHasBeenCalled = true;
            var items = new List<string>();
            items.Add("The images could not be read from.");
            items.Add("The destination folder could not be created.");
            return items;
        }

        public object DataContext { get; set; }

        public bool TryImportHasBeenCalled { get; private set; }
    }
}
