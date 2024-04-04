﻿using NUnit.Framework;
using ServoFramework.Base;
using ServoFramework.Pages;
using ServoTestProject.Data;
using ServoTestProject.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ServoTestProject.Tests.workcell_startActivity.dashboard
{
    public class StartNonAPI_TB : BaseTest
    {
        LoginPage loginPage = new LoginPage(DriverAdministration.getDriver());
        DashBoard dashboardPage = new DashBoard(DriverAdministration.getDriver());
        WorkCellsDetailsPage WorkCellsDetailsPage = new WorkCellsDetailsPage(DriverAdministration.getDriver());
        Calender calender = new Calender(DriverAdministration.getDriver());
        EnumMapping enumMapping = new EnumMapping();

        [Test, TestCaseSource(typeof(TestDataMethods), nameof(TestDataMethods.LoginAdmin)), Order(1)]
        public void startNonAPI_TB(string username, string password)
        {
            loginPage.Login(username, password);
            dashboardPage.StartNonApiActivity(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_TB), "Method11", 31);
            Assert.AreEqual(dashboardPage.GetNonAPIActivityStatus(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_TB)),
                enumMapping.MapEnumToString(ActivityStatus.Running));
        }
        [Test, Order(2)]
        public void AssertActivityRunInWorkCellPageDetails()
        {
            //Assert Activity has started on workcell page details
            dashboardPage.OpenWorkCellsDetailsPage(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_TB));
            Assert.AreEqual(dashboardPage.GetPageTitle(), enumMapping.MapEnumToString(PageTitles.WorkCellDetailsPage));
            Assert.AreEqual(WorkCellsDetailsPage.GetNonAPIActivityStatus(), enumMapping.MapEnumToString(ActivityStatus.Running));
        }
        [Test, Order(3)]
        public void AssertActivityRunInCalendar()
        {
            //Assert Activity has started on calendar
            WorkCellsDetailsPage.NavigateToCalender();
            Assert.AreEqual(calender.GetPageTitle(), enumMapping.MapEnumToString(PageTitles.Calendar));
            Assert.AreEqual(calender.GetWorkCellStatus(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_TB), ActivityStatus.Running), enumMapping.MapEnumToString(ActivityStatus.Running));
        }

        [Test, Order(4)]
        public void AssertAbortActivity()
        {
            //Assert Abort activity from dashboard
            calender.NavigateToDashboard();
            dashboardPage.AbortNonApiActivity(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_TB));
            Assert.True(dashboardPage.IsStartNonAPIActivityBtnExist(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_TB)));
        }
    }
}