using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Support.UI;
using System.Collections;

namespace WebDriverFramework.PageObjects
{
    abstract class ProductEditingPage
    {
        private IWebDriver driver;

        public ProductEditingPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement idProduct => driver.FindElement(By.Id("ProductId"));
        public IWebElement productNameInput => driver.FindElement(By.Id("ProductName"));
        public IWebElement categoryInput => driver.FindElement(By.Id("CategoryId"));
        public IWebElement supplierInput => driver.FindElement(By.Id("SupplierId"));
        public IWebElement unitPriceInput => driver.FindElement(By.Id("UnitPrice"));
        public IWebElement quantityPerUnitInput => driver.FindElement(By.Id("QuantityPerUnit"));
        public IWebElement unitsInStockInput => driver.FindElement(By.Id("UnitsInStock"));
        public IWebElement unitsOnOrderInput => driver.FindElement(By.Id("UnitsOnOrder"));
        public IWebElement reorderLevelInput => driver.FindElement(By.Id("ReorderLevel"));
        public IWebElement discontinuedCheck => driver.FindElement(By.Id("Discontinued"));
        public IWebElement submitButton => driver.FindElement(By.XPath("//input[@type='submit']"));

             
     }   
}
