using System.Waf.Applications.Services;
using ClinImIm.Applications.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.SelectPatientToAssociateImagesWith
{
    [TestClass]
    public class CannotProgressIfPatientIsInvalid : Stories.SelectPatientToAssociateImagesWith
    {
        private readonly MessageServicePositive _messageService = new MessageServicePositive();

        void GivenAnInvalidPatientIsSelected()
        {
            TestDataHelper.MakePatientInValid(ApplicationController.CurrentSelectPatientViewModel.Model);
        }

        void AndGivenUserIsOnTheSelectPatientScreen()
        {
            Assert.IsTrue(ApplicationController.IsOnSelectPatientScreen);
        }

        void ThenUserCannotProgressToNextStep()
        {
            Assert.IsTrue(ApplicationController.CanNext());
            ApplicationController.Next();

            Assert.IsTrue(_messageService.ShowErrorWasCalled);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(ApplicationController.CurrentSelectPatientViewModel.Model.Error));
            Assert.IsTrue(ApplicationController.IsOnSelectPatientScreen);
        }

        protected override IMessageService GetMessageService()
        {
            return _messageService;
        }

    }
}
