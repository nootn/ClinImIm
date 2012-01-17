using System.ComponentModel.Composition;
using System.Windows;
using ClinImIm.Applications.Views;

namespace ClinImIm.Presentation.Views
{
    /// <summary>
    /// Interaction logic for ImageInspectWindow.xaml
    /// </summary>
    [Export(typeof(IImageInspectWindow))]
    public partial class ImageInspectWindow : Window, IImageInspectWindow
    {
        public ImageInspectWindow()
        {
            InitializeComponent();
        }
    }
}
