using System.Collections.ObjectModel;
using ClinImIm.Domain.Validation;
using System.ComponentModel.Composition;

namespace ClinImIm.Domain
{
    [Export]
    public class ImageSelection : ValidationModel
    {
        private readonly ObservableCollection<SelectableImage> _allImages = new ObservableCollection<SelectableImage>();

        [ImportingConstructor]
        public ImageSelection()
        {
        }

        [CollectionContainsSelectedItem(ErrorMessage = "There must be at least one image selected in order to perform the specified action.")]
        public ObservableCollection<SelectableImage> AllImages
        {
            get
            {
                return _allImages;
            }
        }
    }
}
