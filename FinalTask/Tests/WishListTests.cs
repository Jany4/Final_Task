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
    public class WishListTests
    {
        //private static ChromeOptions _browserOptions = new ChromeOptions();
        private static FirefoxOptions _browserOptions = new FirefoxOptions();

        private static IWebDriver _driver;
        private static WishListPage wishListPage;

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

            wishListPage.RemoveAllWishLists();

            _driver.Navigate().GoToUrl("http://automationpractice.com/index.php?mylogout=");
        }


        [AllureTest("Testing automatically created wishlist functionality")]
        [AllureLink("https//:test.com/issue1", false)]
        [AllureSeverity(Allure.Commons.Model.SeverityLevel.Critical)]
        [AllureSubSuite("Account")]
        [AllureOwner("Vorobyov")]
        [TestCase("test+2@gmail.com", "Qwerty123", "TestFirstName TestLastName")]
        [Test]
        public void AutoCreatedWishListTest(string logIn, string password, string firstLastName)
        {
            LoginPage loginPage = new LoginPage(_driver);
            Assert.IsTrue(loginPage.IsLoaded(), "LogIn page was not loaded");

            MyAccountPage accountPage = loginPage.LogIn(logIn, password);
            Assert.IsTrue(accountPage.IsLoaded(firstLastName), "LogIn Failed");

            wishListPage = new WishListPage(_driver);
            Assert.IsTrue(wishListPage.IsLoaded(), "Wishlist page is not available");
            Assert.IsFalse(wishListPage.WishListExists(), "WishList shoud be empty to run the test");

            ProductDetailsPage product = wishListPage.SelectRandomProductFromTopSellers();
            Assert.IsTrue(product.IsLoaded());

            string productName = product.GetProductName();
            product.AddtoWishList();

            wishListPage = new WishListPage(_driver);
            Assert.IsTrue(wishListPage.IsLoaded(), "Wishlist page is not available2");
            Assert.IsTrue(wishListPage.WishListExists(), "Wishlist wasn't created");
            Assert.IsTrue(wishListPage.CheckProductInWishList(productName), "Product wasn't added to the wishlist");
        }

        [AllureTest("Testing adding products to the wishlist functionality")]
        [AllureLink("https//:test.com/issue1", false)]
        [AllureSeverity(Allure.Commons.Model.SeverityLevel.Critical)]
        [AllureSubSuite("Account")]
        [AllureOwner("Vorobyov")]
        [TestCase("test+2@gmail.com", "Qwerty123", "TestFirstName TestLastName", "Test Wishlist")]
        [Test]
        public void AddTowishlistTest(string logIn, string password, string firstLastName, string wishListName)
        {
            LoginPage loginPage = new LoginPage(_driver);
            Assert.IsTrue(loginPage.IsLoaded(), "LogIn page was not loaded");

            MyAccountPage accountPage = loginPage.LogIn(logIn, password);
            Assert.IsTrue(accountPage.IsLoaded(firstLastName), "LogIn Failed");

            wishListPage = new WishListPage(_driver);
            Assert.IsTrue(wishListPage.IsLoaded(), "Wishlist page is not available");

            wishListPage.CreateNewWishList(wishListName);
            Assert.IsTrue(wishListPage.IsWishListCreated(wishListName), "Was not able to create a wishlist");

            ProductDetailsPage product = wishListPage.SelectRandomProductFromTopSellers();
            Assert.IsTrue(product.IsLoaded());

            string productName = product.GetProductName();
            product.AddtoWishList();

            wishListPage = new WishListPage(_driver);
            Assert.IsTrue(wishListPage.IsLoaded(), "Wishlist page is not available");
            Assert.IsTrue(wishListPage.WishListExists(), "Wishlist wasn't created");
            Assert.IsTrue(wishListPage.CheckProductInWishList(productName), "Product wasn't added to the wishlist");
        }



    }
}
