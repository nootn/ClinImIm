using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.Views;
using ClinImIm.Domain;

namespace ClinImIm.Applications.ViewModels
{
    [Export]
    public class ImportImagesViewModel : ViewModel<IImportImagesView>
    {
        private readonly ObservableCollection<string> _warnings = new ObservableCollection<string>();
        private readonly ObservableCollection<string> _importErrorMessages = new ObservableCollection<string>();
        private readonly IDispatcher _dispatcher;
        private ImagesToImport _model;
        private IImportImagesView _view;

        [ImportingConstructor]
        public ImportImagesViewModel(IDispatcher dispatcher, IImportImagesView view, ImagesToImport model)
            : base(view)
        {
            if (dispatcher == null) { throw new ArgumentNullException("dispatcher"); }
            if (view == null) { throw new ArgumentNullException("view"); }
            if (model == null) { throw new ArgumentNullException("model"); }

            _dispatcher = dispatcher;
            _model = model;
            _view = view;
        }

        public ImagesToImport Model { get { return _model; } }

        public void Reset()
        {
            _warnings.Clear();
            _model.Images.Clear();
            _model.PatientImagesAreOf = null;
        }

        public IEnumerable<string> TryImport()
        {
            return _view.TryImport();
        }

        public ObservableCollection<string> Warnings
        {
            get
            {
                return _warnings;
            }
        }

        public ObservableCollection<string> ImportErrorMessages
        {
            get
            {
                return _importErrorMessages;
            }
        }
    }
}
