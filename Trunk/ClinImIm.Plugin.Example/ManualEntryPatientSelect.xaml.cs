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
    [Export(typeof(IPatientSelectPlugin))]
    public partial class ManualEntryPatientSelect : UserControl, IPatientSelectPlugin
    {
        public ManualEntryPatientSelect()
        {
            InitializeComponent();
            Model = new Patient { FullName = "asd", Identifier = "123" };
            this.DataContext = Model;
        }

        public UserControl GetUserControl()
        {
            return this;
        }

        public Patient Model { get; private set; }

        private void HyperlinkRequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

    }
}
