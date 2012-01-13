using System.Waf.Applications.Services;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.Test.Mocks;
using ClinImIm.Applications.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.SelectDriveToPreview
{
    [TestClass]
    public class SelectingDriveWithImagesCanProducePreview : Stories.SelectDriveToPreview
    {
        void GivenAValidDriveWithImagesIsSelected()
        {
            TestPreconditionHelper.MakeDriveValid(ApplicationController.CurrentSelectDriveViewModel.Model);
        }

        void ThenTheCorrectNumberOfImageFilesAreAvailableForPreview()
        {
            Assert.IsTrue(ApplicationController.CurrentSelectDriveViewModel.Model.PhotoFiles.Count == SelectDriveViewModel.MaximumNumberOfFiles);
        }

        protected override IMessageService GetMessageService()
        {
            return new MessageServicePositive();
        }

        protected override IFileEnumerator GetFileEnumerator()
        {
            return new FileEnumeratorHasMoreThanMaxNumberOfFiles();
        }
    }
}
