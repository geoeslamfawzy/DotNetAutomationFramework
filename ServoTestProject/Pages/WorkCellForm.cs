using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ServoFramework.Base;
using ServoTestProject.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ServoFramework.Pages
{
    public class WorkCellForm 
    {
        UIActions ui = new UIActions();
        SideBar sideBar;
        private readonly IWebDriver driver;
        public WorkCellForm(IWebDriver driver)
        {
            this.driver = driver;
            //if (sideBar == null)
            //{
            //    this.sideBar = new SideBar(driver);
            //}
        }

        private By workCellTypeMenue = By.Id("workCellTypesOptions");
        private By workCellApiType = By.Id("workCellAPITypesOptions");
        private By workCellName = By.Id("name");
        private By calenderTypeMenue = By.Id("calendarTypeOptions");
        private By calenderTypeOptions, cellTypeOptions = By.TagName("p-dropdownitem");
        private By activitiesPerDayField = By.XPath("//input[@inputmode='decimal']");
        
        private By colorSlider = By.XPath("//p-colorpicker/div/div/div/div/following-sibling::div/div");
        private By colorBox = By.Id("color");
        private By colorSelector = By.XPath("//p-colorpicker/div/div/div/div/div/div");
        private By addBtn = By.XPath("//button[@label='Add']");

        private By workCellCam = By.XPath("//p-multiselect/div/div/div");



        public WorkCellsDetailsPage CreateWorkCell(string workCellName, WorkCellAPIType workCellAPIType, WorkCellType workCellType, CalenderType calenderType, string noOfActivitiesPerDay = "5")
        {
            ui.ChooseFromMenue(this.workCellTypeMenue, workCellType.ToString()); //workcell type
            ui.InsertText(activitiesPerDayField, noOfActivitiesPerDay);
            ui.InsertText(this.workCellName, workCellName); //workcell name
            ui.ChooseFromMenue(this.calenderTypeMenue, calenderType.ToString()); //calender type
            ui.ChooseFromMenue(this.workCellApiType, workCellAPIType.ToString());
            ChooseRandomColor();
            ui.clickOn(addBtn);
            return new WorkCellsDetailsPage(driver);
        }
        public WorkCellsDetailsPage AddWorkCell(string workCellName, string workCellAPIType, string workCellTypeMenue, string calenderType, string noOfActivitiesPerDay = "5")
        {
            ui.ChooseFromMenue(this.workCellTypeMenue, workCellTypeMenue); //workcell type
            ui.InsertText(activitiesPerDayField, noOfActivitiesPerDay);
            ui.InsertText(this.workCellName, workCellName); //workcell name
            ui.ChooseFromMenue(this.calenderTypeMenue, calenderType); //calender type
            ui.ChooseFromMenue(this.workCellApiType, workCellAPIType);
            ChooseRandomColor();
            ui.clickOn(addBtn);
            return new WorkCellsDetailsPage(driver);
        }

        private void ChooseRandomColor()
        {
            Random random = new Random();
            int y_slider = random.Next(-150, -1);
            int x = random.Next(1, 150);
            int y = random.Next(1, 148);

            Actions action = new Actions(driver);
            ui.clickOn(colorBox);
            Thread.Sleep(1000);
            action.DragAndDropToOffset(ui.Find(colorSlider),0, y_slider).Perform();
            action.MoveByOffset(-x, y).Click().Perform();
        }

    }
}
