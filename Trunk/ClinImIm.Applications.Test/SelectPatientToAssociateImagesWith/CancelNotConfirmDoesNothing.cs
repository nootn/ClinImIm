using System.Waf.Applications.Services;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.SelectPatientToAssociateImagesWith
{
    [TestClass]
    public class CancelNotConfirmDoesNothing : Stories.SelectPatientToAssociateImagesWith
    {
        void GivenAValidPatientIsSelected()
        {
            Assert.Inconclusive();
        }

        void WhenUserClicksCancelAndDoesNotConfirm()
        {
        }

        void ThenAllFormFieldsAreNotCleared()
        {
        }

        void AndThenTheFormIsValid()
        {
        }

        protected override IMessageService GetMessageService()
        {
            return new MessageServiceNegative();
        }

    }
}
