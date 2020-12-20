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
        public static AllProductsPage AutorizationAndAllProducts(User user, IWebDriver driver)
        {
            LoginPage loginPage = new LoginPage(driver);
            HomePage homePage = new HomePage(driver);
            homePage = loginPage.Autorization(user);
            AllProductsPage allProductsPage = new AllProductsPage(driver);
            return homePage.ClickAllProducts();
        }

        public static IWebElement AddProduct(Product product, IWebDriver driver)
        {
            AllProductsPage allProductsPage = new AllProductsPage(driver);
            CreateProductPage createProductPage = new CreateProductPage(driver);
            createProductPage = allProductsPage.CreateProduct();
            IWebElement submitButtonSelector = createProductPage.GetSubmitButtonSelector();
            createProductPage.FillFieldsAndCreate(product);
            return submitButtonSelector;
        }

        public static List<string> OpenProduct(Product product, IWebDriver driver)
        {
            AllProductsPage allProductsPage = new AllProductsPage(driver);
            OpenProductPage openProductPage = new OpenProductPage(driver);
            openProductPage = allProductsPage.OpenProduct(product);
            List<string> Attributes =  openProductPage.GetAttributeFields();
            openProductPage.CloseForm(product);
            return Attributes;
        }

        public static LoginPage LogOut(IWebDriver driver)
        {
            LoginPage loginPage = new LoginPage(driver);
           
            HomePage homePage = new HomePage(driver);
            
            homePage.LogOut();

            return loginPage;

        }

    }
}
