using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;


namespace FinalTask
{
    public class ProductDetailsPage
    {
        private static IWebDriver _driver;

        private const string _addToWishListButtonId = "wishlist_button";
        private const string _productNameXPath = "//h1[@itemprop=\"name\"]";
        private const string _addToCartButtonXPath = "//button[@name=\"Submit\"]";

        [FindsBy(How = How.Id, Using = _addToWishListButtonId)]
        IWebElement _addToWishListButton;

        [FindsBy(How = How.XPath, Using = _productNameXPath)]
        IWebElement _productName;

        [FindsBy(How = How.XPath, Using = _addToCartButtonXPath)]
        IWebElement _addToCartButton;

        public ProductDetailsPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        public ProductDetailsPage(IWebDriver driver, string url)
        {
            _driver = driver;
            _driver.Navigate().GoToUrl(url);
            PageFactory.InitElements(_driver, this);
        }

        public void AddtoWishList()
        {
            _addToWishListButton.Click();
        }

        public void AddToCart()
        {
            _addToCartButton.Click();
        }

        public string GetProductName()
        {
            return _productName.Text;
        }

        public bool IsLoaded()
        {
            return (Waiter.Wait(_driver, By.XPath(_productNameXPath)));
        }
    }
}
