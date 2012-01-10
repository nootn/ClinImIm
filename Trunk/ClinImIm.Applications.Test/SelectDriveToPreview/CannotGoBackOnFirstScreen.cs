using System.Waf.Applications.Services;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.SelectDriveToPreview
{
    [TestClass]
    public class CannotGoBackOnFirstScreen : Stories.SelectDriveToPreview
    {
        void GivenUserIsOnTheFirstScreenInTheApplication()
        {
        }

        void ThenUserCannotGoBack()
        {
            Assert.IsTrue(!ApplicationController.CanBack());
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
