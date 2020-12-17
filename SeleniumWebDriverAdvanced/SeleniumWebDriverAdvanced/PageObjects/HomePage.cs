using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumWebDriverAdvanced.PageObjects
{
    class HomePage
    {
        private IWebDriver driver;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private IWebElement linkAllProducts => driver.FindElement(By.XPath("(//a[contains(@href,'/Product')])[2]"));
        private IWebElement linkLogOut => driver.FindElement(By.LinkText("Logout"));

        public AllProductsPage ClickAllProducts()
        {
            new Actions(driver).MoveToElement(linkAllProducts).Click(linkAllProducts).Build().Perform();
            return new AllProductsPage(driver);
        }

        public void LogOut()
        {
            new Actions(driver).MoveToElement(linkLogOut).Click(linkLogOut).Build().Perform();
        }

        public By GetLogOutSelector()
        {
            return By.LinkText("Logout");
        }


    }
}
