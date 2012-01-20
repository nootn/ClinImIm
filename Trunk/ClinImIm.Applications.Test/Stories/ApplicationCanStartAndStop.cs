using System.ComponentModel.Composition;
using System.Waf.Applications.Services;
using Bddify;
using Bddify.Core;
using ClinImIm.Applications.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition.Hosting;
using ClinImIm.Applications.Controllers;
using ClinImIm.Applications.Core;

namespace ClinImIm.Applications.Test.Stories
{
    [TestClass]
    [Story(
    AsA = "As a user",
    IWant = "I want to be able to start and gracefully stop the application",
    SoThat = "So that I can use the application and not corrupt any data")]
    public abstract class ApplicationCanStartAndStop
    {
        protected CompositionContainer Container;
        protected IApplicationController ApplicationController;

        [TestMethod]
        public void Execute()
        {
            this.Bddify<ApplicationCanStartAndStop>();
        }

        [TestInitialize]
        public void Initialize()
        {
            Container = CompositionHelper.GetContainer();
            CompositionHelper.ComposeContainerWithDefaults(Container);
            CompositionHelper.ComposeMessageServiceImplementation(Container, NSubstitute.Substitute.For<IMessageService>());
            CompositionHelper.ComposeFileEnumeratorImplementation(Container, NSubstitute.Substitute.For<IFileEnumerator>());
            CompositionHelper.ComposeImportImagesViewImplementation(Container, NSubstitute.Substitute.For<IImportImagesView>());

            ApplicationController = Container.GetExportedValue<IApplicationController>();
        }
    }
}
