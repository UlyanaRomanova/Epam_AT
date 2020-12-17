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
        private IWebElement linkTestProduct => driver.FindElement(By.LinkText("Coffee"));
        private IWebElement linkRemoveTestProduct => driver.FindElement(By.XPath("(//*[a='Coffee']/following-sibling::*[a='Remove']/a)[1]"));

        public CreateProductPage CreateProduct()
        {
            new Actions(driver).MoveToElement(buttonCreateNew).Click(buttonCreateNew).Build().Perform();
            return new CreateProductPage(driver);
        }

        public OpenProductPage OpenProduct()
        {
            new Actions(driver).MoveToElement(linkTestProduct).Click(linkTestProduct).Build().Perform();
            return new OpenProductPage(driver);
        }

        public void RemoveTestProduct()
        {
            new Actions(driver).MoveToElement(linkRemoveTestProduct).Click(linkRemoveTestProduct).Build().Perform();
            driver.SwitchTo().Alert().Accept();
            driver.Navigate().Refresh();
            driver.Navigate().Refresh();
        }

        public By GetRemoveSelector()
        {
            return (By.XPath("(//*[a='Coffee']/following-sibling::*[a='Remove']/a)[1]"));
        }

        public string GetIdElement()
        {
            return driver.FindElement(By.XPath("(//*[a='Coffee']/preceding-sibling::td)[1]")).Text;
        }

        public By GetElementSelector()
        {
            return (By.LinkText("Coffee"));
        }
    }
}
