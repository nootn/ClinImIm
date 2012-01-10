using Microsoft.VisualStudio.TestTools.UnitTesting;
    
namespace ClinImIm.Applications.Test.ApplicationCanStartAndStop
{
    [TestClass]
    public class CanBeRun : Stories.ApplicationCanStartAndStop
    {
        void GivenTheApplicationIsCreated()
        {
        }

        void WhenRunIsCalled()
        {
            ApplicationController.Run();
        }

        void ThenTheApplicationIsSuccessfullyStarted()
        {
            Assert.IsTrue(ApplicationController.IsRunning);
        }
    }
}
