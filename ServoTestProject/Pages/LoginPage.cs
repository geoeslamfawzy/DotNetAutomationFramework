using OpenQA.Selenium;
using ServoFramework.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServoFramework.Pages
{
    public class LoginPage 
    {
        UIActions ui = new UIActions();
        private readonly IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        private By txtUsername = By.Id("username");
        private By txtPassword = By.Id("password");
        private By btnLogin = By.XPath("//button[@type='submit']/span");
        public DashBoard Login(string username, string password)
        {
            ui.InsertText(txtUsername, username);
            ui.InsertText(txtPassword, password);
            ui.clickOn(btnLogin);
            return new DashBoard(driver);
        }
        public DashBoard LoginWithAdminCredentials()
        {
            ui.InsertText(txtUsername, "SERVO_AUTOMATION");
            ui.InsertText(txtPassword, "123@Sta.com");
            ui.doubleClickUsingJS(btnLogin);
            return new DashBoard(driver);
        }
    }
}
