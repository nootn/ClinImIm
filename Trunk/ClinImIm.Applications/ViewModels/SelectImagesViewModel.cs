using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using ClinImIm.Applications.Views;
using ClinImIm.Domain;
using ClinImIm.Domain.Validation;

namespace ClinImIm.Applications.ViewModels
{
    [Export, PartCreationPolicy(CreationPolicy.NonShared)]
    public class SelectImagesViewModel : ViewModel<ISelectImagesView>
    {
        private ImageSelection _model;

        [ImportingConstructor]
        public SelectImagesViewModel(ISelectImagesView view, ImageSelection model)
            : base(view)
        {
            _model = model ?? new ImageSelection();
        }

        public ImageSelection Model { get { return _model; } set { _model = value; } }

        public void Reset()
        {
            _model = new ImageSelection();
        }
    }
}
