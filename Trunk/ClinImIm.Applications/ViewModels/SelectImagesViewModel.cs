using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Threading.Tasks;
using System.Waf.Applications;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.Views;
using ClinImIm.Domain;

namespace ClinImIm.Applications.ViewModels
{
    [Export, PartCreationPolicy(CreationPolicy.NonShared)]
    public class SelectImagesViewModel : ViewModel<ISelectImagesView>
    {
        private readonly ObservableCollection<string> _warnings = new ObservableCollection<string>();
        private readonly IDispatcher _dispatcher;
        private ImageSelection _model;
        private readonly IFileEnumerator _fileEnumerator;
        private bool _isCurrentlyLoading;
        
        [ImportingConstructor]
        public SelectImagesViewModel(IDispatcher dispatcher, ISelectImagesView view, ImageSelection model, IFileEnumerator fileEnumerator)
            : base(view)
        {
            if (dispatcher == null) { throw new ArgumentNullException("dispatcher"); }
            if (view == null) { throw new ArgumentNullException("view"); }
            if (fileEnumerator == null) { throw new ArgumentNullException("fileEnumerator"); }

            _dispatcher = dispatcher;
            _model = model ?? new ImageSelection();
            _fileEnumerator = fileEnumerator;
        }

        public ImageSelection Model { get { return _model; } set { _model = value; } }

        public void Reset()
        {
            _isCurrentlyLoading = false;
            _model.AllImages.Clear();
            _warnings.Clear();
        }

        public ObservableCollection<string> Warnings
        {
            get
            {
                return _warnings;
            }
        }

        public void CommenceLoadingImages(string drivePath)
        {
            _isCurrentlyLoading = true;
            if (_dispatcher.AllowMultiThreading)
            {
                Task.Factory.StartNew(() => LoadFiles(drivePath));
            }
            else
            {
                LoadFiles(drivePath);
            }
        }

        private void LoadFiles(string path)
        {
            _dispatcher.Dispatch(() => Model.AllImages.Clear());
            _dispatcher.Dispatch(() => _warnings.Clear());

            if (!string.IsNullOrWhiteSpace(path))
            {
                AddImagesFromDirectory(new DirectoryInfo(path));
            }

            _isCurrentlyLoading = false;
        }

        private void AddImagesFromDirectory(DirectoryInfo dir)
        {
            if (!_isCurrentlyLoading)
            {
                return;
            }

            try
            {
                foreach (var currFile in _fileEnumerator.EnumerateFiles(dir))
                {
                    if (!_isCurrentlyLoading)
                    {
                        return;
                    }

                    var fileName = currFile.FullName;
                    if (IoHelper.IsValidImageFile(fileName))
                    {
                        _dispatcher.Dispatch(() => Model.AllImages.Add(new SelectableImage{FullPath = fileName, IsSelected = false}));
                    }
                }

                foreach (var currSubDir in _fileEnumerator.EnumerateDirectories(dir))
                {
                    AddImagesFromDirectory(currSubDir);
                }

            }
            catch (Exception ex)
            {
                _warnings.Add(ex.Message);
            }
        }
    }
}
