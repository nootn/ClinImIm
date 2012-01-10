using System.Waf.Applications;

namespace ClinImIm.Applications.Views
{
    public interface IShellView : IView
    {
        void Show();
        void Close();
    }
}
