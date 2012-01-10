using System;
using System.Waf.Applications.Services;

namespace ClinImIm.Applications.Test.Mocks
{
    public class MessageServiceNegative : IMessageService
    {
        public void ShowError(object owner, string message)
        {
        }

        public void ShowMessage(object owner, string message)
        {
        }

        public bool? ShowQuestion(object owner, string message)
        {
            return false;
        }

        public void ShowWarning(object owner, string message)
        {
        }

        public bool ShowYesNoQuestion(object owner, string message)
        {
            return false;
        }
    }
}
