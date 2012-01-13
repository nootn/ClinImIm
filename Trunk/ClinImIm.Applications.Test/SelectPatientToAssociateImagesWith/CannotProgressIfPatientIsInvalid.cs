using System.Waf.Applications.Services;
using ClinImIm.Applications.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.SelectPatientToAssociateImagesWith
{
    [TestClass]
    public class CannotProgressIfPatientIsInvalid : Stories.SelectPatientToAssociateImagesWith
    {
        void GivenAnInvalidPatientIsSelected()
        {
            Assert.Inconclusive();
        }

        void ThenUserCannotProgressToNextStep()
        {
        }

        protected override IMessageService GetMessageService()
        {
            return new MessageServiceNegative();
        }

    }
}
