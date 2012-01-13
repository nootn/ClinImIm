using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;

namespace ClinImIm.Domain
{
    [Export]
    public class Patient : ValidationModel
    {
        private string _identifier;
        private string _fullName;

        [ImportingConstructor]
        public Patient()
        {
        }

        [Required(ErrorMessage = "Patient must have an identifier")]
        public string Identifier
        {
            get { return _identifier; }
            set
            {
                if (_identifier != value)
                {
                    _identifier = value;
                    RaisePropertyChanged("Identifier");
                }
            }
        }

        [Required(ErrorMessage = "Patient must have a full name")]
        public string FullName
        {
            get { return _fullName; }
            set
            {
                if (_fullName != value)
                {
                    _fullName = value;
                    RaisePropertyChanged("FullName");
                }
            }
        }
    }
}
