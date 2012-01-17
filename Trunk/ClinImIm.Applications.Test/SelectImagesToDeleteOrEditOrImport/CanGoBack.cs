using System.Waf.Applications.Services;
using ClinImIm.Applications.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.SelectImagesToDeleteOrEditOrImport
{
    [TestClass]
    public class CanGoBack : Stories.SelectImagesToDeleteOrEditOrImport
    {
        private readonly MessageServicePositive _messageService = new MessageServicePositive();

        void GivenValidImagesAreSelected()
        {
            TestDataHelper.MakeImageSelectionValid(ApplicationController.CurrentSelectImagesViewModel.Model);
        }

        void AndGivenUserIsOnTheSelectImagesScreen()
        {
            Assert.IsTrue(ApplicationController.IsOnSelectImagesScreen);
        }

        void ThenUserCanProgressToThePreviousStep()
        {
            TestNavigationHelper.NavigateFromSelectImagesToSelectPatient(ApplicationController);
            Assert.IsTrue(!_messageService.ShowErrorWasCalled);
        }

        protected override IMessageService GetMessageService()
        {
            return _messageService;
        }

    }
}
