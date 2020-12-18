using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumWebDriverAdvanced.PageObjects
{
    class CreateProductPage : ProductEditingPage
    {
        private IWebDriver driver;

        public CreateProductPage(IWebDriver driver) : base (driver)
        {
            this.driver = driver;
        }

        public void FillFieldsAndCreate(string[] dataInput)
        {
            new Actions(driver).SendKeys(productNameInput, dataInput[0]).Build().Perform();
            new Actions(driver).SendKeys(categoryInput, dataInput[1]).Build().Perform();
            new Actions(driver).SendKeys(supplierInput, dataInput[2]).Build().Perform();
            new Actions(driver).SendKeys(unitPriceInput, dataInput[3]).Build().Perform();
            new Actions(driver).SendKeys(quantityPerUnitInput, dataInput[4]).Build().Perform();
            new Actions(driver).SendKeys(unitsInStockInput, dataInput[5]).Build().Perform();
            new Actions(driver).SendKeys(unitsOnOrderInput, dataInput[6]).Build().Perform();
            new Actions(driver).SendKeys(reorderLevelInput, dataInput[7]).Build().Perform();
            new Actions(driver).Click(discontinuedCheck).Build().Perform();
            new Actions(driver).Click(submitButton).Build().Perform();
        }

        public IWebElement GetSubmitButtonSelector()
        {
            return (submitButton);
        }
    }
}
