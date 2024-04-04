using NUnit.Framework;
using ServoFramework.Base;
using ServoFramework.Pages;
using ServoTestProject.Data;
using ServoTestProject.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServoTestProject.Tests.workcell_startActivity.dashboard
{
    public class StartAPI_TB : BaseTest
    {
        LoginPage loginPage = new LoginPage(DriverAdministration.getDriver());
        DashBoard dashboardPage = new DashBoard(DriverAdministration.getDriver());
        WorkCellsDetailsPage workCellsDetailsPage = new WorkCellsDetailsPage(DriverAdministration.getDriver());
        Calender calender = new Calender(DriverAdministration.getDriver());
        EnumMapping enumMapping = new EnumMapping();


        [Test, TestCaseSource(typeof(TestDataMethods), nameof(TestDataMethods.LoginAdmin)), Order(1)]
        public void StartAPI_TB_Activity(string username, string password)
        {
            loginPage.Login(username, password);
            dashboardPage.StartAPIActivity(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB),
                "DemoAssay_003_ReagentTroughToPlate96", "");
            //Assert.That(dashboardPage.GetMessage(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB)).Contains("started"));
            Assert.True(dashboardPage.IsProgressBarExist(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB)));
        }
        [Test, Order(2)]
        public void AssertActivityRunInWorkCellPageDetails()
        {
            //Assert Activity has started on workcell page details
            dashboardPage.OpenWorkCellsDetailsPage(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB));
            Assert.AreEqual(dashboardPage.GetPageTitle(), enumMapping.MapEnumToString(PageTitles.WorkCellDetailsPage));
            Assert.AreEqual(workCellsDetailsPage.GetButtonStatus(), "pause icon");
        }
        [Test, Order(3)]
        public void AssertActivityRunInCalendar()
        {
            //Assert Activity has started on calendar
            workCellsDetailsPage.NavigateToCalender();
            Assert.AreEqual(calender.GetPageTitle(), enumMapping.MapEnumToString(PageTitles.Calendar));
            Assert.AreEqual(calender.GetWorkCellStatus(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB),
                ActivityStatus.Running), enumMapping.MapEnumToString(ActivityStatus.Running));
        }
    }
}
