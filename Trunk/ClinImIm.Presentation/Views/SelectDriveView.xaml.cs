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
    [Export(typeof(ISelectDriveView))]
    public partial class SelectDriveView : ISelectDriveView
    {
        public SelectDriveView()
        {
            InitializeComponent();
        }
    }
}
