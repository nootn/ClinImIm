using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Waf.Applications.Services;
using ClinImIm.Applications.Controllers;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.Test.Mocks;
using ClinImIm.Applications.Views;
using ClinImIm.Domain;
using NSubstitute;

namespace ClinImIm.Applications.Test
{
    internal static class CompositionHelper
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
            batch.AddExportedValue(Substitute.For<IShellView>());
            batch.AddExportedValue(Substitute.For<ISelectDriveView>());
            batch.AddExportedValue(Substitute.For<ISelectPatientView>());
            batch.AddExportedValue(Substitute.For<ISelectImagesView>());
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

        internal static void ComposeImportImagesViewImplementation(CompositionContainer container, IImportImagesView item)
        {
            var batch = new CompositionBatch();
            batch.AddExportedValue(item);
            container.Compose(batch);
        }
    }
}
