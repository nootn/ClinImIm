using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.ApplicationCanStartAndStop
{
    [TestClass]
    public class CanBeShutdown : Stories.ApplicationCanStartAndStop
    {
        void GivenTheApplicationIsCreated()
        {
        }

        void WhenShutdownIsCalled()
        {
            ApplicationController.Shutdown();
        }

        void ThenTheApplicationIsSuccessfullyStopped()
        {
            Assert.IsTrue(ApplicationController.IsRunning == false);
        }
    }
}
