using System.Waf.Applications.Services;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.SelectDriveToPreview
{
    [TestClass]
    public class CannotProgressIfNoDriveIsSelected : Stories.SelectDriveToPreview
    {
        void GivenADriveIsNotSelected()
        {
            TestHelper.MakeDriveInvalidNoDriveAndNoImages(ApplicationController.CurrentSelectDriveViewModel.Model);
        }

        void ThenUserCannotProgressToNextStep()
        {
            //Assert.IsTrue(!ApplicationController.CanNext()); //TODO: this does not work due to the way the UI validates.. needs attention
            ApplicationController.Next();

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
