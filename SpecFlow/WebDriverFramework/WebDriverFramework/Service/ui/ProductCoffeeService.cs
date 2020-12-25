using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using WebDriverFramework.BusinessObjects;
using WebDriverFramework.PageObjects;

namespace WebDriverFramework.Service.ui
{
    class ProductCoffeeService
    {
        public static HomePage Autorization(User user, IWebDriver driver)
        {
            LoginPage loginPage = new LoginPage(driver);
            HomePage homePage = new HomePage(driver);
            homePage = loginPage.Autorization(user);
            return homePage;
        }

        public static AllProductsPage ClickAllProducts(IWebDriver driver)
        {
            HomePage homePage = new HomePage(driver);
            return homePage.ClickAllProducts();
        }

        public static AllProductsPage AddProduct(Product product, IWebDriver driver)
        {
            AllProductsPage allProductsPage = new AllProductsPage(driver);
            CreateProductPage createProductPage = allProductsPage.CreateProduct();
            createProductPage.FillFieldsAndCreate(product);
            return allProductsPage;
        }

        public static OpenProductPage OpenProduct(Product product, IWebDriver driver)
        {
            AllProductsPage allProductsPage = new AllProductsPage(driver);
            OpenProductPage openProductPage = allProductsPage.OpenProduct(product);
            return openProductPage;
        }

        public static LoginPage LogOut(IWebDriver driver)
        {
            LoginPage loginPage = new LoginPage(driver);
            HomePage homePage = new HomePage(driver);
            homePage.LogOut();
            return loginPage;
        }

        public static AllProductsPage DeleteProduct(Product product, IWebDriver driver)
        {
            AllProductsPage allProductsPage = new AllProductsPage(driver);
            allProductsPage.RemoveTestProduct(product);
            return allProductsPage;
        }

    }
}
