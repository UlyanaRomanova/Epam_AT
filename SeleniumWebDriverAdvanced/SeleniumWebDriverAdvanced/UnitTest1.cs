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

        private bool IsElementPresent(By locator)
        {
            try
            {
                return driver.FindElement(locator).Displayed;
            }
            catch (NoSuchElementException)
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
            createProductPage.FillFieldsAndCreate();
            
            // Check form close. Button "submit" is not present.
            bool ButtonSend = IsElementPresent(createProductPage.GetSubmitButtonSelector());
            Assert.IsFalse(ButtonSend);
        }

        [Test, Order(3)]
        public void OpenProduct()
        {
            allProductsPage = new AllProductsPage(driver);
            openProductPage = new OpenProductPage(driver);
            //allProductsPage = homePage.ClickAllProducts();
            openProductPage = allProductsPage.OpenProduct();
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

            openProductPage.CloseForm();
        }

        [Test, Order(4)]
        public void DeleteProductTest()
        {
            allProductsPage = new AllProductsPage(driver);

            // Check element "Remove" for element "Coffee"
            bool RemoveCoffeElement = IsElementPresent(allProductsPage.GetRemoveSelector());
            Assert.IsTrue(RemoveCoffeElement);

            // Для сравнения по ProductID при наличии нескольких элементов "Coffee".
            string StrProductIDBefore = allProductsPage.GetIdElement();
            int ProductIdBefore = int.Parse(StrProductIDBefore);

            // Remove.
            allProductsPage.RemoveTestProduct();

            // Check "Coffee" is present.
            bool CoffeElementAfter = IsElementPresent(allProductsPage.GetElementSelector());
            if (CoffeElementAfter)
            {
                string StrProductIDAfter = allProductsPage.GetIdElement();
                int ProductIdAfter = int.Parse(StrProductIDAfter);
                Assert.AreNotEqual(ProductIdAfter, ProductIdBefore);
            }
            else 
            {
                Assert.IsFalse(CoffeElementAfter);
            }

        }

        [Test, Order(5)]
        public void LogOutTest()
        {
            // Check "Logout" is present.
            bool ElementLogout = IsElementPresent(homePage.GetLogOutSelector());
            Assert.IsTrue(ElementLogout);

            // Logout.
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