using ClinImIm.Applications.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.ApplicationCanStartAndStop
{
    [TestClass]
    public class CanBeInitializedThenRunThenShutdown : Stories.ApplicationCanStartAndStop
    {
        void GivenTheApplicationIsCreated()
        {
        }

        void WhenInitializeIsCalled()
        {
            ApplicationController.Initialize();
        }

        void AndWhenRunIsCalled()
        {
            ApplicationController.Run();
        }

        void AndWhenShutdownIsCalled()
        {
            ApplicationController.Shutdown();
        }

        void ThenTheApplicationIsSuccessfullyStopped()
        {
            Assert.IsTrue(ApplicationController.IsRunning == false);
        }
    }
}
