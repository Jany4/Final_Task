using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;

namespace FinalTask
{
    public class CreateAccountPage
    {
        private static IWebDriver _driver;

        private const string _pageHeaderText = "CREATE AN ACCOUNT";
        private const string _firstNameFieldId = "customer_firstname";
        private const string _lastNameFieldId = "customer_lastname";
        private const string _passwordFieldId = "passwd";
        private const string _address1FieldId = "address1";
        private const string _cityFieldId = "city";
        private const string _stateDropDownId = "id_state";
        private const string _zipCodeFieldId = "postcode";
        private const string _countryDropDownId = "id_country";
        private const string _mobilePhoneFieldId = "phone_mobile";
        private const string _addressAliasFieldId = "alias";
        private const string _createAccountButtonid = "submitAccount";
        private const string _createAccountPageHeaderXPath = "//h1[text()=\"Create an account\"]";



        [FindsBy(How = How.Id, Using = _firstNameFieldId)]
        IWebElement _firstNameField;

        [FindsBy(How = How.Id, Using = _lastNameFieldId)]
        IWebElement _lastNameField;

        [FindsBy(How = How.Id, Using = _passwordFieldId)]
        IWebElement _passwordField;

        [FindsBy(How = How.Id, Using = _address1FieldId)]
        IWebElement _address1Field;

        [FindsBy(How = How.Id, Using = _cityFieldId)]
        IWebElement _cityField;

        [FindsBy(How = How.Id, Using = _zipCodeFieldId)]
        IWebElement _zipCodeField;

        [FindsBy(How = How.Id, Using = _mobilePhoneFieldId)]
        IWebElement _mobilePhoneField;

        [FindsBy(How = How.Id, Using = _addressAliasFieldId)]
        IWebElement _addressAliasField;

        [FindsBy(How = How.Id, Using = _createAccountButtonid)]
        IWebElement _createAccountButton;

        [FindsBy(How = How.XPath, Using = _createAccountPageHeaderXPath)]
        IWebElement _createAccountPageHeader;

        public CreateAccountPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        public MyAccountPage CretaeAccount(Dictionary<string, string> fieldsValues)
        {
            _firstNameField.SendKeys(fieldsValues["FirstName"]);
            _lastNameField.SendKeys(fieldsValues["LastName"]);
            _passwordField.SendKeys(fieldsValues["Password"]);
            _address1Field.SendKeys(fieldsValues["Address1"]);
            _cityField.SendKeys(fieldsValues["City"]);

            SelectElement _stateDropDown = new SelectElement(_driver.FindElement(By.Id(_stateDropDownId)));
            _stateDropDown.SelectByText(fieldsValues["State"]);
            _zipCodeField.SendKeys(fieldsValues["ZipCode"]);

            SelectElement _countryDropDown = new SelectElement(_driver.FindElement(By.Id(_countryDropDownId)));
            _countryDropDown.SelectByText(fieldsValues["Country"]);
            _mobilePhoneField.SendKeys(fieldsValues["MobilePhone"]);

            _createAccountButton.Click();

            return new MyAccountPage(_driver);
        }

        public bool IsLoaded()
        {
            return ((Waiter.Wait(_driver, By.XPath(_createAccountPageHeaderXPath))));
        }
    }
}
