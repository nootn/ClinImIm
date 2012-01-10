using System.Waf.Applications.Services;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.SelectDriveToPreview
{
    [TestClass]
    public class CancelConfirmClearsForm : Stories.SelectDriveToPreview
    {
        void GivenAValidDriveIsSelected()
        {
            TestHelper.MakeDriveValid(ApplicationController.CurrentSelectDriveViewModel.Model);
        }

        void WhenUserClicksCancelAndConfirms()
        {
            Assert.IsTrue(ApplicationController.CanCancel());
            ApplicationController.Cancel();
        }

        void ThenAllFormFieldsAreCleared()
        {
            Assert.IsTrue(string.IsNullOrWhiteSpace(ApplicationController.CurrentSelectDriveViewModel.Model.SelectedDrivePath));
            Assert.IsTrue(ApplicationController.CurrentSelectDriveViewModel.Model.PhotoFiles.Count == 0);
        }

        void AndThenTheFormIsInvalid()
        {
            Assert.IsTrue(!string.IsNullOrWhiteSpace(ApplicationController.CurrentSelectDriveViewModel.Model.Error));
        }

        protected override IMessageService GetMessageService()
        {
            return new MessageServicePositive();
        }

        protected override IFileEnumerator GetFileEnumerator()
        {
            return new FileEnumeratorHasMoreThanMaxNumberOfFiles();
        }
    }
}
