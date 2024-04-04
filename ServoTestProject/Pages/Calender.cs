using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ServoFramework.Base;
using ServoTestProject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServoFramework.Pages
{
    public class Calender : SideBar
    {

        UIActions ui = new UIActions();
        //SideBar sideBar;
        private readonly IWebDriver driver;
        public Calender(IWebDriver driver)
        {
            this.driver = driver;
            //if(sideBar == null)
            //{
            //    this.sideBar = new SideBar(driver);
            //}

        }
        private static readonly string value = "value";
        /// <summary>
        /// private static readonly string _workcellstatusPath = $"//h5[contains(text(),  '{value}')]/parent::div/following-sibling::div/div/div/div/span";
        /// </summary>
        private static readonly string _startDatePath = $"//h5[contains(text(),  '{value}')]/parent::div/following-sibling::div//span[contains(text(), 'Start Date')]/following-sibling::span";
        private static readonly string _workcellstatusPath = $"//h5[contains(text(),  '{value}')]/parent::div/following-sibling::div[last()]/div/div/div/span";
        private static readonly string _completeBtn = $"//h5[contains(text(),  '{value}')]/parent::div/following-sibling::div//span[text()=' Running ']/parent::div/parent::div/following-sibling::div//button[@ptooltip='Complete Activity']";
        private static readonly string _pauseResumeBtnGenericPath = $"//h5[contains(text(),  '{value}')]/parent::div/following-sibling::div//button[@ptooltip='Resume Activity' or @ptooltip = 'Pause Activity']";
        private By schedualActivityBtn = By.Id("header-btn");

        public void ScheduleActivity()
        {
            ui.clickOn(schedualActivityBtn);
        }

        public string GetWorkCellStatus(string workcellName)
        {
            try
            {
                return ui.Find(ui.GetLocator(_workcellstatusPath, workcellName)).Text.ToString();
            }
            catch (Exception)
            {

                return ui.Find(ui.GetLocator(_workcellstatusPath, workcellName)).Text.ToString();
            }
        }

        public string GetWorkCellStatus(string workcellName, ActivityStatus activityStatus)
        {
            By workcellStatus = By.XPath(_workcellstatusPath.Replace(value, workcellName));
            var jobsStatusInCalender = ui.FindElements(workcellStatus);
            foreach (var status in jobsStatusInCalender)
            {
                if (status.Text == activityStatus.ToString())
                {
                    return status.Text.ToString();
                }
            }
            return "I didn't find " + activityStatus.ToString(); ;
        }

        public Calender CompleteActivity(string workcellName)
        {
            ui.ClickByJS(ui.GetLocator(_completeBtn, workcellName));
            Confirm();
            ui.Refresh();
            return this;
        }
        public Calender PauseResumeActivity(string workcellname)
        {
            try
            {
                ui.doubleClickUsingJS(ui.GetLocator(_pauseResumeBtnGenericPath, workcellname));
                ui.WaitFor(3);
                return this;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}