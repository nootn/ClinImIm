using ClinImIm.Applications.ViewModels;

namespace ClinImIm.Applications.Controllers
{
    /// <summary>
    /// Responsible for the application lifecycle.
    /// </summary>
    public interface IApplicationController
    {
        void Initialize();

        void Run();

        void Shutdown();

        bool IsRunning { get; }

        ShellViewModel CurrentShellViewModel { get; }

        SelectDriveViewModel CurrentSelectDriveViewModel { get; }

        bool CanCancel();

        void Cancel();

        bool CanBack();

        void Back();

        bool CanNext();

        void Next();
    }
}
