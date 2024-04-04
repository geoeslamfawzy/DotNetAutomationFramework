using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using ServoFramework.Base;
using ServoFramework.ProjectConstants;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServoFramework.Factory
{
    public class WaitFactory 
    {
        static IWebElement element = null;
        public static IWebElement performExplicitWait(WaitStrategy waitStrategy, By locator)
        {  
            switch (waitStrategy)
            {
                case WaitStrategy.CLICKABLE:
                    element =  new WebDriverWait(DriverAdministration.getDriver(), TimeSpan.FromSeconds(Constants.GetExplicitWait()))
                    .Until(ExpectedConditions.ElementToBeClickable(locator));
                    break;
                case WaitStrategy.PRESENCE:
                    element = new WebDriverWait(DriverAdministration.getDriver(), TimeSpan.FromSeconds(Constants.GetExplicitWait()))
                   .Until(ExpectedConditions.ElementExists(locator));
                    break;
                case WaitStrategy.VISIBLE:
                    element = new WebDriverWait(DriverAdministration.getDriver(), TimeSpan.FromSeconds(Constants.GetExplicitWait()))
                   .Until(ExpectedConditions.ElementIsVisible(locator));
                    break;
                case WaitStrategy.NONE:
                    element = DriverAdministration.getDriver().FindElement(locator);
                    break;

            }
            return element;
        }
        public static IWebElement PerformFluentWait(By locator)
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(DriverAdministration.getDriver());
            fluentWait.Timeout = TimeSpan.FromSeconds(Constants.GetFluentWait());
            fluentWait.PollingInterval = TimeSpan.FromSeconds(Constants.GetPollingInterval());
            fluentWait.IgnoreExceptionTypes(typeof(Exception));
            try
            {
                element = fluentWait.Until(x => DriverAdministration.getDriver().FindElement(locator));
            }
            catch (Exception e)
            {
                Console.WriteLine("You have an Exception : " + e.Message);
            }
            return element;
        }
       
        public static void PerformImplicitWait()
        {
            DriverAdministration.getDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Constants.GetImplictWait());
        }
    }

    public enum WaitStrategy
    {
        CLICKABLE,
        PRESENCE,
        VISIBLE,
        NONE
    }

    public enum WaitType
    {
        Explicit,
        Implicit,
        Fluent
    }
}
