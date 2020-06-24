using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using SeleniumExtras.PageObjects;


namespace FinalTask
{
    public class LoginPage
    {        private static string _pageUrl = "http://automationpractice.com/index.php?controller=authentication&back=my-account";
        private static string _titleText = "Login - My Store";
        private const string _createAccountEmailFieldId = "email_create";
        private const string _createAccountButtonId = "SubmitCreate";
        private const string _signInEmailFieldId = "email";
        private const string _passwordFieldId = "passwd";
        private const string _signInButtonId = "SubmitLogin";

        private IWebDriver _driver;

        [FindsBy(How = How.Id, Using = _createAccountEmailFieldId)]
        private IWebElement _createAccountEmailField;

        [FindsBy(How = How.Id, Using = _createAccountButtonId)]
        private IWebElement _createAccountButton;

        [FindsBy(How = How.Id, Using = _signInEmailFieldId)]
        private IWebElement _signInEmailField;

        [FindsBy(How = How.Id, Using = _passwordFieldId)]
        private IWebElement _passwordField;

        [FindsBy(How = How.Id, Using = _signInButtonId)]
        private IWebElement _signInButton;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            Load();
            PageFactory.InitElements(_driver, this);
        }

        private void Load()
        {
            _driver.Navigate().GoToUrl(_pageUrl);
        }

        public MyAccountPage LogIn(string LogInStr, string PasswordStr)
        {
            _signInEmailField.SendKeys(LogInStr);
            _passwordField.SendKeys(PasswordStr);
            _signInButton.Click();

            return new MyAccountPage(_driver);
        }

        public CreateAccountPage CreateAccount(string LogInStr)
        {
            _createAccountEmailField.SendKeys(LogInStr);
            _createAccountButton.Click();

            return new CreateAccountPage(_driver);
        }

        public bool IsLoaded()
        {
            //string title = _driver.Title;
            return (Waiter.Wait(_driver, By.Id(_signInButtonId)) && (_driver.Title.Equals(_titleText)));
        }
    }
}
