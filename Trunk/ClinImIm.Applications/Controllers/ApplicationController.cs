﻿using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Waf.Applications;
using System.Waf.Applications.Services;
using ClinImIm.Applications.ViewModels;

namespace ClinImIm.Applications.Controllers
{
    /// <summary>
    /// Responsible for the application lifecycle.
    /// </summary>
    [Export(typeof(IApplicationController))]
    internal class ApplicationController : Controller, IApplicationController
    {
        private readonly IMessageService _messageService;
        private readonly ShellViewModel _shellViewModel;
        private readonly DelegateCommand _cancelCommand;
        private readonly DelegateCommand _backCommand;
        private readonly DelegateCommand _nextCommand;
        private readonly SelectDriveViewModel _selectDriveViewModel;
        private readonly SelectPatientViewModel _selectPatientViewModel;
        private readonly SelectImagesViewModel _selectImagesViewModel;
        private readonly ImportImagesViewModel _importImagesViewModel;

        [ImportingConstructor]
        public ApplicationController(IMessageService messageService, ShellViewModel shellViewModel, SelectDriveViewModel selectDriveViewModel, SelectPatientViewModel selectPatientViewModel, SelectImagesViewModel selectImagesViewModel, ImportImagesViewModel importImagesViewModel)
        {
            if (messageService == null) { throw new ArgumentNullException("messageService"); }
            if (shellViewModel == null) { throw new ArgumentNullException("shellViewModel"); }
            if (selectDriveViewModel == null) { throw new ArgumentNullException("selectDriveViewModel"); }
            if (selectImagesViewModel == null) { throw new ArgumentNullException("selectImagesViewModel"); }
            if (importImagesViewModel == null) { throw new ArgumentNullException("importImagesViewModel"); }

            _messageService = messageService;
            _shellViewModel = shellViewModel;
            _selectDriveViewModel = selectDriveViewModel;
            _selectPatientViewModel = selectPatientViewModel;
            _selectImagesViewModel = selectImagesViewModel;
            _importImagesViewModel = importImagesViewModel;

            _cancelCommand = new DelegateCommand(Cancel, CanCancel);
            _backCommand = new DelegateCommand(Back, CanBack);
            _nextCommand = new DelegateCommand(Next, CanNext);

            AddWeakEventListener(_shellViewModel, ShellViewModelPropertyChanged);
        }

        public bool IsRunning { get; private set; }

        public ShellViewModel CurrentShellViewModel { get { return _shellViewModel; } }

        public SelectDriveViewModel CurrentSelectDriveViewModel { get { return _selectDriveViewModel; } }

        public SelectPatientViewModel CurrentSelectPatientViewModel { get { return _selectPatientViewModel; } }

        public SelectImagesViewModel CurrentSelectImagesViewModel { get { return _selectImagesViewModel; } }

        public ImportImagesViewModel CurrentImportImagesViewModel { get { return _importImagesViewModel; } }

        public void Initialize()
        {
            _shellViewModel.ExitCommand = new DelegateCommand(Close);
            _shellViewModel.CancelCommand = _cancelCommand;
            _shellViewModel.BackCommand = _backCommand;
            _shellViewModel.NextCommand = _nextCommand;
        }

        public void Run()
        {
            _shellViewModel.Show();
            _shellViewModel.ContentView = _selectDriveViewModel.View;
            IsRunning = true;
        }

        public void Shutdown()
        {
            IsRunning = false;
        }

        private void Close()
        {
            _shellViewModel.Close();
        }

        public bool CanCancel()
        {
            return true;
        }

        public void Cancel()
        {
            if (_messageService.ShowYesNoQuestion("Are you sure you want to lose all progress and start again?"))
            {
                ResetState();
            }
        }



        public bool CanBack()
        {
            return !IsOnSelectDriveScreen;
        }

        public void Back()
        {
            if (IsOnSelectPatientScreen)
            {
                _shellViewModel.ContentView = _selectDriveViewModel.View;
            }
            else if (IsOnSelectImagesScreen)
            {
                _shellViewModel.ContentView = _selectPatientViewModel.View;
            }
            else if (IsOnImportImagesScreen)
            {
                _shellViewModel.ContentView = _selectImagesViewModel.View;
            }

            _shellViewModel.IsLastPage = false;
            UpdateCommandsState();
        }

