﻿using OpenQA.Selenium;
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

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

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

        public AllProductsPage RemoveTestProduct(Product product)
        {
            new Actions(driver).MoveToElement(linkRemoveTestProduct(product.productName)).Click().Build().Perform();
            driver.SwitchTo().Alert().Accept();
            driver.Navigate().Refresh();
            driver.Navigate().Refresh();
            return new AllProductsPage(driver);
        }

        public bool GetRemoveSelector(Product product)
        {
            return IsElementPresent(By.XPath($"(//*[a='{product.productName}']/following-sibling::*[a='Remove']/a)[1]"));
        }

        public int GetIdElementBefore(Product product)
        {
            int ProductIdBefore = int.Parse(driver.FindElement(By.XPath($"(//*[a='{product.productName}']/preceding-sibling::td)[1]")).Text);
            return ProductIdBefore;
        }

        public int GetIdElementAfter(Product product)
        {
            if (IsElementPresent(By.LinkText($"{product.productName}")))
            {
                int ProductIdAfter = int.Parse(driver.FindElement(By.XPath($"(//*[a='{product.productName}']/preceding-sibling::td)[1]")).Text);
                return ProductIdAfter;
            }
            else return 0;
        }

        public bool GetLogOutSelector()
        {
            return IsElementPresent(By.LinkText("Logout"));
        }

        public bool GetCreateNewSelector()
        {
            return IsElementPresent(By.LinkText("Create new"));
        }

        public void LogOut()
        {
            new Actions(driver).MoveToElement(linkLogOut).Click(linkLogOut).Build().Perform();
        }

        public void ClickButtonCreateProduct()
        {
           buttonCreateNew.Click();
        }

        public bool GetLinkProductSelector(string productName)
        {
            return IsElementPresent(By.LinkText($"{productName}"));
        }
    }
}
