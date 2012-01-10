using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using Bddify;
using Bddify.Core;
using ClinImIm.Applications.Controllers;
using ClinImIm.Applications.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Waf.Applications.Services;

namespace ClinImIm.Applications.Test.Stories
{
    [TestClass]
    [Story(
    AsA = "As a user",
    IWant = "I want to be able to select a drive",
    SoThat = "So that I can preview the images available on the drive")]
    public abstract class SelectDriveToPreview
    {
        protected CompositionContainer Container;
        protected IApplicationController ApplicationController;

        [TestMethod]
        public void Execute()
        {
            this.Bddify<SelectDriveToPreview>();
        }

        [TestInitialize]
        public void Initialize()
        {
            Container = TestHelper.GetContainer();
            TestHelper.ComposeContainerWithDefaults(Container);
            TestHelper.ComposeMessageServiceImplementation(Container, GetMessageService());
            TestHelper.ComposeFileEnumeratorImplementation(Container, GetFileEnumerator());

            ApplicationController = Container.GetExportedValue<IApplicationController>();
            ApplicationController.Initialize();
            ApplicationController.Run();
        }

        protected abstract IMessageService GetMessageService();
        protected abstract IFileEnumerator GetFileEnumerator();
    }
}
