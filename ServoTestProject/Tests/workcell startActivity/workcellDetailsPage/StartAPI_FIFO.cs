﻿using NUnit.Framework;
using ServoFramework.Base;
using ServoFramework.Pages;
using ServoTestProject.Data;
using ServoTestProject.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServoTestProject.Tests.workcell_startActivity.workcellDetailsPage
{
    public class StartAPI_FIFO : BaseTest
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

            dashboardPage.OpenWorkCellsDetailsPage(enumMapping.MapEnumToString(WorkCellNames.Test));
            Assert.AreEqual(dashboardPage.GetPageTitle(), enumMapping.MapEnumToString(PageTitles.WorkCellDetailsPage));
            workCellsDetailsPage.StartAPIActivity("DemoAssay_004_Aliquote_1ColSourceDWPTo12ColTargetM", "WL4");

            //Make sure the activity is running
            Assert.True(workCellsDetailsPage.IsProgressBarExist()); //Failed to start activity, There is an activity running on this work cell (bug)
        }
        [Test, Order(2)]
        public void AssertActivityRunInCalendar()
        {
            //Assert Activity has started on calendar
            workCellsDetailsPage.NavigateToCalender();
            Assert.AreEqual(calender.GetPageTitle(), enumMapping.MapEnumToString(PageTitles.Calendar));
            Assert.AreEqual(calender.GetWorkCellStatus(enumMapping.MapEnumToString(WorkCellNames.Test),
                            ActivityStatus.Running), enumMapping.MapEnumToString(ActivityStatus.Running));
        }
        [Test, Order(3)]
        public void AssertActivityRunInDashboard()
        {
            //Assert Activity has started on calendar
            calender.NavigateToDashboard();
            Assert.AreEqual(dashboardPage.GetPageTitle(), enumMapping.MapEnumToString(PageTitles.Dashboard));
            Assert.True(dashboardPage.IsProgressBarExist(enumMapping.MapEnumToString(WorkCellNames.Test)));
        }
    }
}