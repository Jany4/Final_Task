using NUnit.Framework;
using System;
//using System.IO;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Remote;
using Allure.Commons;
using Allure.NUnit.Attributes;

namespace FinalTask.Tests
{
    public class AccountTests:AllureReport
    {
        //private static ChromeOptions _browserOptions = new ChromeOptions();
        private static FirefoxOptions _browserOptions = new FirefoxOptions();

        private static IWebDriver _driver;

        private Dictionary<string, string> AccountAttributes = new Dictionary<string, string>
        {
            {"FirstName", "TestFirstName"},
            {"LastName", "TestLastName" },
            {"Email", "test+3@gmail.com" },
            {"Password", "Qwerty123" },
            {"Address1", "Test Address"},
            {"City", "Boston" },
            {"State", "Arizona" },
            {"ZipCode", "12345" },
            {"Country", "United States" },
            {"MobilePhone", "123456789" },
            {"Alias", "Test Alias" }
        };

        [OneTimeTearDown]
        public void Final()
        {
            _driver.Quit();
        }

        [TearDown]
        public void CleanUp()
        {
            string result = TestContext.CurrentContext.Result.Outcome.Status.ToString();

            if (TestContext.CurrentContext.Result.Outcome.Status.ToString() == "Failed")
            {
                AllureLifecycle.Instance.AddAttachment("Fail Screenshot", AllureLifecycle.AttachFormat.ImagePng, ((ITakesScreenshot)_driver).GetScreenshot().AsByteArray);
            }

            _driver.Navigate().GoToUrl("http://automationpractice.com/index.php?mylogout=");

        }

        [SetUp]
        public void Setup()
        {
            _driver = new RemoteWebDriver(new Uri("http://192.168.100.4:4444/wd/hub"), _browserOptions);
        }

        [AllureTest("Testing account creation functionality")]
        [AllureLink("https//:test.com/issue1", false)]
        [AllureSeverity(Allure.Commons.Model.SeverityLevel.Critical)]
        [AllureSubSuite("Account")]
        [AllureOwner("Vorobyov")]
        [Test]
        public void CreateAccountTest()
        {
            LoginPage loginPage = new LoginPage(_driver);
            Assert.IsTrue(loginPage.IsLoaded(), "LogIn page was not loaded");

            CreateAccountPage createAccountPage = loginPage.CreateAccount(AccountAttributes["Email"]);
            Assert.IsTrue(createAccountPage.IsLoaded(), "Create Account page was not loaded");


            Assert.IsTrue(createAccountPage.CretaeAccount(AccountAttributes).IsLoaded(AccountAttributes["FirstName"] + " " + AccountAttributes["LastName"]), "Account wasn't created");
        }

        [AllureTest("Testing login functionality")]
        [AllureLink("https//:test.com/issue2", false)]
        [AllureSeverity(Allure.Commons.Model.SeverityLevel.Critical)]
        [AllureSubSuite("Account")]
        [AllureOwner("Vorobyov")]
        [TestCase("test+2@gmail.com", "Qwerty123", "TestFirstName TestLastName")]
        [Test]
        public void LogInTest(string logIn, string password, string firstLastName)
        {
            LoginPage loginPage = new LoginPage(_driver);
            Assert.IsTrue(loginPage.IsLoaded(), "LogIn page was not loaded");

            MyAccountPage accountPage = loginPage.LogIn(logIn, password);
            Assert.IsTrue(accountPage.IsLoaded(firstLastName), "LogIn Failed");
        }
    }
}