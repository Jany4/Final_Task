using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace FinalTask
{
    public class Waiter
    {
        private IWebDriver _driver;
        private string _path;


        static public bool Wait(IWebDriver driver, By path)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                return (wait.Until(driver => driver.FindElement(path).Displayed));
            }
            catch(WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}
