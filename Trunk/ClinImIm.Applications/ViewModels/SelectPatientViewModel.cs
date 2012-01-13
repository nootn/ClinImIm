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
    [Export, PartCreationPolicy(CreationPolicy.NonShared)]
    public class SelectPatientViewModel : ViewModel<ISelectPatientView>
    {
        private Patient _model;

        [ImportingConstructor]
        public SelectPatientViewModel(ISelectPatientView view, Patient model)
            : base(view)
        {
            _model = model ?? new Patient();
        }

        public Patient Model { get { return _model; } set { _model = value; } }

        public void Reset()
        {
            _model = new Patient();
        }
    }
}
