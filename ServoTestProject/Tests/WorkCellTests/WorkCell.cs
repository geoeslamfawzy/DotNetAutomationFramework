using NUnit.Framework;
using ServoFramework.Base;
using ServoFramework.Factory;
using ServoFramework.Pages;
using ServoTestProject.Data;
using ServoTestProject.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServoTestProject.Tests.WorkCellTests
{
    [TestFixture]
    public class WorkCell : BaseTest
    {
        LoginPage loginPage = new LoginPage(DriverAdministration.getDriver());
        string workCellName = "Eslam_Demo_Auto" + new Random().Next();
        EnumMapping enumMapping = new EnumMapping();
        
        [Test, Parallelizable(ParallelScope.All), TestCaseSource(typeof(TestDataMethods), nameof(TestDataMethods.LoginAdmin))]
        public void AddWorkCell(string username, string password)
        {
            loginPage.Login(username, password);
            DashBoard dashBoard = new DashBoard(DriverAdministration.getDriver());
            Assert.AreEqual(dashBoard.GetPageTitle(), enumMapping.MapEnumToString(PageTitles.Dashboard));
            dashBoard.navigateToWorkCells();
            WorkCellsDetailsPage workCellPage = new WorkCellsDetailsPage(DriverAdministration.getDriver());
            workCellPage.openWorkCellForm();
            WorkCellForm cellForm = new WorkCellForm(DriverAdministration.getDriver());
            cellForm.AddWorkCell(workCellName, 
                enumMapping.MapEnumToString(WorkCellAPIType.InstinctS), 
                enumMapping.MapEnumToString(WorkCellType.General), 
                enumMapping.MapEnumToString(CalenderType.TimeBased));
        }
    }
}