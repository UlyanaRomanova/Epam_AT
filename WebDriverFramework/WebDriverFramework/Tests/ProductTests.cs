using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverFramework.PageObjects;
using System;
using WebDriverFramework.BusinessObjects;
using WebDriverFramework.Service.ui;
using System.Collections.Generic;

namespace WebDriverFramework.Tests
{
    [TestFixture]
    public class ProductTests : BaseTest
    {
        User user = new User("user","user");
        Product productCoffee = new Product("Coffee", "Beverages", "Exotic Liquids", "25", "5 boxes x 10 bags", "32", "3", "5", true);

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


        [Test, Order(1)]
        public void LoginTest()
        {
            AllProductsPage allProductsPage = ProductCoffeeService.AutorizationAndAllProducts(user, driver);
            bool ElementLogout = IsElementPresent(allProductsPage.GetLogOutSelector());
            Assert.IsTrue(ElementLogout);
        }

        [Test, Order(2)]
        public void AddNewProduct()
        {
            IWebElement submitButtonSelector = ProductCoffeeService.AddProduct(productCoffee, driver);
            bool ButtonSend = IsElementPresent(submitButtonSelector);
            Assert.IsFalse(ButtonSend);
        }

        [Test, Order(3)]
        public void OpenProduct()
        {
            List<string> Attributes = ProductCoffeeService.OpenProduct(productCoffee, driver);

            Assert.Multiple(() =>
            {
                Assert.IsNotEmpty(Attributes[0]);
                Assert.AreEqual("Coffee", Attributes[1]);
                Assert.AreEqual("1", Attributes[2]);
                Assert.AreEqual("1", Attributes[3]);
                Assert.AreEqual("25,0000", Attributes[4]);
                Assert.AreEqual("5 boxes x 10 bags", Attributes[5]);
                Assert.AreEqual("32", Attributes[6]);
                Assert.AreEqual("3", Attributes[7]);
                Assert.AreEqual("5", Attributes[8]);
                Assert.IsTrue(Convert.ToBoolean(Attributes[9]));
            });
        }

        [Test, Order(4)]
        public void DeleteProductTest()
        {
            AllProductsPage allProductsPage = new AllProductsPage(driver);

            // Check element "Remove" for element Product
            bool RemoveProductElement = IsElementPresent(allProductsPage.GetRemoveSelector(productCoffee));
            Assert.IsTrue(RemoveProductElement);

            // For comparison by ProductID if there are multiple elements Product.
            string StrProductIDBefore = allProductsPage.GetIdElement(productCoffee);
            int ProductIdBefore = int.Parse(StrProductIDBefore);

            IWebElement linkProductSelector = allProductsPage.GetElementSelector(productCoffee);

            allProductsPage.RemoveTestProduct(productCoffee);

            // Check Product is present.
            bool ProductElementAfter = IsElementPresent(linkProductSelector);
            if (ProductElementAfter)
            {
                string StrProductIDAfter = allProductsPage.GetIdElement(productCoffee);
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
            LoginPage loginPage = ProductCoffeeService.LogOut(driver);

            bool ElementLogInName = IsElementPresent(loginPage.GetNameSelector());
            bool ElementLogInPassword = IsElementPresent(loginPage.GetNameSelector());
            Assert.IsTrue(ElementLogInName);
            Assert.IsTrue(ElementLogInPassword);
        }


    }
}