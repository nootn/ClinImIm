using System;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.Views;
using ClinImIm.Domain;

namespace ClinImIm.Applications.ViewModels
{
    [Export, PartCreationPolicy(CreationPolicy.NonShared)]
    public class ImageInspectViewModel : ViewModel<IImageInspectView>
    {
        private const double FixedDpi = 96;

        private readonly IImageInspectView _view;
        private readonly IMainWindowProvider _mainWindowProvider;
        private ImageInspection _model;
        private readonly ICommand _close;
        private readonly ICommand _setZoom;
        private double _zoom;
        private BitmapImage _imageSource;

        [ImportingConstructor]
        public ImageInspectViewModel(IImageInspectView view, IMainWindowProvider mainWindowProvider)
            : base(view)
        {
            if (view == null) throw new ArgumentNullException("view");

            _view = view;
            _mainWindowProvider = mainWindowProvider;
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
            RaisePropertyChanged("ActualZoom");
        }

        private double GetFitFactor()
        {
            if (_imageSource == null) return 1.0;

            var size = _view.ImageContainerSize;
            
            if (size.Width < 0.0001 || size.Height < 0.0001) return 1.0;

            var widthScale = size.Width / _imageSource.PixelWidth;
            var heightScale = size.Height / _imageSource.PixelHeight;

            return Math.Min(widthScale, heightScale);
        }

        private double TranslateZoomToActualZoom(double zoom)
        {
            return zoom/GetFitFactor();
            //return _imageSource == null ? 0 : _imageSource.DpiX*zoom/FixedDpi;
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
                RaisePropertyChanged("ActualZoom");
            }
        }

        public double ActualZoom
        {
            get { return TranslateZoomToActualZoom(_zoom); }
        }

        public void ShowView()
        {
            _view.Show();
            _view.Owner = _mainWindowProvider.Window;
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