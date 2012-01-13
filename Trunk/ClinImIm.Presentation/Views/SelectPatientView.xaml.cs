using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Waf.Applications;
using System.Windows.Controls;
using ClinImIm.Applications.ViewModels;
using ClinImIm.Applications.Views;
using ClinImIm.Plugin.Contracts;

namespace ClinImIm.Presentation.Views
{
    /// <summary>
    /// Interaction logic for SelectPatientView.xaml
    /// </summary>
    [Export(typeof(ISelectPatientView)), PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SelectPatientView : UserControl, ISelectPatientView
    {
        private IPatientSelectPlugin _plugin;

        public SelectPatientView()
        {
            InitializeComponent();
        }

        private void UserControlLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var catalog = new DirectoryCatalog("PluginsToLoad");
            var container = new CompositionContainer(catalog);
            _plugin = container.GetExportedValue<IPatientSelectPlugin>();

            HostContainer.Children.Add(_plugin.GetUserControl());

            var vm = ViewHelper.GetViewModel<SelectPatientViewModel>(this);
            vm.Model = _plugin.Model;
        }
    }
}
