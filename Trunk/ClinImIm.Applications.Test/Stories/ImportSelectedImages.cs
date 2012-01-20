using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using Bddify;
using Bddify.Core;
using ClinImIm.Applications.Controllers;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.Test.Mocks;
using ClinImIm.Applications.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Waf.Applications.Services;

namespace ClinImIm.Applications.Test.Stories
{
    [TestClass]
    [Story(
    AsA = "As a user",
    IWant = "I want to be able to import selected images",
    SoThat = "So that I can save them somewhere associated with the patient and possibly clear them off the drive")]
    public abstract class ImportSelectedImages
    {
        protected CompositionContainer Container;
        protected IApplicationController ApplicationController;

        [TestMethod]
        public void Execute()
        {
            this.Bddify<ImportSelectedImages>();
        }

        [TestInitialize]
        public void Initialize()
        {
            Container = CompositionHelper.GetContainer();
            CompositionHelper.ComposeContainerWithDefaults(Container);
            CompositionHelper.ComposeMessageServiceImplementation(Container, GetMessageService());
            CompositionHelper.ComposeFileEnumeratorImplementation(Container, new FileEnumeratorHasMoreThanMaxNumberOfFiles());
            CompositionHelper.ComposeImportImagesViewImplementation(Container, GetImportImagesView());

            ApplicationController = Container.GetExportedValue<IApplicationController>();
            ApplicationController.Initialize();
            ApplicationController.Run();

            TestDataHelper.MakeDriveValid(ApplicationController.CurrentSelectDriveViewModel.Model);
            TestNavigationHelper.NavigateFromSelectDriveToSelectPatient(ApplicationController);

            TestDataHelper.MakePatientValid(ApplicationController.CurrentSelectPatientViewModel.Model);
            TestNavigationHelper.NavigateFromSelectPatientToSelectImages(ApplicationController);

            TestDataHelper.MakeImageSelectionValid(ApplicationController.CurrentSelectImagesViewModel.Model);
            TestNavigationHelper.NavigateFromSelectImagesToImportImages(ApplicationController);
        }

        protected abstract IMessageService GetMessageService();

        protected abstract IImportImagesView GetImportImagesView();
    }
}
