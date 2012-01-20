using ClinImIm.Applications.Views;

namespace ClinImIm.Applications.Test.Mocks
{
    public class ImportImagesViewValid : IImportImagesView
    {
        public System.Collections.Generic.IEnumerable<string> TryImport()
        {
            TryImportHasBeenCalled = true;
            return new string[] { };
        }

        public object DataContext { get; set; }

        public bool TryImportHasBeenCalled { get; private set; }
    }
}
