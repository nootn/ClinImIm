using System.Waf.Applications.Services;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.SelectDriveToPreview
{
    [TestClass]
    public class CancelNotConfirmDoesNotClearForm : Stories.SelectDriveToPreview
    {
        void GivenAValidDriveIsSelected()
        {
            TestHelper.MakeDriveValid(ApplicationController.CurrentSelectDriveViewModel.Model);
        }

        void WhenUserClicksCancelAndDoesNotConfirm()
        {
            Assert.IsTrue(ApplicationController.CanCancel());
            ApplicationController.Cancel();
        }

        void ThenAllFormFieldsAreNotCleared()
        {
            Assert.IsTrue(!string.IsNullOrWhiteSpace(ApplicationController.CurrentSelectDriveViewModel.Model.SelectedDrivePath));
            Assert.IsTrue(ApplicationController.CurrentSelectDriveViewModel.Model.PhotoFiles.Count > 0);
        }

        void AndThenTheFormIsValid()
        {
            Assert.IsTrue(string.IsNullOrWhiteSpace(ApplicationController.CurrentSelectDriveViewModel.Model.Error));
        }

        protected override IMessageService GetMessageService()
        {
            return new MessageServiceNegative();
        }

        protected override IFileEnumerator GetFileEnumerator()
        {
            return new FileEnumeratorHasMoreThanMaxNumberOfFiles();
        }
    }
}
