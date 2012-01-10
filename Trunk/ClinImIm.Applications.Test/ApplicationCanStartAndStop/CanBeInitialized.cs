using ClinImIm.Applications.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.ApplicationCanStartAndStop
{
    [TestClass]
    public class CanBeInitialized : Stories.ApplicationCanStartAndStop
    {
        void GivenTheApplicationIsCreated()
        {
        }

        void WhenInitializeIsCalled()
        {
            ApplicationController.Initialize();
        }

        void ThenTheApplicationIsSuccessfullyInitialized()
        {
            Assert.IsTrue(ApplicationController.IsRunning == false);
        }
    }
}
