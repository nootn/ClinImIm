using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using Bddify;
using Bddify.Core;
using ClinImIm.Applications.Controllers;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Waf.Applications.Services;

namespace ClinImIm.Applications.Test.Stories
{
    [TestClass]
    [Story(
    AsA = "As a user",
    IWant = "I want to be able to select a patient",
    SoThat = "So that I can associate images with that patient")]
    public abstract class SelectPatientToAssociateImagesWith
    {
        protected CompositionContainer Container;
        protected IApplicationController ApplicationController;

        [TestMethod]
        public void Execute()
        {
            this.Bddify<SelectPatientToAssociateImagesWith>();
        }

        [TestInitialize]
        public void Initialize()
        {
            Container = CompositionHelper.GetContainer();
            CompositionHelper.ComposeContainerWithDefaults(Container);
            CompositionHelper.ComposeMessageServiceImplementation(Container, GetMessageService());
            CompositionHelper.ComposeFileEnumeratorImplementation(Container, new FileEnumeratorHasMoreThanMaxNumberOfFiles());

            ApplicationController = Container.GetExportedValue<IApplicationController>();
            ApplicationController.Initialize();
            ApplicationController.Run();

            TestDataHelper.MakeDriveValid(ApplicationController.CurrentSelectDriveViewModel.Model);
            TestNavigationHelper.NavigateFromSelectDriveToSelectPatient(ApplicationController);
        }

        protected abstract IMessageService GetMessageService();
    }
}
