using OpenQA.Selenium;
using ServoFramework.Base;
using ServoFramework.ProjectConstants;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServoFramework.Pages
{
    public class SideBar 
    {
        UIActions ui = new UIActions();
        private readonly IWebDriver driver;

        private By dashBoard = By.CssSelector("li:nth-child(1) > a > div");
        private By notifications = By.ClassName("p-button-icon.pi.pi-bell");
        private By calender = By.XPath("//a[@routerlink='/calendar']/div");
        private By activityHistory = By.XPath("//div[@class='img-container activity-history']");
        private By maintenance = By.CssSelector("#p-accordiontab-1 > p-header > div > div");
        private By maintenanceStore = By.XPath("//*[text()=' Store ']");
        private By analytics = By.ClassName("img-container.analytics");
        private By settings = By.XPath("//div[@class= 'img-container settings']");
        private By users = By.XPath("//div[@id='p-accordiontab-0-content']//a[contains(text(), 'Users')]");
        private By workCells  =By.XPath("//a[contains(text(), 'Work Cells')]");
        private By methods = By.XPath("//a[contains(text(), 'Methods')]");
        private By worklists = By.XPath("//a[contains(text(), 'Worklists')]");
        private By cameras = By.XPath("//a[contains(text(), 'Cameras')]");
        private By pageTitle = By.TagName("h3");
        private By _confirmDialog = By.XPath("//div[@role='dialog']//button[@label='Ok']");
        private By _cancelDialog = By.XPath("//div[@role='dialog']//button[@label='Cancel']");
        private By _toastMsg = By.XPath("//p-toastitem//div[contains(text(), 'Activity')]");

        public MethodsPage OpenWorkCellForm()
        {
            ui.clickOn(settings);
            ui.ClickByJS(workCells);
            return new MethodsPage(driver);
        }
        public ActivityHistory NavigateToActivityHistory()
        {
            ui.clickOn(activityHistory);
            return new ActivityHistory(driver);
        }

        public MaintenanceStore NavigateToMaintenanceStore()
        {
            ui.clickOn(maintenance);
            ui.clickOn(maintenanceStore);
            return new MaintenanceStore(driver);
        }
        public SideBar NavigateToDashboard()
        {
            if (GetPageTitle().Contains("Dashboard"))
            {
                return this;
            }
            else
            {
                ui.NavigateTo(Constants.GetURL());
                return new SideBar();
            }
        }


        public WorkCellsDetailsPage navigateToWorkCells()
        {
            ui.clickOn(settings);
            ui.ClickByJS(workCells);
            return new WorkCellsDetailsPage(DriverAdministration.getDriver());
        }
        public Calender NavigateToCalender()
        {
            ui.clickOn(calender);
            return new Calender(driver);
        }

        public string GetPageTitle()
        {
            return ui.Find(pageTitle).Text;
        }
        public void Confirm()
        {
            ui.ClickByJS(_confirmDialog);
        }
        public void Cancel()
        {
            ui.ClickByJS(_cancelDialog);
        }
        public string GetToastMessage() 
        {
            return ui.Find(_toastMsg).Text.ToString();
        }
    }
}