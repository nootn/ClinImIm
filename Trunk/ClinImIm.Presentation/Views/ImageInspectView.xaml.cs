using System;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.ViewModels;
using ClinImIm.Applications.Views;

namespace ClinImIm.Presentation.Views
{
    /// <summary>
    /// Interaction logic for ImageInspectView.xaml
    /// </summary>
    [Export(typeof(IImageInspectView))]
    public partial class ImageInspectView : Window, IImageInspectView
    {
        private Point _imgTranslateStart;
        private readonly Lazy<ImageInspectViewModel> _viewModel;

        [ImportingConstructor]
        public ImageInspectView()
        {
            InitializeComponent();

            _viewModel = new Lazy<ImageInspectViewModel>(() => this.GetViewModel<ImageInspectViewModel>());

            img.MouseLeftButtonDown += ImageMouseLeftButtonDown;
            img.MouseLeftButtonUp += ImageMouseLeftButtonUp;
            img.MouseMove += ImageMouseMove;
        }

        private void ImageMouseMove(object sender, MouseEventArgs e)
        {
            if (img.IsMouseCaptured)
            {
                var disp = _imgTranslateStart - e.GetPosition(img);
                _viewModel.Value.TranslateImage(disp);
            }
        }

        private void ImageMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            img.ReleaseMouseCapture();
        }

        private void ImageMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!_viewModel.Value.CanTranslateImage)
                return;

            _imgTranslateStart = e.GetPosition(img);
            img.CaptureMouse();
        }

        public Size ImageContainerSize
        {
            get { return new Size(imgContainer.ActualWidth, imgContainer.ActualHeight); }
        }
    }
}
