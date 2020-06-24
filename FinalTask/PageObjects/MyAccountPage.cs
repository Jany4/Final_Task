using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using SeleniumExtras.PageObjects;

namespace FinalTask
{
    public class MyAccountPage
    {
        private static string _customerAccNameXPath = "//a[@title=\"View my customer account\"]/span";

        private static IWebDriver _driver;
        private const string _logOutButtonXPath = "//a[@title=\"Log me out\"]";

        [FindsBy(How = How.XPath, Using = _logOutButtonXPath)]
        IWebElement _logOutButton;


        public MyAccountPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        public bool IsLoaded(string firstLastName)
        {
            string elemt = _driver.FindElement(By.XPath(_customerAccNameXPath)).Text;
            return (Waiter.Wait(_driver, By.XPath(_customerAccNameXPath)) && (_driver.FindElement(By.XPath(_customerAccNameXPath)).Text == firstLastName));
        }

        public void LogOut()
        {
            _logOutButton.Click();
        }

    }
}
