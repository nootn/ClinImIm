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
            TestDataHelper.MakeDriveValid(ApplicationController.CurrentSelectDriveViewModel.Model);
        }

        void AndGivenUserIsOnTheSelectDriveScreen()
        {
            Assert.IsTrue(ApplicationController.IsOnSelectDriveScreen);
        }

        void WhenUserClicksCancelAndConfirms()
        {
            Assert.IsTrue(ApplicationController.CanCancel());
            ApplicationController.Cancel();
        }

        void ThenAllFormFieldsAreCleared()
        {
            TestNavigationHelper.EnsureAllScreensAreCleared(ApplicationController);
        }

        void AndThenTheFormIsInvalid()
        {
            Assert.IsTrue(!string.IsNullOrWhiteSpace(ApplicationController.CurrentSelectDriveViewModel.Model.Error));
        }

        void AndThenTheUserIsStillOnTheSelectDriveScreen()
        {
            TestNavigationHelper.EnsureIsOnStartScreen(ApplicationController);
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
