using NUnit.Framework;
using ServoFramework.Base;
using ServoFramework.Pages;
using ServoTestProject.Data;
using ServoTestProject.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServoTestProject.Tests.pauesResumeAbort.Calendar
{
    public class CompleteActivtiy_FIFO : BaseTest
    {
        LoginPage loginPage = new LoginPage(DriverAdministration.getDriver());
        DashBoard dashboardPage = new DashBoard(DriverAdministration.getDriver());
        WorkCellsDetailsPage workCellsDetailsPage = new WorkCellsDetailsPage(DriverAdministration.getDriver());
        Calender calender = new Calender(DriverAdministration.getDriver());
        EnumMapping enumMapping = new EnumMapping();
        static string date;

        [Test, TestCaseSource(typeof(TestDataMethods), nameof(TestDataMethods.LoginAdmin)), Order(1)]
        public void startNonAPIFIFOActivity(string username, string password)
        {
            loginPage.Login(username, password);
            dashboardPage.StartNonApiActivity(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_FIFO), "Method2", 313, "WL1");
            date = DateTime.Now.ToString("MM/dd/yyy h:mm");
            Assert.AreEqual(dashboardPage.GetNonAPIActivityStatus(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_FIFO)),
               enumMapping.MapEnumToString(ActivityStatus.Running));
        }
        [Test, Order(2)]
        public void CompleteActivityFromCalendar()
        {
            dashboardPage.NavigateToCalender();
            Assert.AreEqual(calender.GetPageTitle(), enumMapping.MapEnumToString(PageTitles.Calendar));
            calender.CompleteActivity(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_FIFO));
            Assert.That(calender.GetWorkCellStatus(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_FIFO)), 
                Is.EqualTo(enumMapping.MapEnumToString(ActivityStatus.Completed)));
        }
        [Test, Order(3)]
        public void AssertActivityIsStoredInHistory()
        {
            calender.NavigateToDashboard();
            dashboardPage.OpenWorkCellsDetailsPage(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_FIFO));
            Assert.That(workCellsDetailsPage.GetActivityStatueFromHistory(date),
                Is.EqualTo(enumMapping.MapEnumToString(ActivityStatus.Completed)));
        }
    }
}
