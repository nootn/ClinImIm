using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Waf.Applications;
using ClinImIm.Applications.ViewModels;
using ClinImIm.Applications.Views;
using ClinImIm.Domain;
using ClinImIm.Plugin.Contracts;

namespace ClinImIm.Presentation.Views
{
    /// <summary>
    /// Interaction logic for ImportImagesView.xaml
    /// </summary>
    [Export(typeof(IImportImagesView))]
    public partial class ImportImagesView : IImportImagesView
    {
        private IImportImagesPlugin _plugin;
        private ImagesToImport _model;

        [ImportingConstructor]
        public ImportImagesView(ImagesToImport model)
        {
            InitializeComponent();
            _model = model;
        }

        public IEnumerable<string> TryImport()
        {
            return _plugin.TryImport(_model);
        }

        private void UserControlLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var catalog = new DirectoryCatalog(Properties.Resources.PluginDirectory);
            var container = new CompositionContainer(catalog);
            _plugin = container.GetExportedValue<IImportImagesPlugin>();

            HostContainer.Children.Add(_plugin.GetUserControl());

            //var vm = ViewHelper.GetViewModel<SelectPatientViewModel>(this);
            //vm.Model = _plugin.Model;
        }
    }
}
