using System.Collections.Generic;
using System.Windows.Controls;
using ClinImIm.Domain;

namespace ClinImIm.Plugin.Contracts
{
    public interface IImportImagesPlugin
    {
        UserControl GetUserControl();

        IEnumerable<string> TryImport(ImagesToImport model);
    }
}
