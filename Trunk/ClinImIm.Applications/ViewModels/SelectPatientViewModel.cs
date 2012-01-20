using System;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using ClinImIm.Applications.Views;
using ClinImIm.Domain;

namespace ClinImIm.Applications.ViewModels
{
    [Export]
    public class SelectPatientViewModel : ViewModel<ISelectPatientView>
    {
        private Patient _model;

        [ImportingConstructor]
        public SelectPatientViewModel(ISelectPatientView view, Patient model)
            : base(view)
        {
            if (view == null) { throw new ArgumentNullException("view"); }

            _model = model ?? new Patient();
        }

        public Patient Model { get { return _model; } set { _model = value; } }

        public void Reset()
        {
            _model.Identifier = string.Empty;
            _model.FullName = string.Empty;
        }
    }
}
