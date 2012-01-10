using System.ComponentModel.Composition;
using System.Waf.Applications;
using System.Windows.Input;
using ClinImIm.Applications.Views;

namespace ClinImIm.Applications.ViewModels
{
    [Export]
    public class ShellViewModel : ViewModel<IShellView>
    {
        private ICommand _exitCommand;
        private ICommand _cancelCommand;
        private ICommand _backCommand;
        private ICommand _nextCommand;
        private object _contentView;
        private bool _isValid = true;
        private bool _isLastPage;

        [ImportingConstructor]
        public ShellViewModel(IShellView view)
            : base(view)
        {
        }


        public string Title { get { return ApplicationInfo.ProductName; } }

        public ICommand ExitCommand
        {
            get { return _exitCommand; }
            set
            {
                if (_exitCommand != value)
                {
                    _exitCommand = value;
                    RaisePropertyChanged("ExitCommand");
                }
            }
        }

        public ICommand CancelCommand
        {
            get { return _cancelCommand; }
            set
            {
                if (_cancelCommand != value)
                {
                    _cancelCommand = value;
                    RaisePropertyChanged("CancelCommand");
                }
            }
        }

        public ICommand BackCommand
        {
            get { return _backCommand; }
            set
            {
                if (_backCommand != value)
                {
                    _backCommand = value;
                    RaisePropertyChanged("BackCommand");
                }
            }
        }

        public ICommand NextCommand
        {
            get { return _nextCommand; }
            set
            {
                if (_nextCommand != value)
                {
                    _nextCommand = value;
                    RaisePropertyChanged("NextCommand");
                }
            }
        }

        public object ContentView
        {
            get { return _contentView; }
            set
            {
                if (_contentView != value)
                {
                    _contentView = value;
                    RaisePropertyChanged("ContentView");
                }
            }
        }

        public bool IsValid
        {
            get { return _isValid; }
            set
            {
                if (_isValid != value)
                {
                    _isValid = value;
                    RaisePropertyChanged("IsValid");
                }
            }
        }

        public bool IsLastPage
        {
            get { return _isLastPage; }
            set
            {
                if (_isLastPage != value)
                {
                    _isLastPage = value;
                    RaisePropertyChanged("IsLastPage");
                }
            }
        }

        public void Show()
        {
            ViewCore.Show();
        }

        public void Close()
        {
            ViewCore.Close();
        }
    }
}
