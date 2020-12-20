using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;
using WebDriverFramework.BusinessObjects;

namespace WebDriverFramework.PageObjects
{
    class AllProductsPage
    {
        private IWebDriver driver;

        public AllProductsPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private IWebElement buttonCreateNew => driver.FindElement(By.LinkText("Create new"));
        private IWebElement linkTestProduct(string productName) => driver.FindElement(By.LinkText($"{productName}"));
        private IWebElement linkRemoveTestProduct(string productName) => driver.FindElement(By.XPath($"(//*[a='{productName}']/following-sibling::*[a='Remove']/a)[1]"));
        private IWebElement linkLogOut => driver.FindElement(By.LinkText("Logout"));

        public CreateProductPage CreateProduct()
        {
            new Actions(driver).MoveToElement(buttonCreateNew).Click(buttonCreateNew).Build().Perform();
            return new CreateProductPage(driver);
        }

        public OpenProductPage OpenProduct(Product product)
        {
            new Actions(driver).MoveToElement(linkTestProduct(product.productName)).Click().Build().Perform();
            return new OpenProductPage(driver);
        }

        public void RemoveTestProduct(Product product)
        {
            new Actions(driver).MoveToElement(linkRemoveTestProduct(product.productName)).Click().Build().Perform();
            driver.SwitchTo().Alert().Accept();
            driver.Navigate().Refresh();
            driver.Navigate().Refresh();
        }

        public IWebElement GetRemoveSelector(Product product)
        {
            return linkRemoveTestProduct(product.productName);
        }

        public string GetIdElement(Product product)
        {
            return driver.FindElement(By.XPath($"(//*[a='{product.productName}']/preceding-sibling::td)[1]")).Text;
        }

        public IWebElement GetElementSelector(Product product)
        {
            return driver.FindElement(By.LinkText($"{product.productName}"));
        }

        public IWebElement GetLogOutSelector()
        {
            return linkLogOut;
        }

        public void LogOut()
        {
            new Actions(driver).MoveToElement(linkLogOut).Click(linkLogOut).Build().Perform();
        }
    }
}
