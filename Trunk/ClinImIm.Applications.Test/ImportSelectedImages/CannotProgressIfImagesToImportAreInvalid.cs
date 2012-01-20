using System.Waf.Applications.Services;
using ClinImIm.Applications.Test.Mocks;
using ClinImIm.Applications.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.ImportSelectedImages
{
    [TestClass]
    public class CannotProgressIfImagesToImportAreInvalid : Stories.ImportSelectedImages
    {
        private readonly MessageServicePositive _messageService = new MessageServicePositive();
        private readonly ImportImagesViewValid _importImagesViewValid = new ImportImagesViewValid();

        void GivenImagesAreNotSelectedThusInvalid()
        {
            TestDataHelper.MakeImagesToImportInvalidNoImages(ApplicationController.CurrentImportImagesViewModel.Model);
        }

        void AndGivenUserIsOnTheImportImagesScreen()
        {
            Assert.IsTrue(ApplicationController.IsOnImportImagesScreen);
        }

        void ThenUserCannotProgressToNextStep()
        {
            Assert.IsTrue(ApplicationController.CanNext());
            ApplicationController.Next();

            Assert.IsTrue(_messageService.ShowErrorWasCalled);
            Assert.IsTrue(!_importImagesViewValid.TryImportHasBeenCalled);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(ApplicationController.CurrentImportImagesViewModel.Model.Error));
            Assert.IsTrue(ApplicationController.IsOnImportImagesScreen);
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
