using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumWebDriverAdvanced.PageObjects;
using System;

namespace SeleniumWebDriverAdvanced
{
    [TestFixture]
    public class Tests
    {
        private static IWebDriver driver;
        
        private LoginPage loginPage;
        private HomePage homePage;
        private AllProductsPage allProductsPage;
        private CreateProductPage createProductPage;
        private OpenProductPage openProductPage;

        private string[] testDataInput;

        private bool IsElementPresent(IWebElement webElement)
        {
            try
            {
                return webElement.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        }

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:5000");
            driver.Manage().Window.Maximize();
            testDataInput = new string[] { "Coffee", "Beverages", "Exotic Liquids", "25", "5 boxes x 10 bags", "32", "3", "5" };
        }

        [Test, Order(1)]
        public void LoginTest()
        {
            loginPage = new LoginPage(driver);
            homePage = new HomePage(driver);
            homePage = loginPage.Autorization("user", "user");
            bool ElementLogout = IsElementPresent(homePage.GetLogOutSelector());
            Assert.IsTrue(ElementLogout);
        }

        [Test, Order(2)]
        public void AddNewProduct()
        {
            allProductsPage = new AllProductsPage(driver);
            createProductPage = new CreateProductPage(driver);
            allProductsPage = homePage.ClickAllProducts();
            createProductPage = allProductsPage.CreateProduct();
            IWebElement submitButtonSelector = createProductPage.GetSubmitButtonSelector();
            createProductPage.FillFieldsAndCreate(testDataInput);
            
            // Check form close. Button "submit" is not present.
            bool ButtonSend = IsElementPresent(submitButtonSelector);
            Assert.IsFalse(ButtonSend);
        }

        [Test, Order(3)]
        public void OpenProduct()
        {
            allProductsPage = new AllProductsPage(driver);
            openProductPage = new OpenProductPage(driver);
            openProductPage = allProductsPage.OpenProduct(testDataInput[0]);
            openProductPage.GetAttributeFields();

            Assert.IsNotEmpty(openProductPage.GetAttributeFields()[0]);
            Assert.AreEqual("Coffee", openProductPage.GetAttributeFields()[1]);
            Assert.AreEqual("1", openProductPage.GetAttributeFields()[2]);
            Assert.AreEqual("1", openProductPage.GetAttributeFields()[3]);
            Assert.AreEqual("25,0000", openProductPage.GetAttributeFields()[4]);
            Assert.AreEqual("5 boxes x 10 bags", openProductPage.GetAttributeFields()[5]);
            Assert.AreEqual("32", openProductPage.GetAttributeFields()[6]);
            Assert.AreEqual("3", openProductPage.GetAttributeFields()[7]);
            Assert.AreEqual("5", openProductPage.GetAttributeFields()[8]);
            Assert.IsTrue(Convert.ToBoolean(openProductPage.GetAttributeFields()[9]));

            openProductPage.CloseForm(testDataInput[3]);
        }

        [Test, Order(4)]
        public void DeleteProductTest()
        {
            allProductsPage = new AllProductsPage(driver);

            // Check element "Remove" for element Product
            bool RemoveProductElement = IsElementPresent(allProductsPage.GetRemoveSelector(testDataInput[0]));
            Assert.IsTrue(RemoveProductElement);

            // For comparison by ProductID if there are multiple elements Product.
            string StrProductIDBefore = allProductsPage.GetIdElement(testDataInput[0]);
            int ProductIdBefore = int.Parse(StrProductIDBefore);

            IWebElement linkProductSelector = allProductsPage.GetElementSelector(testDataInput[0]);
            
            allProductsPage.RemoveTestProduct(testDataInput[0]);

            // Check Product is present.
            bool ProductElementAfter = IsElementPresent(linkProductSelector);
            if (ProductElementAfter)
            {
                string StrProductIDAfter = allProductsPage.GetIdElement(testDataInput[0]);
                int ProductIdAfter = int.Parse(StrProductIDAfter);
                Assert.AreNotEqual(ProductIdAfter, ProductIdBefore);
            }
            else 
            {
                Assert.IsFalse(ProductElementAfter);
            }

        }

        [Test, Order(5)]
        public void LogOutTest()
        {
            // Check "Logout" is present.
            bool ElementLogout = IsElementPresent(homePage.GetLogOutSelector());
            Assert.IsTrue(ElementLogout);

            homePage.LogOut();
  
            // Check authorization page. 
            bool ElementLogInName = IsElementPresent(loginPage.GetNameSelector());
            bool ElementLogInPassword = IsElementPresent(loginPage.GetNameSelector());
            Assert.IsTrue(ElementLogInName);
            Assert.IsTrue(ElementLogInPassword);
        }

        [OneTimeTearDown]
        public void CloseDriver()
        {
            driver.Close();
            driver.Quit();
        }

    }
}