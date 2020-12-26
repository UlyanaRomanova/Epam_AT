using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using WebDriverFramework.BusinessObjects;
using WebDriverFramework.PageObjects;

namespace WebDriverFramework.Step_definitions
{
    [Binding]
    class CreateCoffeeSteps
    {
        private IWebDriver driver;

        [Given(@"I open ""(.+)"" url")]
        public void GivenIOpenLoginPage(string url)
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
        }

        [When(@"I log in with the ""(.+)"", ""(.+)"" parameters")]
        public void WhenILogInWithTheParameters(string userName, string userPassword)
        {
            User user = new User(userName, userPassword);
            new LoginPage(driver).TypeLogInData(user);
            new LoginPage(driver).ClickButtonSend();
        }

        [When(@"I click the All products link")]
        public void WhenIClickTheLink()
        {
            new HomePage(driver).ClickAllProductsLink();
        }

        [When(@"I click the Create new button")]
        public void WhenIClickTheButton()
        {
            new AllProductsPage(driver).ClickButtonCreateProduct();
        }

        [When(@"I create product with values ""(.+)"", ""(.+)"", ""(.+)"", ""(.+)"", ""(.+)"", ""(.+)"", ""(.+)"", ""(.+)"", check Discontinued")]
        public void WhenIFillFieldsWithValues(string productName, string category, string supplier, string unitPrice, string quantity, string unitsInStock, string unitsOrder, string reorderLevel)
        {
            Product productCoffee = new Product(productName, category, supplier, unitPrice, quantity, unitsInStock, unitsOrder, reorderLevel, true);
            new CreateProductPage(driver).FillFieldsAndCreate(productCoffee);
        }

        [Then(@"the product ""(.*)"" must be created")]
        public void ThenTheProductMustBeCreated(string productName)
        {
            Assert.IsTrue(new AllProductsPage(driver).GetLinkProductSelector(productName));
        }

        [AfterScenario]
        public void CloseDriver()
        {
            driver.Close();
            driver.Quit();
        }

    }
}
