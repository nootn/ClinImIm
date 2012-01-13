using System.ComponentModel.Composition;
using System.Windows.Controls;
using ClinImIm.Applications.Views;

namespace ClinImIm.Presentation.Views
{
    /// <summary>
    /// Interaction logic for SelectPatientView.xaml
    /// </summary>
    [Export(typeof(ISelectPatientView)), PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SelectPatientView : UserControl, ISelectPatientView
    {
        public SelectPatientView()
        {
            InitializeComponent();
        }
    }
}
