using NUnit.Framework;
using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support;
using SeleniumExtras.PageObjects;
using Allure.Commons;
using Allure.NUnit.Attributes;
using OpenQA.Selenium.Remote;


namespace FinalTask.Tests
{
    public class CardTests
    {
        //private static ChromeOptions _browserOptions = new ChromeOptions();
        private static FirefoxOptions _browserOptions = new FirefoxOptions();

        private static IWebDriver _driver;
        private static ProductDetailsPage productDetailsPage;
        private static CartPage cartPage;

        private static List<string> products = new List<string>() {"http://automationpractice.com/index.php?id_product=1&controller=product",
                                                                   "http://automationpractice.com/index.php?id_product=2&controller=product",
                                                                   "http://automationpractice.com/index.php?id_product=3&controller=product"};
        [SetUp]
        public void Setup()
        {
            _driver = new RemoteWebDriver(new Uri("http://192.168.100.4:4444/wd/hub"), _browserOptions);
        }

        [OneTimeTearDown]
        public void Final()
        {
            _driver.Close();
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

        [AllureTest("Testing adding product to a shopping cart functionality")]
        [AllureLink("https//:test.com/issue1", false)]
        [AllureSeverity(Allure.Commons.Model.SeverityLevel.Critical)]
        [AllureSubSuite("Account")]
        [AllureOwner("Vorobyov")]
        [TestCase("test+2@gmail.com", "Qwerty123", "TestFirstName TestLastName")]
        [Test]
        public void AddProductToCartTest(string logIn, string password, string firstLastName)
        {
            LoginPage loginPage = new LoginPage(_driver);
            Assert.IsTrue(loginPage.IsLoaded(), "LogIn page was not loaded");

            MyAccountPage accountPage = loginPage.LogIn(logIn, password);
            Assert.IsTrue(accountPage.IsLoaded(firstLastName), "LogIn Failed");

            List<string> productList = new List<string>();
            foreach (string product in products)
            {
                productDetailsPage = new ProductDetailsPage(_driver, product);
                Assert.IsTrue(productDetailsPage.IsLoaded(), "Product details page is not available");
                productList.Add(productDetailsPage.GetProductName());
                productDetailsPage.AddToCart();
            }

            cartPage = new CartPage(_driver);

            foreach (string productName in productList)
            {
                Assert.IsTrue(cartPage.CheckProductInCart(productName), "Not all the products were added to the Shopping cart");
            }
        }


    }
}
