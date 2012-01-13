using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
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

        [ImportingConstructor]
        public ApplicationController(IMessageService messageService, ShellViewModel shellViewModel, SelectDriveViewModel selectDriveViewModel, SelectPatientViewModel selectPatientViewModel)
        {
            if (messageService == null) { throw new ArgumentNullException("messageService"); }
            if (shellViewModel == null) { throw new ArgumentNullException("shellViewModel"); }
            if (selectDriveViewModel == null) { throw new ArgumentNullException("selectDriveViewModel"); }

            _messageService = messageService;
            _shellViewModel = shellViewModel;
            _selectDriveViewModel = selectDriveViewModel;
            _selectPatientViewModel = selectPatientViewModel;

            _cancelCommand = new DelegateCommand(Cancel, CanCancel);
            _backCommand = new DelegateCommand(Back, CanBack);
            _nextCommand = new DelegateCommand(Next, CanNext);

            AddWeakEventListener(_shellViewModel, ShellViewModelPropertyChanged);
        }

        public bool IsRunning { get; private set; }

        public ShellViewModel CurrentShellViewModel { get { return _shellViewModel; } }

        public SelectDriveViewModel CurrentSelectDriveViewModel { get { return _selectDriveViewModel; } }

        public SelectPatientViewModel CurrentSelectPatientViewModel { get { return _selectPatientViewModel; } }

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
                _selectDriveViewModel.Reset();
                _selectPatientViewModel.Reset();

                _shellViewModel.ContentView = _selectDriveViewModel.View;
                UpdateCommandsState();
            }
        }

        public bool CanBack()
        {
            return _shellViewModel.ContentView != _selectDriveViewModel.View;
        }

        public void Back()
        {
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
                    _shellViewModel.ContentView = _selectPatientViewModel.View;
                }
            }
            else if (IsOnSelectPatientScreen)
            {
                errorMessages = _selectPatientViewModel.Model.Error;
                if (string.IsNullOrWhiteSpace(errorMessages))
                {
                    throw new NotImplementedException("Next screen is not built yet!");
                }
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
    }
}
