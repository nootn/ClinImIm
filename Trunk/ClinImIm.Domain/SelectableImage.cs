using System.ComponentModel.DataAnnotations;
using ClinImIm.Domain.Core;
namespace ClinImIm.Domain
{
    public class SelectableImage : ValidationModel, ISelectable
    {
        private bool _isSelected;
        private string _fullPath;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    RaisePropertyChanged("IsSelected");
                }
            }
        }

        [Required(ErrorMessage = "You must select a drive.")]
        public string FullPath
        {
            get { return _fullPath; }
            set
            {
                if (_fullPath != value)
                {
                    _fullPath = value;
                    RaisePropertyChanged("FullPath");
                }
            }
        }
    }
}