        public bool CanNext()
        {
            //TODO: discuss this.  Currently you can always go next (unless you are on the last screen?) and if you have validation errors it will show a msg box.
            //      Reasons:
            //              1. The Select Drive screen can't currently validate that a drive has at least one image on the UI.. this may be able to be fixed
            //              2. The errors do not currently show anywhere on the screen - if a field has an error the text is not shown, so the user could click next to see the error


            // ReSharper disable UnusedVariable
            var isUiValid = _shellViewModel.IsValid;
            // ReSharper restore UnusedVariable

            //we still need to call "IsValid" to get the UI validation to fire, even though we don't care about the result, so just return true
            return true;
        }

        public void Next()
        {
            var errorMessages = string.Empty;

            if (IsOnSelectDriveScreen)
            {
                errorMessages = _selectDriveViewModel.Model.Error;
                if (string.IsNullOrWhiteSpace(errorMessages))
                {
                    _selectImagesViewModel.CommenceLoadingImages(_selectDriveViewModel.Model.SelectedDrivePath);
                    _shellViewModel.ContentView = _selectPatientViewModel.View;
                }
            }
            else if (IsOnSelectPatientScreen)
            {
                errorMessages = _selectPatientViewModel.Model.Error;
                if (string.IsNullOrWhiteSpace(errorMessages))
                {
                    _shellViewModel.ContentView = _selectImagesViewModel.View;
                }
            }
            else if (IsOnSelectImagesScreen)
            {
                errorMessages = _selectImagesViewModel.Model.Error;
                if (string.IsNullOrWhiteSpace(errorMessages))
                {
                    _importImagesViewModel.Model.Images.Clear();
                    foreach (var currImage in _selectImagesViewModel.Model.AllImages.Where(i => i.IsSelected))
                    {
                        _importImagesViewModel.Model.Images.Add(currImage.FullPath);
                    }
                    _importImagesViewModel.Model.PatientImagesAreOf = _selectPatientViewModel.Model;
                    _shellViewModel.ContentView = _importImagesViewModel.View;
                }
            }
            else if (IsOnImportImagesScreen)
            {
                errorMessages = _importImagesViewModel.Model.Error;
                if (string.IsNullOrWhiteSpace(errorMessages))
                {
                    var importErrors = _importImagesViewModel.TryImport();

                    if (importErrors != null && importErrors.Any())
                    {
                        _messageService.ShowError(string.Format("There was a problem importing images: {0}{0}{1}", Environment.NewLine, string.Join(Environment.NewLine, importErrors)));
                    }
                    else
                    {
                        _messageService.ShowMessage("Success! The images have been imported.");
                        ResetState();
                    }
                }
            }
            else
            {
                throw new ApplicationException(string.Format("Unknown screen to navigate to!  Current view is: {0}", _shellViewModel.ContentView ?? "[null]"));
            }

            if (!string.IsNullOrWhiteSpace(errorMessages))
            {
                _messageService.ShowError(string.Format("Please fix the following validation errors then try again: {0}{0}{1}", Environment.NewLine, errorMessages));
            }

            UpdateCommandsState();
        }

        private void UpdateCommandsState()
        {
            _cancelCommand.RaiseCanExecuteChanged();
            _backCommand.RaiseCanExecuteChanged();
            _nextCommand.RaiseCanExecuteChanged();
        }

        private void ShellViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsValid") { UpdateCommandsState(); }
        }


        public bool IsOnSelectDriveScreen
        {
            get { return _shellViewModel.ContentView == _selectDriveViewModel.View; }
        }

        public bool IsOnSelectPatientScreen
        {
            get { return _shellViewModel.ContentView == _selectPatientViewModel.View; }
        }

        public bool IsOnSelectImagesScreen
        {
            get { return _shellViewModel.ContentView == _selectImagesViewModel.View; }
        }

        public bool IsOnImportImagesScreen
        {
            get { return _shellViewModel.ContentView == _importImagesViewModel.View; }
        }

        private void ResetState()
        {
            _selectDriveViewModel.Reset();
            _selectPatientViewModel.Reset();
            _selectImagesViewModel.Reset();
            _importImagesViewModel.Reset();

            _shellViewModel.ContentView = _selectDriveViewModel.View;
            UpdateCommandsState();
        }
    }
}
