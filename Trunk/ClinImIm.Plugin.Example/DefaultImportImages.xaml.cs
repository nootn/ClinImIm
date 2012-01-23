using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows.Controls;
using ClinImIm.Domain;
using ClinImIm.Plugin.Contracts;
using ClinImIm.Plugin.Example.Domain;
using ClinImIm.Plugin.Example.ViewModels;

namespace ClinImIm.Plugin.Example
{
    /// <summary>
    /// Interaction logic for ManualEntryPatientSelect.xaml
    /// </summary>
    [Export(typeof(IImportImagesPlugin))]
    public partial class DefaultImportImages : IImportImagesPlugin
    {
        private readonly DefaultImportImagesViewModel _viewModel;

        [ImportingConstructor]
        public DefaultImportImages()
        {
            InitializeComponent();

            _viewModel = new DefaultImportImagesViewModel(new ImportDetails());
            DataContext = _viewModel;
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
            return _viewModel.TryImport(model);
        }
    }
}
