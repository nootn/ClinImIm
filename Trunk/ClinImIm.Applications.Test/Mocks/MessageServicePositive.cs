using System;
using System.Waf.Applications.Services;

namespace ClinImIm.Applications.Test.Mocks
{
    public class MessageServicePositive : IMessageService
    {
        public void ShowError(object owner, string message)
        {
            ShowErrorWasCalled = true;
        }

        public void ShowMessage(object owner, string message)
        {
        }

        public bool? ShowQuestion(object owner, string message)
        {
            return true;
        }

        public void ShowWarning(object owner, string message)
        {
        }

        public bool ShowYesNoQuestion(object owner, string message)
        {
            return true;
        }

        public bool ShowErrorWasCalled { get; private set; }
    }
}
