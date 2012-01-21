using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media.Imaging;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.Views;

namespace ClinImIm.Presentation.Views
{
    /// <summary>
    /// Interaction logic for ImageInspectView.xaml
    /// </summary>
    [Export(typeof(IImageInspectView))]
    public partial class ImageInspectView : Window, IImageInspectView
    {
        [ImportingConstructor]
        public ImageInspectView()
        {
            InitializeComponent();
        }

        public Size ImageContainerSize
        {
            get { return new Size(imgContainer.ActualWidth, imgContainer.ActualHeight); }
        }
    }
}
