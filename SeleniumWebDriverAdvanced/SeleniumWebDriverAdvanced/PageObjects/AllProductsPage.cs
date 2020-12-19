using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumWebDriverAdvanced.PageObjects
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

        public CreateProductPage CreateProduct()
        {
            new Actions(driver).MoveToElement(buttonCreateNew).Click(buttonCreateNew).Build().Perform();
            return new CreateProductPage(driver);
        }

        public OpenProductPage OpenProduct(string productName)
        {
            new Actions(driver).MoveToElement(linkTestProduct(productName)).Click().Build().Perform();
            return new OpenProductPage(driver);
        }

        public void RemoveTestProduct(string productName)
        {
            new Actions(driver).MoveToElement(linkRemoveTestProduct(productName)).Click().Build().Perform();
            driver.SwitchTo().Alert().Accept();
            driver.Navigate().Refresh();
            driver.Navigate().Refresh();
        }

        public IWebElement GetRemoveSelector(string productName)
        {
            return linkRemoveTestProduct(productName);
        }

        public string GetIdElement(string productName)
        {
            return driver.FindElement(By.XPath($"(//*[a='{productName}']/preceding-sibling::td)[1]")).Text;
        }

        public IWebElement GetElementSelector(string productName)
        {
            return driver.FindElement(By.LinkText($"{productName}"));
        }
    }
}
