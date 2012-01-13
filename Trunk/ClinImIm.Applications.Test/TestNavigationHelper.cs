using ClinImIm.Applications.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test
{
    internal static class TestNavigationHelper
    {
        internal static void NavigateFromSelectDriveToSelectPatient(IApplicationController controller)
        {
            Assert.IsTrue(controller.IsOnSelectDriveScreen);
            Assert.IsTrue(controller.CanNext());
            controller.Next();
            Assert.IsTrue(controller.IsOnSelectPatientScreen);
        }

        internal static void NavigateFromSelectPatientToSelectImages(IApplicationController controller)
        {
            Assert.IsTrue(controller.IsOnSelectPatientScreen);
            Assert.IsTrue(controller.CanNext());
            controller.Next();
            Assert.Inconclusive("Next screen is not built yet");
        }

        internal static void EnsureAllScreensAreCleared(IApplicationController controller)
        {
            //Select Drive Screen
            Assert.IsTrue(string.IsNullOrWhiteSpace(controller.CurrentSelectDriveViewModel.Model.SelectedDrivePath));
            Assert.IsTrue(controller.CurrentSelectDriveViewModel.Model.PhotoFiles.Count == 0);

            //Select Patient Screen

        }

        internal static void EnsureIsOnStartScreen(IApplicationController controller)
        {
            Assert.IsTrue(controller.IsOnSelectDriveScreen);
        }
    }
}
