using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;

namespace FinalTask
{
    public class WishListPage
    {
        private static IWebDriver _driver;

        private const string _wishListUrl = "http://automationpractice.com/index.php?fc=module&module=blockwishlist&controller=mywishlist";
        private const string _wishListPageHeaderXPath = "//h1[contains(text(), \"My wishlists\")]";
        private const string _wishListGridId = "block-history";
        private const string _viewWishListLinkXPath = "//a[contains(@onclick, \"WishlistManage\")]";
        private const string _productInWishListId = "s_title";
        private const string _topSellersProductXPath = "//div[@id=\"best-sellers_block_right\"]//a[contains(@href, \"product\")]";
        private const string _newWishListNameFieldId = "name";
        private const string _createNewWishListButtonId = "submitWishlist";
        private const string _removeWishListIconsXPath = "//*[@id=\"block-history\"]//a[contains(@onclick, \"WishlistDelete\")]";

        [FindsBy(How = How.Id, Using = _wishListGridId)]
        IWebElement _wishListGrid;

        [FindsBy(How = How.XPath, Using = _wishListPageHeaderXPath)]
        IWebElement _wishListPageHeader;

        [FindsBy(How = How.XPath, Using = _topSellersProductXPath)]
        IList<IWebElement> topSellerProductsList;

        [FindsBy(How = How.Id, Using = _newWishListNameFieldId)]
        IWebElement _newWishListNameField;

        [FindsBy(How = How.Id, Using = _createNewWishListButtonId)]
        IWebElement _createNewWishListButton;

        public WishListPage(IWebDriver driver)
        {
            _driver = driver;
            _driver.Navigate().GoToUrl(_wishListUrl);
            PageFactory.InitElements(_driver, this);
        }

        public bool IsLoaded()
        {
            return (Waiter.Wait(_driver, By.XPath(_wishListPageHeaderXPath)));
        }

        public bool WishListExists()
        {
            return (Waiter.Wait(_driver, By.Id(_wishListGridId)));
        }

        public bool CheckProductInWishList(string productName)
        {
            _driver.FindElement(By.XPath(_viewWishListLinkXPath)).Click();
            return (Waiter.Wait(_driver, By.Id(_productInWishListId)) && (_driver.FindElement(By.Id(_productInWishListId)).Text.Contains(productName)));
        }

        public ProductDetailsPage SelectRandomProductFromTopSellers()
        {
            Random rnd = new Random();
            topSellerProductsList[rnd.Next(0, topSellerProductsList.Count)].Click();

            return new ProductDetailsPage(_driver);
        }

        public void CreateNewWishList(string wishlistName)
        {
            _newWishListNameField.SendKeys(wishlistName);
            _createNewWishListButton.Click();
        }

        public bool IsWishListCreated(string wishlistName)
        {
            return (Waiter.Wait(_driver, By.XPath($"//div[@id=\"{_wishListGridId}\"]" + $"//a[contains(text(), \"{wishlistName}\")]")));
        }

        public void RemoveAllWishLists()
        {
            while (Waiter.Wait(_driver, By.XPath(_removeWishListIconsXPath)))
            {
                _driver.FindElement(By.XPath(_removeWishListIconsXPath)).Click();
                IAlert removeAlertMessage = _driver.SwitchTo().Alert();
                removeAlertMessage.Accept();
            }
        }
    }
}
