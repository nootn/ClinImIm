using System.Waf.Applications.Services;
using ClinImIm.Applications.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.SelectPatientToAssociateImagesWith
{
    [TestClass]
    public class CannotProgressIfPatientIsNotSelected : Stories.SelectPatientToAssociateImagesWith
    {
        void GivenAPatientIsNotSelected()
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
