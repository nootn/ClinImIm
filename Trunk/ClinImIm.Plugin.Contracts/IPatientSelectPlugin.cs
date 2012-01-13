using System.Windows.Controls;
using ClinImIm.Domain;

namespace ClinImIm.Plugin.Contracts
{
    public interface IPatientSelectPlugin
    {
        UserControl GetUserControl();

        Patient Model { get; }
    }
}
