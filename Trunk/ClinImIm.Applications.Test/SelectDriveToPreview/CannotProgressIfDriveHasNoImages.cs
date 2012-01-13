using System.Waf.Applications.Services;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.SelectDriveToPreview
{
    [TestClass]
    public class CannotProgressIfDriveHasNoImages : Stories.SelectDriveToPreview
    {
        private MessageServicePositive _messageService = new MessageServicePositive();

        void GivenAValidDriveWithNoImagesIsSelected()
        {
            TestDataHelper.MakeDriveInvalidNoImages(ApplicationController.CurrentSelectDriveViewModel.Model);
        }

        void AndGivenUserIsOnTheSelectDriveScreen()
        {
            Assert.IsTrue(ApplicationController.IsOnSelectDriveScreen);
        }

        void ThenUserCannotProgressToNextStep()
        {
            Assert.IsTrue(ApplicationController.CanNext());
            ApplicationController.Next();
            
            Assert.IsTrue(_messageService.ShowErrorWasCalled);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(ApplicationController.CurrentSelectDriveViewModel.Model.Error));
            Assert.IsTrue(ApplicationController.IsOnSelectDriveScreen);
        }

        protected override IMessageService GetMessageService()
        {
            return _messageService;
        }

        protected override IFileEnumerator GetFileEnumerator()
        {
            return new FileEnumeratorNoFiles();
        }
    }
}
