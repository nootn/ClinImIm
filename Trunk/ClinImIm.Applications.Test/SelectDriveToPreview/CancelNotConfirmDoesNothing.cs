using System.Waf.Applications.Services;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.SelectDriveToPreview
{
    [TestClass]
    public class CancelNotConfirmDoesNothing : Stories.SelectDriveToPreview
    {
        private string _oldValue;
        private string _newValue;

        void GivenAValidDriveIsSelected()
        {
            TestDataHelper.MakeDriveValid(ApplicationController.CurrentSelectDriveViewModel.Model);
            _oldValue = TestDataHelper.GetStringRepresentationOfObject(ApplicationController.CurrentSelectDriveViewModel.Model);
        }

        void AndGivenUserIsOnTheSelectDriveScreen()
        {
            Assert.IsTrue(ApplicationController.IsOnSelectDriveScreen);
        }

        void WhenUserClicksCancelAndDoesNotConfirm()
        {
            Assert.IsTrue(ApplicationController.CanCancel());
            ApplicationController.Cancel();
        }

        void ThenAllFormFieldsArePreserved()
        {
            _newValue = TestDataHelper.GetStringRepresentationOfObject(ApplicationController.CurrentSelectDriveViewModel.Model);
            Assert.IsTrue(string.Equals(_oldValue, _newValue));
        }

        void AndThenTheFormIsStillValid()
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
