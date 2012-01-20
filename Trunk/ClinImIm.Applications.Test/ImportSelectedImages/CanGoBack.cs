using System.Waf.Applications.Services;
using ClinImIm.Applications.Test.Mocks;
using ClinImIm.Applications.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.ImportSelectedImages
{
    [TestClass]
    public class CanGoBack : Stories.ImportSelectedImages
    {
        private readonly MessageServicePositive _messageService = new MessageServicePositive();

        void GivenValidImagesAreSelected()
        {
            TestDataHelper.MakeImagesToImportValid(ApplicationController.CurrentImportImagesViewModel.Model);
        }

        void AndGivenUserIsOnTheImportImagesScreen()
        {
            Assert.IsTrue(ApplicationController.IsOnImportImagesScreen);
        }

        void ThenUserCanProgressToThePreviousStep()
        {
            TestNavigationHelper.NavigateFromImportImagesToSelectImages(ApplicationController);
            Assert.IsTrue(!_messageService.ShowErrorWasCalled);
        }

        protected override IMessageService GetMessageService()
        {
            return _messageService;
        }

        protected override IImportImagesView GetImportImagesView()
        {
            return new ImportImagesViewValid();
        }
    }
}
