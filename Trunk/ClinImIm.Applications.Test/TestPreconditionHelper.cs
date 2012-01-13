using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using ClinImIm.Applications.Controllers;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.Test.Mocks;
using ClinImIm.Applications.Views;
using System.Waf.Applications.Services;
using ClinImIm.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test
{
    internal static class TestPreconditionHelper
    {

        internal static void MakeDriveValid(SelectedDrive item)
        {
            item.SelectedDrivePath = @"C:\";
            Assert.IsTrue(string.IsNullOrWhiteSpace(item.Error), "SelectedDrive had validation errors: {0}", item.Error);
        }

        internal static void MakeDriveInvalidNoImages(SelectedDrive item)
        {
            item.SelectedDrivePath = @"C:\";
            Assert.IsTrue(!string.IsNullOrWhiteSpace(item.Error));
            Assert.IsTrue(item.PhotoFiles.Count == 0);
        }

        internal static void MakeDriveInvalidNoDriveAndNoImages(SelectedDrive item)
        {
            item.SelectedDrivePath = string.Empty;
            Assert.IsTrue(!string.IsNullOrWhiteSpace(item.Error));
            Assert.IsTrue(item.PhotoFiles.Count == 0);
        }
    }
}
