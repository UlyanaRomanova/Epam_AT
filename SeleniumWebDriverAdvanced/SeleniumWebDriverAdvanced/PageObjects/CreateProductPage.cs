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

        public void FillFieldsAndCreate()
        {
            new Actions(driver).SendKeys(productNameInput,"Coffee").Build().Perform();
            new Actions(driver).SendKeys(categoryInput, "Beverages").Build().Perform();
            new Actions(driver).SendKeys(supplierInput, "Exotic Liquids").Build().Perform();
            new Actions(driver).SendKeys(unitPriceInput, "25").Build().Perform();
            new Actions(driver).SendKeys(quantityPerUnitInput, "5 boxes x 10 bags").Build().Perform();
            new Actions(driver).SendKeys(unitsInStockInput, "32").Build().Perform();
            new Actions(driver).SendKeys(unitsOnOrderInput, "3").Build().Perform();
            new Actions(driver).SendKeys(reorderLevelInput, "5").Build().Perform();
            new Actions(driver).Click(discontinuedCheck).Build().Perform();
            new Actions(driver).Click(submitButton).Build().Perform();
        }

        public By GetSubmitButtonSelector()
        {
            return By.XPath("//input[@type='submit']");
        }
    }
}
