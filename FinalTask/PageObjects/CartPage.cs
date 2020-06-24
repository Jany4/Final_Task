using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;

namespace FinalTask
{
    public class CartPage
    {
        private static IWebDriver _driver;

        private const string _cartPageUrl = "http://automationpractice.com/index.php?controller=order";
        private const string _productsInCartXPath = "//table[@id=\"cart_summary\"]//p[@class=\"product-name\"]//a";
        private const string _cartPageHeaderXPath = "//h1[contains(text(), \"Shopping-cart summary\")]";

        public CartPage(IWebDriver driver)
        {
            _driver = driver;
            _driver.Navigate().GoToUrl(_cartPageUrl);

            PageFactory.InitElements(_driver, this);
        }

        public bool IsLoaded()
        {
            return (Waiter.Wait(_driver, By.XPath(_cartPageHeaderXPath)));
        }

        public bool CartIsEmpty()
        {
            return (!Waiter.Wait(_driver, By.XPath(_productsInCartXPath)));
        }

        public bool CheckProductInCart(string productName)
        {
            if (CartIsEmpty())
            {
                return false;
            }
            IList<IWebElement> productList = _driver.FindElements(By.XPath(_productsInCartXPath));

            foreach (IWebElement product in productList)
            {
                if (productName == product.Text)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
