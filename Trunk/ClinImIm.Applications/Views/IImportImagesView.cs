using System.Collections.Generic;
using System.Waf.Applications;

namespace ClinImIm.Applications.Views
{
    public interface IImportImagesView : IView
    {
        IEnumerable<string> TryImport();
    }
}
