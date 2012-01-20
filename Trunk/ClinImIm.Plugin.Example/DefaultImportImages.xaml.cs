using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows.Controls;
using ClinImIm.Domain;
using ClinImIm.Plugin.Contracts;

namespace ClinImIm.Plugin.Example
{
    /// <summary>
    /// Interaction logic for ManualEntryPatientSelect.xaml
    /// </summary>
    [Export(typeof(IImportImagesPlugin))]
    public partial class DefaultImportImages : IImportImagesPlugin
    {
        [ImportingConstructor]
        public DefaultImportImages()
        {
            InitializeComponent();
        }

        public UserControl GetUserControl()
        {
            return this;
        }

        private void HyperlinkRequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        public IEnumerable<string> TryImport(ImagesToImport model)
        {
            var errorMessages = new List<string>();

            //If you don't add an error, it is "successful"
            errorMessages.Add("We need to now implement this example way of importing images!");

            return errorMessages;
        }
    }
}
