using NUnit.Framework;
using ServoFramework.Base;
using ServoFramework.Pages;
using ServoTestProject.Data;
using ServoTestProject.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServoTestProject.Tests.pauesResumeAbort.Dashboard
{
    public class PauseResumeAPIActivity_TB : BaseTest
    {
        LoginPage loginPage = new LoginPage(DriverAdministration.getDriver());
        DashBoard dashboardPage = new DashBoard(DriverAdministration.getDriver());
        WorkCellsDetailsPage workCellsDetailsPage = new WorkCellsDetailsPage(DriverAdministration.getDriver());
        Calender calender = new Calender(DriverAdministration.getDriver());
        EnumMapping enumMapping = new EnumMapping();


        [Test, TestCaseSource(typeof(TestDataMethods), nameof(TestDataMethods.LoginAdmin)), Order(1)]
        public void startNonAPIFIFOActivity(string username, string password)
        {
            loginPage.Login(username, password);
            dashboardPage.OpenWorkCellsDetailsPage(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB));
            Assert.AreEqual(dashboardPage.GetPageTitle(), enumMapping.MapEnumToString(PageTitles.WorkCellDetailsPage));
            workCellsDetailsPage.StartAPIActivity("DemoAssay_004_Aliquote_1ColSourceDWPTo12ColTargetM");
            Assert.That(workCellsDetailsPage.GetCurrentActiveStatus(),
             Is.EqualTo(enumMapping.MapEnumToString(ActivityStatus.Running)));
        }

        [Test, Order(2)]
        public void PauseFromDashboard()
        {
            workCellsDetailsPage.NavigateToDashboard();
            Assert.That(dashboardPage.GetPageTitle(), Is.EqualTo(enumMapping.MapEnumToString(PageTitles.Dashboard)));
            Assert.That(dashboardPage.GetButtonStatus(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB)), Does.Contain("pause"));
            dashboardPage.PauseResumeActivity(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB));
        }
        [Test, Order(3)]
        public void AssertThatActivityIsPausedInCalendar()
        {
        }

        [Test, Order(4)]
        public void AssertActivityPausedInDashboard()
        {

        }

        [Test, Order(5)]
        public void ResumeFromWorkcellDetailsPage()
        {

        }

        [Test, Order(6)]
        public void AssertThatActivityIsRunningInCalendar()
        {

        }
        [Test, Order(7)]
        public void AssertThatActivityIsRunningInDashboard()
        {

        }
    }
}
