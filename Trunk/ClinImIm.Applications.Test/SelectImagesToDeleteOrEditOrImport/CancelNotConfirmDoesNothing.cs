using System.Waf.Applications.Services;
using ClinImIm.Applications.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.SelectImagesToDeleteOrEditOrImport
{
    [TestClass]
    public class CancelNotConfirmDoesNothing : Stories.SelectImagesToDeleteOrEditOrImport
    {
        private string _oldValue;
        private string _newValue;

        void GivenValidImagesAreSelected()
        {
            TestDataHelper.MakeImageSelectionValid(ApplicationController.CurrentSelectImagesViewModel.Model);
            _oldValue = TestDataHelper.GetStringRepresentationOfObject(ApplicationController.CurrentSelectImagesViewModel.Model);
        }

        void AndGivenUserIsOnTheSelectImagesScreen()
        {
            Assert.IsTrue(ApplicationController.IsOnSelectImagesScreen);
        }

        void WhenUserClicksCancelAndDoesNotConfirm()
        {
            Assert.IsTrue(ApplicationController.CanCancel());
            ApplicationController.Cancel();
        }

        void ThenAllFormFieldsArePreserved()
        {
            _newValue = TestDataHelper.GetStringRepresentationOfObject(ApplicationController.CurrentSelectImagesViewModel.Model);
            Assert.IsTrue(string.Equals(_oldValue, _newValue));
        }

        void AndThenTheFormIsStillValid()
        {
            Assert.IsTrue(string.IsNullOrWhiteSpace(ApplicationController.CurrentSelectImagesViewModel.Model.Error));
        }

        protected override IMessageService GetMessageService()
        {
            return new MessageServiceNegative();
        }

    }
}
