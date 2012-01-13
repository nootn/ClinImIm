using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ClinImIm.Plugin.Contracts
{
    public interface IPatientSelectPlugin
    {
        UserControl GetUserControl();
    }
}
