using System;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ClinImIm.Applications.Views;
using ClinImIm.Domain;

namespace ClinImIm.Applications.ViewModels
{
    [Export, PartCreationPolicy(CreationPolicy.NonShared)]
    public class ImageInspectViewModel : ViewModel<IImageInspectView>
    {
        private readonly IImageInspectView _view;
        private ImageInspection _model;
        private readonly ICommand _close;
        private readonly ICommand _setZoom;
        private double _zoom;
        private BitmapImage _imageSource;

        [ImportingConstructor]
        public ImageInspectViewModel(IImageInspectView view)
            : base(view)
        {
            if (view == null) throw new ArgumentNullException("view");

            _view = view;
            _close = new DelegateCommand(() => _view.Hide());
            _setZoom = new DelegateCommand(
                z =>
                    {
                        double zoom;
                        if (z != null && double.TryParse(z.ToString(), out zoom))
                        {
                            Zoom = zoom;
                        }
                        else
                        {
                            SetZoomToFit();
                        }
                    });
        }

        public ImageInspection Model
        {
            get { return _model; }
            set
            {
                if (_model != value)
                {
                    _model = value;
                    _imageSource = new BitmapImage(new Uri(_model.FullPath, UriKind.Relative));
                    SetZoomToFit();
                    RaisePropertyChanged("Model");
                    RaisePropertyChanged("ImageSource");
                }
            }
        }

        private void SetZoomToFit()
        {
            var size = _view.ImageContainerSize;
            var widthScale = size.Width/_imageSource.PixelWidth;
            var heightScale = size.Height/_imageSource.PixelHeight;

            var zoomFactor = Math.Min(widthScale, heightScale);
            _zoom = zoomFactor;
            RaisePropertyChanged("Zoom");
        }

        public BitmapImage ImageSource
        {
            get { return _imageSource; }
        }

        public double Zoom
        {
            get { return _zoom; }
            set
            {
                value = Math.Max(0.5d, Math.Min(3.0d, value));
                _zoom = value;
                RaisePropertyChanged("Zoom");
            }
        }

        public void ShowView()
        {
            _view.Show();
            SetZoomToFit();
        }

        public ICommand Close
        {
            get { return _close; }
        }

        public ICommand SetZoom
        {
            get { return _setZoom; }
        }
    }
}