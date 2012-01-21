using System.ComponentModel.Composition;
using System.Windows;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.Views;
using System;
using System.Collections.Generic;
using ClinImIm.Applications.ViewModels;
using System.Windows.Controls;
using System.Waf.Applications;
using System.Linq;

namespace ClinImIm.Presentation.Views
{
    [Export(typeof(IShellView)), Export(typeof(IMainWindowProvider))]
    public partial class ShellWindow : Window, IShellView, IMainWindowProvider
    {
        private readonly Lazy<ShellViewModel> _viewModel;
        private readonly List<Tuple<object, ValidationError>> errors = new List<Tuple<object, ValidationError>>();

        public ShellWindow()
        {
            InitializeComponent();

            _viewModel = new Lazy<ShellViewModel>(() => ViewHelper.GetViewModel<ShellViewModel>(this));
            Validation.AddErrorHandler(this, ErrorChangedHandler);
        }

        private ShellViewModel ViewModel { get { return _viewModel.Value; } }


        private void ErrorChangedHandler(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                errors.Add(new Tuple<object, ValidationError>(e.OriginalSource, e.Error));

                if (e.OriginalSource is FrameworkElement)
                {
                    ((FrameworkElement)e.OriginalSource).Unloaded += ValidationSourceUnloaded;
                }
                else if (e.OriginalSource is FrameworkContentElement)
                {
                    ((FrameworkContentElement)e.OriginalSource).Unloaded += ValidationSourceUnloaded;
                }
            }
            else
            {
                Tuple<object, ValidationError> error = errors.FirstOrDefault(err => err.Item1 == e.OriginalSource && err.Item2 == e.Error);
                if (error != null) { errors.Remove(error); }
            }

            ViewModel.IsValid = !errors.Any();
        }

        private void ValidationSourceUnloaded(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement)
            {
                ((FrameworkElement)sender).Unloaded -= ValidationSourceUnloaded;
            }
            else
            {
                ((FrameworkContentElement)sender).Unloaded -= ValidationSourceUnloaded;
            }

            foreach (Tuple<object, ValidationError> error in errors.Where(err => err.Item1 == sender).ToArray())
            {
                errors.Remove(error);
            }

            ViewModel.IsValid = !errors.Any();
        }

        public Window Window
        {
            get { return this; }
        }
    }
}
