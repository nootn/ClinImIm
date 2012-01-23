using System.Waf.Applications.Services;
using ClinImIm.Applications.Test.Mocks;
using ClinImIm.Applications.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.ImportSelectedImages
{
    [TestClass]
    public class CanFinishIfImagesToImportAreValidAndImagesCanBeImported : Stories.ImportSelectedImages
    {
        private readonly MessageServicePositive _messageService = new MessageServicePositive();
        private readonly ImportImagesViewValid _importImagesViewValid = new ImportImagesViewValid();

        void GivenImagesAreNotSelectedThusInvalid()
        {
            TestDataHelper.MakeImagesToImportValid(ApplicationController.CurrentImportImagesViewModel.Model);
        }

        void AndGivenUserIsOnTheImportImagesScreen()
        {
            Assert.IsTrue(ApplicationController.IsOnImportImagesScreen);
        }

        void ThenUserIsFinishedAndBackOnStartScreen()
        {
            Assert.IsTrue(ApplicationController.CanNext());
            ApplicationController.Next();

            Assert.IsTrue(!_messageService.ShowErrorWasCalled);
            Assert.IsTrue(_messageService.ShowMessageWasCalled);
            Assert.IsTrue(_importImagesViewValid.TryImportHasBeenCalled);
            TestNavigationHelper.EnsureIsOnStartScreen(ApplicationController);
        }

        void AndThenAllScreensAreCleared()
        {
            TestNavigationHelper.EnsureAllScreensAreCleared(ApplicationController);
        }

        protected override IMessageService GetMessageService()
        {
            return _messageService;
        }

        protected override IImportImagesView GetImportImagesView()
        {
            return _importImagesViewValid;
        }
    }
}
