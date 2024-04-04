using NUnit.Framework;
using ServoFramework.Base;
using ServoFramework.Pages;
using ServoTestProject.Data;
using ServoTestProject.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServoTestProject.Tests.workcell_startActivity.workcellDetailsPage
{
    public class StartNonAPI_TB : BaseTest
    {
        LoginPage loginPage = new LoginPage(DriverAdministration.getDriver());
        DashBoard dashboardPage = new DashBoard(DriverAdministration.getDriver());
        WorkCellsDetailsPage workCellsDetailsPage = new WorkCellsDetailsPage(DriverAdministration.getDriver());
        Calender calender = new Calender(DriverAdministration.getDriver());
        EnumMapping enumMapping = new EnumMapping();

        [Test, TestCaseSource(typeof(TestDataMethods), nameof(TestDataMethods.LoginAdmin)), Order(1)]
        public void startNonAPI_TB(string username, string password)
        {
            loginPage.Login(username, password);

            //Open Workcell Details Page
            dashboardPage.OpenWorkCellsDetailsPage(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_TB));
            Assert.AreEqual(dashboardPage.GetPageTitle(), enumMapping.MapEnumToString(PageTitles.WorkCellDetailsPage));
            workCellsDetailsPage.StartNonApiActivity("Method11", 3);

            //Make sure the activity is running
            Assert.AreEqual(workCellsDetailsPage.GetNonAPIActivityStatus(), enumMapping.MapEnumToString(ActivityStatus.Running));
        }

        [Test, Order(2)]
        public void AssertActivityRunInCalendar()
        {
            //Assert Activity has started on calendar
            workCellsDetailsPage.NavigateToCalender();
            Assert.AreEqual(calender.GetPageTitle(), enumMapping.MapEnumToString(PageTitles.Calendar));
            Assert.AreEqual(calender.GetWorkCellStatus(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_TB), ActivityStatus.Running), enumMapping.MapEnumToString(ActivityStatus.Running));
        }

        [Test, Order(3)]
        public void AssertActivityRunInDashboard()
        {
            //Assert Activity has started on calendar
            calender.NavigateToDashboard();
            Assert.AreEqual(dashboardPage.GetPageTitle(), enumMapping.MapEnumToString(PageTitles.Dashboard));
            Assert.AreEqual(dashboardPage.GetNonAPIActivityStatus(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_TB)),
                enumMapping.MapEnumToString(ActivityStatus.Running));
        }

        [Test, Order(4)]
        public void AssertAbortActivity()
        {
            //Assert Abort activity from workcell details
            dashboardPage.NavigateToWorkcellDetailsPage(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_TB));
            workCellsDetailsPage.AbortNonApiActivity();
            Assert.True(workCellsDetailsPage.IsActivitystatusAborted());
        }
    }
}
