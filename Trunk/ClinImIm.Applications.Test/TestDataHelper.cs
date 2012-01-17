using System;
using System.IO;
using ClinImIm.Applications.Test.Mocks;
using ClinImIm.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace ClinImIm.Applications.Test
{
    internal static class TestDataHelper
    {
        #region Common Methods

        internal static string GetStringRepresentationOfObject(object item)
        {
            return item == null ? String.Empty : JsonConvert.SerializeObject(item);
        }

        #endregion

        #region SelectedDrive Methods

        internal static void MakeDriveValid(SelectedDrive item)
        {
            item.SelectedDrivePath = @"C:\";
            Assert.IsTrue(String.IsNullOrWhiteSpace(item.Error), "SelectedDrive had validation errors: {0}", item.Error);
        }

        internal static void MakeDriveInvalidNoImages(SelectedDrive item)
        {
            item.SelectedDrivePath = @"C:\";
            Assert.IsTrue(!String.IsNullOrWhiteSpace(item.Error));
            Assert.IsTrue(item.PhotoFiles.Count == 0);
        }

        internal static void MakeDriveInvalidNoDriveAndNoImages(SelectedDrive item)
        {
            item.SelectedDrivePath = String.Empty;
            Assert.IsTrue(!String.IsNullOrWhiteSpace(item.Error));
            Assert.IsTrue(item.PhotoFiles.Count == 0);
        }

        #endregion

        #region Patient Methods

        internal static void MakePatientValid(Patient item)
        {
            item.Identifier = "999999999";
            item.FullName = "Jones, Jane Mary (64 years old)";
            Assert.IsTrue(String.IsNullOrWhiteSpace(item.Error), "Patient had validation errors: {0}", item.Error);
        }

        internal static void MakePatientInValid(Patient item)
        {
            item.Identifier = "";
            item.FullName = "";
            Assert.IsTrue(!String.IsNullOrWhiteSpace(item.Error));
        }

        #endregion

        #region SelectedImages Methods

        internal static void MakeImageSelectionValid(ImageSelection item)
        {
            Assert.IsTrue(item.AllImages.Count > 0);
            foreach (var currItem in item.AllImages)
            {
                currItem.IsSelected = true;
            }
            Assert.IsTrue(String.IsNullOrWhiteSpace(item.Error));
        }

        internal static void MakeImageSelectionInvalidNoSelectedImages(ImageSelection item)
        {
            Assert.IsTrue(item.AllImages.Count > 0);
            foreach (var currItem in item.AllImages)
            {
                currItem.IsSelected = false;
            }
            Assert.IsTrue(!String.IsNullOrWhiteSpace(item.Error));
        }

        #endregion
    }
}
