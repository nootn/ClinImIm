using System.ComponentModel.Composition;
using System.Waf.Foundation;

namespace ClinImIm.Domain
{
    [Export]
    public class ImageInspection : Model
    {
        [ImportingConstructor]
        public ImageInspection()
        {
            
        }

        private string _fullPath;
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