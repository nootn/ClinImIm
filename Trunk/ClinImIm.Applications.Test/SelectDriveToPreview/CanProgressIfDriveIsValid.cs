using System.Waf.Applications.Services;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.SelectDriveToPreview
{
    [TestClass]
    public class CanProgressIfDriveIsValid : Stories.SelectDriveToPreview
    {
        private MessageServicePositive _messageService = new MessageServicePositive();

        void GivenAValidDriveIsSelected()
        {
            TestDataHelper.MakeDriveValid(ApplicationController.CurrentSelectDriveViewModel.Model);
        }

        void AndGivenUserIsOnTheSelectDriveScreen()
        {
            Assert.IsTrue(ApplicationController.IsOnSelectDriveScreen);
        }

        void ThenUserCanProgressToNextStep()
        {
            TestNavigationHelper.NavigateFromSelectDriveToSelectPatient(ApplicationController);
            Assert.IsTrue(!_messageService.ShowErrorWasCalled);
        }

        protected override IMessageService GetMessageService()
        {
            return _messageService;
        }

        protected override IFileEnumerator GetFileEnumerator()
        {
            return new FileEnumeratorHasMoreThanMaxNumberOfFiles();
        }
    }
}
