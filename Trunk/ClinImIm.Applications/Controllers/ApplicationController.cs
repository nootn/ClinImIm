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

        [ImportingConstructor]
        public ApplicationController(IMessageService messageService, ShellViewModel shellViewModel, SelectDriveViewModel selectDriveViewModel)
        {
            if (messageService == null) { throw new ArgumentNullException("messageService"); }
            if (shellViewModel == null) { throw new ArgumentNullException("shellViewModel"); }
            if (selectDriveViewModel == null) { throw new ArgumentNullException("selectDriveViewModel"); }

            _messageService = messageService;
            _shellViewModel = shellViewModel;
            _selectDriveViewModel = selectDriveViewModel;

            _cancelCommand = new DelegateCommand(Cancel, CanCancel);
            _backCommand = new DelegateCommand(Back, CanBack);
            _nextCommand = new DelegateCommand(Next, CanNext);

            AddWeakEventListener(_shellViewModel, ShellViewModelPropertyChanged);
        }

        public bool IsRunning { get; private set; }

        public ShellViewModel CurrentShellViewModel { get { return _shellViewModel; } }

        public SelectDriveViewModel CurrentSelectDriveViewModel { get { return _selectDriveViewModel; } }

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
            return _shellViewModel.IsValid;
        }

        public void Next()
        {
            if (_shellViewModel.ContentView == _selectDriveViewModel.View)
            {
                var errorMessage = _selectDriveViewModel.Model.Error;
                if (string.IsNullOrWhiteSpace(errorMessage))
                {
                    //todo: load next screen
                    throw new ApplicationException("Not ready to navigate to next screen yet because it's not built!");
                }
                _messageService.ShowError(string.Format("Please fix the following validation errors then try again: {0}{0}{1}", Environment.NewLine, errorMessage));
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

    }
}
