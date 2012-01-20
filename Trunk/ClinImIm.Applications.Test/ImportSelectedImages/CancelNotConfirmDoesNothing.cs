using System.Waf.Applications.Services;
using ClinImIm.Applications.Test.Mocks;
using ClinImIm.Applications.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.ImportSelectedImages
{
    [TestClass]
    public class CancelNotConfirmDoesNothing : Stories.ImportSelectedImages
    {
        private string _oldValue;
        private string _newValue;

        void GivenValidImagesAreSelected()
        {
            TestDataHelper.MakeImagesToImportValid(ApplicationController.CurrentImportImagesViewModel.Model);
            _oldValue = TestDataHelper.GetStringRepresentationOfObject(ApplicationController.CurrentImportImagesViewModel.Model);
        }

        void AndGivenUserIsOnTheImportImagesScreen()
        {
            Assert.IsTrue(ApplicationController.IsOnImportImagesScreen);
        }

        void WhenUserClicksCancelAndDoesNotConfirm()
        {
            Assert.IsTrue(ApplicationController.CanCancel());
            ApplicationController.Cancel();
        }

        void ThenAllFormFieldsArePreserved()
        {
            _newValue = TestDataHelper.GetStringRepresentationOfObject(ApplicationController.CurrentImportImagesViewModel.Model);
            Assert.IsTrue(string.Equals(_oldValue, _newValue));
        }

        void AndThenTheFormIsStillValid()
        {
            Assert.IsTrue(string.IsNullOrWhiteSpace(ApplicationController.CurrentImportImagesViewModel.Model.Error));
        }

        protected override IMessageService GetMessageService()
        {
            return new MessageServiceNegative();
        }

        protected override IImportImagesView GetImportImagesView()
        {
            return new ImportImagesViewValid();
        }
    }
}
