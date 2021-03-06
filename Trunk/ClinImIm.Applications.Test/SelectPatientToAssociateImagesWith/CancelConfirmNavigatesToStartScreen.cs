﻿using System.Waf.Applications.Services;
using ClinImIm.Applications.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinImIm.Applications.Test.SelectPatientToAssociateImagesWith
{
    [TestClass]
    public class CancelConfirmNavigatesToStartScreen : Stories.SelectPatientToAssociateImagesWith
    {
        void GivenAValidPatientIsSelected()
        {
            TestDataHelper.MakePatientValid(ApplicationController.CurrentSelectPatientViewModel.Model);
        }

        void AndGivenUserIsOnTheSelectPatientScreen()
        {
            Assert.IsTrue(ApplicationController.IsOnSelectPatientScreen);
        }

        void WhenUserClicksCancelAndConfirms()
        {
            Assert.IsTrue(ApplicationController.CanCancel());
            ApplicationController.Cancel();
        }

        void ThenAllScreensAreCleared()
        {
            TestNavigationHelper.EnsureAllScreensAreCleared(ApplicationController);
        }

        void AndThenTheUserIsReturnedToTheStartScreenOfTheApplication()
        {
            TestNavigationHelper.EnsureIsOnStartScreen(ApplicationController);
        }

        protected override IMessageService GetMessageService()
        {
            return new MessageServicePositive();
        }
    }
}
