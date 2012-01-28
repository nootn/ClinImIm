using System;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using System.Windows;
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
        private readonly IImageInspectView _view;
        private readonly IMainWindowProvider _mainWindowProvider;
        private ImageInspection _model;
        private readonly ICommand _close;
        private readonly ICommand _setZoom;
        private double _zoom;
        private BitmapImage _imageSource;
        private double _minZoom;
        private double _maxZoom;
        private Vector _origin;

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
            _minZoom = 0.125;
            _maxZoom = 3.0;
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
                    SetZoomToDefault();
                    RaisePropertyChanged("Model");
                    RaisePropertyChanged("ImageSource");
                }
            }
        }

        /// <summary>
        /// Sets the default zoom for the current image. Default is "Fit" if the image is larger than
        /// the available space, otherwise "1:1".
        /// </summary>
        private void SetZoomToDefault()
        {
            SetZoomInternal(Math.Min(GetFitRatio(), 1.0));
        }

        /// <summary>
        /// Sets the zoom factor to fit the image to the available size. RenderZoom should be 1 when image is set to fit
        /// (this assumes that the Image control has Stretch="Uniform"), and Zoom will show the image's zoom factor based
        /// on its PixelWidth and PixelHeight
        /// </summary>
        private void SetZoomToFit()
        {
            SetZoomInternal(GetFitRatio());
        }
        
        /// <summary>
        /// Uses the image's PixelWidth and PixelHeight to get a value indicating how much the image is "zoomed"
        /// when it is set to fit the available space (it assumes the Image control housing it is set to Stretch="Uniform").
        /// This allows the image's "real" zoom value to be monitored and adjusted by the user, but a translated value to be
        /// applied to the Image's ScaleTransform. This helps get around WPF drawing the image at different sizes based on its
        /// DPI metadata. It also ensures the image is always fully rendered by using Stretch="Uniform", but allows scaling to
        /// still be accurate
        /// </summary>
        /// <returns>Image's "actual" zoom value (based on PixelWidth and Height) when it is scaled to fit the container</returns>
        private double GetFitRatio()
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
            return zoom/GetFitRatio();
        }

        public BitmapImage ImageSource
        {
            get { return _imageSource; }
        }

        /// <summary>
        /// Zoom factor of the image. Setting value is clamped between MinZoom and MaxZoom
        /// </summary>
        public double Zoom
        {
            get { return _zoom; }
            set
            {
                value = Math.Max(MinZoom, Math.Min(MaxZoom, value));
                SetZoomInternal(value);
            }
        }

        private void SetZoomInternal(double zoom)
        {
            var renderPrev = RenderZoom;
            _zoom = zoom;
            var renderNew = RenderZoom;
            if (renderNew > 1)
            {
                var normalised = _origin/renderPrev;
                var centerFactor = Math.Min(renderNew - 1, 1);
                _origin = normalised*renderNew*centerFactor;
            }
            else
            {
                _origin = new Vector(0, 0);
            }
            
            RaisePropertyChanged("Zoom");
            RaisePropertyChanged("RenderZoom");
            RaisePropertyChanged("TranslateX");
            RaisePropertyChanged("TranslateY");
        }

        /// <summary>
        /// Value to apply to ScaleTransform to render the image at the value specified in Zoom.
        /// This assumes that Image control hosting image has Streth="Uniform"
        /// </summary>
        public double RenderZoom
        {
            get { return TranslateZoomToActualZoom(_zoom); }
        }

        public double MaxZoom
        {
            get { return _maxZoom; }
            set
            {
                if (value > _minZoom)
                {
                    _maxZoom = value;
                    RaisePropertyChanged("MaxZoom");
                }
            }
        }

        public double MinZoom
        {
            get { return _minZoom; }
            set
            {
                if (value < _maxZoom)
                {
                    _minZoom = value;
                    RaisePropertyChanged("MinZoom");
                }
            }
        }

        public bool CanTranslateImage
        {
            get { return _zoom > GetFitRatio(); }
        }

        public double TranslateX
        {
            get { return _origin.X; }
        }

        public double TranslateY
        {
            get { return _origin.Y; }
        }

        public void ShowView()
        {
            _view.Show();
            _view.Owner = _mainWindowProvider.Window;
            SetZoomToDefault();
        }

        public ICommand Close
        {
            get { return _close; }
        }

        public ICommand SetZoom
        {
            get { return _setZoom; }
        }

        public void TranslateImage(Vector displacement)
        {
            if (!CanTranslateImage)
                return;

            _origin -= displacement;

            var scaledPixelDimensions = new Vector(
                x: _imageSource.PixelWidth*_zoom,
                y: _imageSource.PixelHeight*_zoom);

            var margin = new Vector(
                x: Math.Max(_view.ImageContainerSize.Width - scaledPixelDimensions.X, 0),
                y: Math.Max(_view.ImageContainerSize.Height - scaledPixelDimensions.Y, 0));

            var bounds = new Vector(
                x: (scaledPixelDimensions.X - _view.ImageContainerSize.Width + margin.X) / 2,
                y: (scaledPixelDimensions.Y - _view.ImageContainerSize.Height + margin.Y) / 2);

            _origin = new Vector(
                x: Math.Min(Math.Max(_origin.X, -bounds.X), bounds.X),
                y: Math.Min(Math.Max(_origin.Y, -bounds.Y), bounds.Y));

            RaisePropertyChanged("TranslateX");
            RaisePropertyChanged("TranslateY");
        }
    }
}