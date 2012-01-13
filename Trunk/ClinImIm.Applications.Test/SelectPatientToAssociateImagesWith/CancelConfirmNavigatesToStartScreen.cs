using System.Waf.Applications.Services;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.SelectDriveToPreview
{
    [TestClass]
    public class CancelConfirmNavigatesToStartScreen : Stories.SelectPatientToAssociateImagesWith
    {
        void GivenAValidPatientIsSelected()
        {
            Assert.Inconclusive();
        }

        void WhenUserClicksCancelAndConfirms()
        {
            
        }

        void ThenAllScreensAreCleared()
        {

        }

        void AndThenTheUserIsReturnedToTheStartScreenOfTheApplication()
        {
            
        }

        protected override IMessageService GetMessageService()
        {
            return new MessageServicePositive();
        }
    }
}
