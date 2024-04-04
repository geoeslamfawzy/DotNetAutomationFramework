using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using ServoFramework.Base;
using ServoFramework.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServoFramework.Extensions
{
    public static class WebElementExtensions
    {
        //public static IWebElement Find(this IWebElement webElement)
        //{
            
        //    return webElement.Find();
        //}

        
        public static void AssertElementPresent(this IWebElement element)
        {
            if (!IsElementPresent(element))
                throw new Exception(String.Format("AssertElementNotPresent exception"));
        }

        public static void Hover(this IWebElement element)
        {
            Actions actions = new Actions(DriverAdministration.getDriver());
            actions.MoveToElement(element).Perform();
        }
        private static bool IsElementPresent(IWebElement element)
        {
            try
            {
                bool el = element.Displayed;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }




        /* Aya Element Actions */

    }
}