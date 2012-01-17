using System.Waf.Applications.Services;
using ClinImIm.Applications.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.SelectImagesToDeleteOrEditOrImport
{
    [TestClass]
    public class CannotProgressIfSelectedImagesAreInvalid : Stories.SelectImagesToDeleteOrEditOrImport
    {
        private readonly MessageServicePositive _messageService = new MessageServicePositive();

        void GivenImagesAreNotSelectedThusInvalid()
        {
            TestDataHelper.MakeImageSelectionInvalidNoSelectedImages(ApplicationController.CurrentSelectImagesViewModel.Model);
        }

        void AndGivenUserIsOnTheSelectImagesScreen()
        {
            Assert.IsTrue(ApplicationController.IsOnSelectImagesScreen);
        }

        void ThenUserCannotProgressToNextStep()
        {
            Assert.IsTrue(ApplicationController.CanNext());
            ApplicationController.Next();

            Assert.IsTrue(_messageService.ShowErrorWasCalled);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(ApplicationController.CurrentSelectImagesViewModel.Model.Error));
            Assert.IsTrue(ApplicationController.IsOnSelectImagesScreen);
        }

        protected override IMessageService GetMessageService()
        {
            return _messageService;
        }

    }
}
