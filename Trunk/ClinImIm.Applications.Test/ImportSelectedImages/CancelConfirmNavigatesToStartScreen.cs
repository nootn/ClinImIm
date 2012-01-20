using System.Waf.Applications.Services;
using ClinImIm.Applications.Test.Mocks;
using ClinImIm.Applications.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.ImportSelectedImages
{
    [TestClass]
    public class CancelConfirmNavigatesToStartScreen : Stories.ImportSelectedImages
    {
        void GivenValidImagesAreSelected()
        {
            TestDataHelper.MakeImagesToImportValid(ApplicationController.CurrentImportImagesViewModel.Model);
        }

        void AndGivenUserIsOnTheImportImagesScreen()
        {
            Assert.IsTrue(ApplicationController.IsOnImportImagesScreen);
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

        protected override IImportImagesView GetImportImagesView()
        {
            return new ImportImagesViewValid();
        }
    }
}
