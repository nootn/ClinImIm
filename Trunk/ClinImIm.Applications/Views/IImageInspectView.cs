using System.Waf.Applications;
using System.Windows;

namespace ClinImIm.Applications.Views
{
    public interface IImageInspectView : IView
    {
        void Show();
        void Hide();

        Size ImageContainerSize { get; }
    }
}