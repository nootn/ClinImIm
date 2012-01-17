using System.Waf.Applications.Services;
using ClinImIm.Applications.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.SelectImagesToDeleteOrEditOrImport
{
    [TestClass]
    public class CancelConfirmNavigatesToStartScreen : Stories.SelectImagesToDeleteOrEditOrImport
    {
        void GivenValidImagesAreSelected()
        {
            TestDataHelper.MakeImageSelectionValid(ApplicationController.CurrentSelectImagesViewModel.Model);
        }

        void AndGivenUserIsOnTheSelectImagesScreen()
        {
            Assert.IsTrue(ApplicationController.IsOnSelectImagesScreen);
        }

        void WhenUserClicksCancelAndConfirms()
        {
            Assert.IsTrue(ApplicationController.CanCancel());
            ApplicationController.Cancel();
        }

        void ThenAllScreensAreCleared()
        {
            TestNavigationHelper.EnsureAllScreensAreCleared(ApplicationController);
        }

        void AndThenTheUserIsReturnedToTheStartScreenOfTheApplication()
        {
            TestNavigationHelper.EnsureIsOnStartScreen(ApplicationController);
        }

        protected override IMessageService GetMessageService()
        {
            return new MessageServicePositive();
        }
    }
}
