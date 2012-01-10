using System.Waf.Applications.Services;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.SelectDriveToPreview
{
    [TestClass]
    public class CannotProgressIfDriveHasNoImages : Stories.SelectDriveToPreview
    {
        void GivenAValidDriveWithNoImagesIsSelected()
        {
            TestHelper.MakeDriveInvalidNoImages(ApplicationController.CurrentSelectDriveViewModel.Model);
        }

        void ThenUserCannotProgressToNextStep()
        {
            //Assert.IsTrue(!ApplicationController.CanNext()); - TODO: this does not work due to the way the UI validates.. needs attention
            ApplicationController.Next();
            //TODO: should see if we can determine that "ShowError" was called on the IMessageService

            Assert.IsTrue(!string.IsNullOrWhiteSpace(ApplicationController.CurrentSelectDriveViewModel.Model.Error));
            Assert.IsTrue(ApplicationController.CurrentShellViewModel.ContentView == ApplicationController.CurrentSelectDriveViewModel.View);
        }

        protected override IMessageService GetMessageService()
        {
            return new MessageServicePositive();
        }

        protected override IFileEnumerator GetFileEnumerator()
        {
            return new FileEnumeratorNoFiles();
        }
    }
}
