using System.ComponentModel.Composition;
using System.Windows.Controls;
using ClinImIm.Applications.Views;
using System;
using ClinImIm.Applications.ViewModels;
using System.Waf.Applications;

namespace ClinImIm.Presentation.Views
{
    /// <summary>
    /// Interaction logic for SelectDriveView.xaml
    /// </summary>
    [Export(typeof(ISelectDriveView)), PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SelectDriveView : UserControl, ISelectDriveView
    {
        public SelectDriveView()
        {
            InitializeComponent();
        }
    }
}
