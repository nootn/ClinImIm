﻿using System.Waf.Applications.Services;
using ClinImIm.Applications.Core;
using ClinImIm.Applications.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.SelectDriveToPreview
{
    [TestClass]
    public class SelectingDriveWithNoImagesCannotProducePreview : Stories.SelectDriveToPreview
    {
        private void GivenAValidDriveWithoutImagesIsSelected()
        {
            TestDataHelper.MakeDriveInvalidNoImages(ApplicationController.CurrentSelectDriveViewModel.Model);
        }

        void AndGivenUserIsOnTheSelectDriveScreen()
        {
            Assert.IsTrue(ApplicationController.IsOnSelectDriveScreen);
        }

        private void ThenImageFilesAreNotAvailableForPreview()
        {
            Assert.IsTrue(ApplicationController.CurrentSelectDriveViewModel.Model.PhotoFiles.Count == 0);
        }

        protected override IMessageService GetMessageService()
        {
            return new MessageServicePositive();
        }

        protected override IFileEnumerator GetFileEnumerator()
        {
            return new FileEnumeratorNoFiles();
        }
    }
}
