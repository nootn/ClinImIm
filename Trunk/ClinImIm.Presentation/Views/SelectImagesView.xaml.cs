using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClinImIm.Applications.Views;

namespace ClinImIm.Presentation.Views
{
    /// <summary>
    /// Interaction logic for SelectImagesView.xaml
    /// </summary>
    [Export(typeof(ISelectImagesView)), PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SelectImagesView : UserControl, ISelectImagesView
    {
        public SelectImagesView()
        {
            InitializeComponent();

            
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Debugger.Break();
        }
    }
}
