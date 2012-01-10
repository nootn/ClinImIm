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
    internal static class TestHelper
    {
        internal static CompositionContainer GetContainer()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(IApplicationController).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ValidationModel).Assembly));
            return new CompositionContainer(catalog);
        }

        internal static void ComposeContainerWithDefaults(CompositionContainer container)
        {
            var batch = new CompositionBatch();
            batch.AddExportedValue(container);
            batch.AddExportedValue<IDispatcher>(new MockDispatcher());
            batch.AddExportedValue(NSubstitute.Substitute.For<IShellView>());
            batch.AddExportedValue(NSubstitute.Substitute.For<ISelectDriveView>());
            container.Compose(batch);
        }

        internal static void ComposeMessageServiceImplementation(CompositionContainer container, IMessageService item)
        {
            var batch = new CompositionBatch();
            batch.AddExportedValue(item);
            container.Compose(batch);
        }

        internal static void ComposeFileEnumeratorImplementation(CompositionContainer container, IFileEnumerator item)
        {
            var batch = new CompositionBatch();
            batch.AddExportedValue(item);
            container.Compose(batch);
        }

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
