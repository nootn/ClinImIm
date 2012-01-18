using System.ComponentModel.Composition;
using System.Windows;
using ClinImIm.Applications.Views;

namespace ClinImIm.Presentation.Views
{
    /// <summary>
    /// Interaction logic for ImageInspectView.xaml
    /// </summary>
    [Export(typeof(IImageInspectView))]
    public partial class ImageInspectView : Window, IImageInspectView
    {
        public ImageInspectView()
        {
            InitializeComponent();
        }
    }
}
