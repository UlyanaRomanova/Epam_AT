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

        [Test, Order(1)]
        public void LoginTest()
        {
            HomePage homePage = ProductCoffeeService.Autorization(user, driver);
            AllProductsPage allProductsPage = ProductCoffeeService.ClickAllProducts(driver);
            Assert.IsTrue(allProductsPage.GetLogOutSelector());
        }

        [Test, Order(2)]
        public void AddNewProduct()
        {
            AllProductsPage allProductsPage = ProductCoffeeService.AddProduct(productCoffee, driver);
            Assert.IsTrue(allProductsPage.GetCreateNewSelector());
        }

        [Test, Order(3)]
        public void OpenProduct()
        {
            OpenProductPage openProductPage = ProductCoffeeService.OpenProduct(productCoffee, driver);
            Product openTestProduct = openProductPage.GetAttributeFields();

            Assert.Multiple(() =>
            {
                Assert.IsNotEmpty(openTestProduct.productId);
                Assert.AreEqual(productCoffee.productName, openTestProduct.productName);
                Assert.AreEqual(productCoffee.Category, openTestProduct.Category);
                Assert.AreEqual(productCoffee.Supplier, openTestProduct.Supplier);
                Assert.AreEqual(productCoffee.UnitPrice, openTestProduct.UnitPrice);
                Assert.AreEqual(productCoffee.Quantity, openTestProduct.Quantity);
                Assert.AreEqual(productCoffee.UnitsInStock, openTestProduct.UnitsInStock);
                Assert.AreEqual(productCoffee.UnitsOrder, openTestProduct.UnitsOrder);
                Assert.AreEqual(productCoffee.ReorderLevel, openTestProduct.ReorderLevel);
                Assert.IsTrue(openTestProduct.discontinued);
            });
        }

        [Test, Order(4)]
        public void DeleteProductTest()
        {
            AllProductsPage allProductsPage = ProductCoffeeService.ClickAllProducts(driver);
            Assert.IsTrue(allProductsPage.GetRemoveSelector(productCoffee));

            int StrProductIDBefore = allProductsPage.GetIdElementBefore(productCoffee);
            allProductsPage = ProductCoffeeService.DeleteProduct(productCoffee, driver);
            int StrProductIDAfter = allProductsPage.GetIdElementAfter(productCoffee);

            if (allProductsPage.GetIdElementAfter(productCoffee) != 0)
            {
                Assert.AreNotEqual(StrProductIDAfter, StrProductIDBefore);
            }
            else
            {
                Assert.IsFalse(allProductsPage.GetRemoveSelector(productCoffee));
            }

        }

        [Test, Order(5)]
        public void LogOutTest()
        {
            LoginPage loginPage = ProductCoffeeService.LogOut(driver);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(loginPage.GetElementLogInName());
                Assert.IsTrue(loginPage.GetElementLogInPassword());
            });

        }

    }
}