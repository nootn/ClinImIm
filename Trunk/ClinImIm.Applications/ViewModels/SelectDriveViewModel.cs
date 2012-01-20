using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Waf.Applications;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.Views;
using ClinImIm.Domain;

namespace ClinImIm.Applications.ViewModels
{
    [Export]
    public class SelectDriveViewModel : ViewModel<ISelectDriveView>
    {
        //TODO: should be configurable and UI should use same config value
        public const int MaximumNumberOfFiles = 10;

        private readonly ObservableCollection<string> _warnings = new ObservableCollection<string>();
        private readonly IDispatcher _dispatcher;
        private SelectedDrive _model;
        private readonly IFileEnumerator _fileEnumerator;

        [ImportingConstructor]
        public SelectDriveViewModel(IDispatcher dispatcher, ISelectDriveView view, SelectedDrive model, IFileEnumerator fileEnumerator)
            : base(view)
        {
            if (dispatcher == null) { throw new ArgumentNullException("dispatcher"); }
            if (view == null) { throw new ArgumentNullException("view"); }
            if (fileEnumerator == null) { throw new ArgumentNullException("fileEnumerator"); }

            _dispatcher = dispatcher;
            _model = model ?? new SelectedDrive();
            _fileEnumerator = fileEnumerator;

            Model.PropertyChanged += ModelPropertyChanged;
        }

        void ModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedDrivePath")
            {
                if (_dispatcher.AllowMultiThreading)
                {
                    Task.Factory.StartNew(() => LoadFiles(Model.SelectedDrivePath));
                }
                else
                {
                    LoadFiles(Model.SelectedDrivePath);
                }
            }
        }

        public SelectedDrive Model { get { return _model; } }

        public DriveInfo[] AllDrives
        {
            get { return DriveInfo.GetDrives(); }
        }

        public ObservableCollection<string> Warnings
        {
            get
            {
                return _warnings;
            }
        }

        public void Reset()
        {
            _model.SelectedDrivePath = string.Empty;
            _model.PhotoFiles.Clear();
            _warnings.Clear();
        }

        private void LoadFiles(string path)
        {
            _dispatcher.Dispatch(() => Model.PhotoFiles.Clear());
            _dispatcher.Dispatch(() => _warnings.Clear());

            if (!string.IsNullOrWhiteSpace(path))
            {
                AddImagesFromDirectory(new DirectoryInfo(path));
            }
        }

        private void AddImagesFromDirectory(DirectoryInfo dir)
        {
            try
            {
                foreach (var currFile in _fileEnumerator.EnumerateFiles(dir))
                {
                    var fileName = currFile.FullName;
                    if (IoHelper.IsValidImageFile(fileName))
                    {
                        _dispatcher.Dispatch(() => Model.PhotoFiles.Add(fileName));
                        if (HasEnoughFiles)
                        {
                            return;
                        }
                    }
                }

                if (!HasEnoughFiles)
                {
                    foreach (var currSubDir in _fileEnumerator.EnumerateDirectories(dir))
                    {
                        AddImagesFromDirectory(currSubDir);
                        if (HasEnoughFiles)
                        {
                            return;
                        }    
                    }
                    
                }
                
            }
            catch (Exception ex)
            {
                _warnings.Add(ex.Message);
            }
        }

        private bool HasEnoughFiles
        {
            get { return Model.PhotoFiles.Count() >= MaximumNumberOfFiles; }
        }
    }
}
