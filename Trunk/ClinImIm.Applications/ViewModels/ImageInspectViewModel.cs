using System;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using System.Windows.Input;
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

        [ImportingConstructor]
        public ImageInspectViewModel(IImageInspectView view)
            : base(view)
        {
            if (view == null) throw new ArgumentNullException("view");

            _view = view;
            _close = new DelegateCommand(() => _view.Hide());
        }

        public ImageInspection Model
        {
            get { return _model; }
            set
            {
                if (_model != value)
                {
                    _model = value;
                    RaisePropertyChanged("Model");
                }
            }
        }

        public void ShowView()
        {
            _view.Show();
        }

        public ICommand Close
        {
            get { return _close; }
        }
    }
}