using System.Waf.Applications.Services;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.SelectPatientToAssociateImagesWith
{
    [TestClass]
    public class CancelNotConfirmDoesNothing : Stories.SelectPatientToAssociateImagesWith
    {
        private string _oldValue;
        private string _newValue;

        void GivenAValidPatientIsSelected()
        {
            TestDataHelper.MakePatientValid(ApplicationController.CurrentSelectPatientViewModel.Model);
            _oldValue = TestDataHelper.GetStringRepresentationOfObject(ApplicationController.CurrentSelectPatientViewModel.Model);
        }

        void AndGivenUserIsOnTheSelectPatientScreen()
        {
            Assert.IsTrue(ApplicationController.IsOnSelectPatientScreen);
        }

        void WhenUserClicksCancelAndDoesNotConfirm()
        {
            Assert.IsTrue(ApplicationController.CanCancel());
            ApplicationController.Cancel();
        }

        void ThenAllFormFieldsArePreserved()
        {
            _newValue = TestDataHelper.GetStringRepresentationOfObject(ApplicationController.CurrentSelectPatientViewModel.Model);
            Assert.IsTrue(string.Equals(_oldValue, _newValue));
        }

        void AndThenTheFormIsStillValid()
        {
            Assert.IsTrue(string.IsNullOrWhiteSpace(ApplicationController.CurrentSelectPatientViewModel.Model.Error));
        }

        protected override IMessageService GetMessageService()
        {
            return new MessageServiceNegative();
        }

    }
}
