using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using ClinImIm.Domain.Validation;

namespace ClinImIm.Domain
{
    [Export]
    public class ImagesToImport : ValidationModel
    {
        private readonly ObservableCollection<string> _images = new ObservableCollection<string>();
        private Patient _patientImagesAreOf;

        [ImportingConstructor]
        public ImagesToImport()
        {
        }

        [CollectionContainsItems(ErrorMessage = "There must be at least one image selected to import.")]
        public ObservableCollection<string> Images
        {
            get
            {
                return _images;
            }
        }

        public Patient PatientImagesAreOf
        {
            get { return _patientImagesAreOf; }
            set
            {
                if (_patientImagesAreOf != value)
                {
                    _patientImagesAreOf = value;
                    RaisePropertyChanged("PatientImagesAreOf");
                }
            }
        }

    }
}
