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

        internal static void NavigateFromSelectPatientToSelectDrive(IApplicationController controller)
        {
            Assert.IsTrue(controller.IsOnSelectPatientScreen);
            Assert.IsTrue(controller.CanBack());
            controller.Back();
            Assert.IsTrue(controller.IsOnSelectDriveScreen);
        }

        internal static void NavigateFromSelectPatientToSelectImages(IApplicationController controller)
        {
            Assert.IsTrue(controller.IsOnSelectPatientScreen);
            Assert.IsTrue(controller.CanNext());
            controller.Next();
            Assert.IsTrue(controller.IsOnSelectImagesScreen);
        }

        internal static void NavigateFromSelectImagesToSelectPatient(IApplicationController controller)
        {
            Assert.IsTrue(controller.IsOnSelectImagesScreen);
            Assert.IsTrue(controller.CanBack());
            controller.Back();
            Assert.IsTrue(controller.IsOnSelectPatientScreen);
        }

        internal static void NavigateFromSelectImagesToImportImages(IApplicationController controller)
        {
            Assert.IsTrue(controller.IsOnSelectImagesScreen);
            Assert.IsTrue(controller.CanNext());
            controller.Next();
            Assert.Inconclusive("Next screen does not exist yet");
        }

        internal static void EnsureAllScreensAreCleared(IApplicationController controller)
        {
            //Select Drive Screen
            Assert.IsTrue(string.IsNullOrWhiteSpace(controller.CurrentSelectDriveViewModel.Model.SelectedDrivePath));
            Assert.IsTrue(controller.CurrentSelectDriveViewModel.Model.PhotoFiles.Count == 0);

            //Select Patient Screen
            Assert.IsTrue(string.IsNullOrWhiteSpace(controller.CurrentSelectPatientViewModel.Model.Identifier));
            Assert.IsTrue(string.IsNullOrWhiteSpace(controller.CurrentSelectPatientViewModel.Model.FullName));

            //Select Images Screen
            Assert.IsTrue(controller.CurrentSelectImagesViewModel.Model.AllImages.Count == 0);
        }

        internal static void EnsureIsOnStartScreen(IApplicationController controller)
        {
            Assert.IsTrue(controller.IsOnSelectDriveScreen);
        }
    }
}
