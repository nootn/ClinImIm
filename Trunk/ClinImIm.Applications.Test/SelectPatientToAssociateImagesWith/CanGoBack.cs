using System.Waf.Applications.Services;
using ClinImIm.Applications.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.SelectPatientToAssociateImagesWith
{
    [TestClass]
    public class CanGoBack : Stories.SelectPatientToAssociateImagesWith
    {
        private MessageServicePositive _messageService = new MessageServicePositive();

        void GivenAValidPatientIsSelected()
        {
            TestDataHelper.MakePatientValid(ApplicationController.CurrentSelectPatientViewModel.Model);
        }

        void AndGivenUserIsOnTheSelectPatientScreen()
        {
            Assert.IsTrue(ApplicationController.IsOnSelectPatientScreen);
        }

        void ThenUserCanProgressToThePreviousStep()
        {
            TestNavigationHelper.NavigateFromSelectPatientToSelectDrive(ApplicationController);
            Assert.IsTrue(!_messageService.ShowErrorWasCalled);
        }

        protected override IMessageService GetMessageService()
        {
            return _messageService;
        }

    }
}
