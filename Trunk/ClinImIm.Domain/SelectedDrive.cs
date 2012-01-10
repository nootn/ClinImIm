using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using ClinImIm.Domain.Validation;

namespace ClinImIm.Domain
{
    [Export]
    public class SelectedDrive : ValidationModel
    {
        private string _selectedDrivePath;
        private readonly ObservableCollection<string> _photoFiles = new ObservableCollection<string>();

        [ImportingConstructor]
        public SelectedDrive()
        {
        }

        [Required(ErrorMessage = "You must select a drive.")]
        public string SelectedDrivePath
        {
            get { return _selectedDrivePath; }
            set
            {
                if (_selectedDrivePath != value)
                {
                    _selectedDrivePath = value;
                    RaisePropertyChanged("SelectedDrivePath");
                }
            }
        }

        [CollectionContainsItems(ErrorMessage = "There must be at least one image contained in the selected drive - please ensure you have selected the correct drive.")]
        public ObservableCollection<string> PhotoFiles
        {
            get
            {
                return _photoFiles;
            }
        }
    }
}
