using System.Windows;

namespace ClinImIm.Applications.Core
{
    public interface IMainWindowProvider
    {
        Window Window { get; }
    }
}