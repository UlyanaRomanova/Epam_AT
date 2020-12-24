using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using WebDriverFramework.BusinessObjects;

namespace WebDriverFramework.PageObjects
{
    class CreateProductPage : ProductEditingPage
    {
        private IWebDriver driver;

        public CreateProductPage(IWebDriver driver) : base (driver)
        {
            this.driver = driver;
        }

        public void FillFieldsAndCreate(Product productCoffee)
        {
            new Actions(driver).SendKeys(productNameInput, productCoffee.productName).Build().Perform();
            new Actions(driver).SendKeys(categoryInput, productCoffee.Category).Build().Perform();
            new Actions(driver).SendKeys(supplierInput, productCoffee.Supplier).Build().Perform();
            new Actions(driver).SendKeys(unitPriceInput, productCoffee.UnitPrice).Build().Perform();
            new Actions(driver).SendKeys(quantityPerUnitInput, productCoffee.Quantity).Build().Perform();
            new Actions(driver).SendKeys(unitsInStockInput, productCoffee.UnitsInStock).Build().Perform();
            new Actions(driver).SendKeys(unitsOnOrderInput, productCoffee.UnitsOrder).Build().Perform();
            new Actions(driver).SendKeys(reorderLevelInput, productCoffee.ReorderLevel).Build().Perform();
            
            if (productCoffee.discontinued == true) 
            { 
                new Actions(driver).Click(discontinuedCheck).Build().Perform(); 
            }

            new Actions(driver).Click(submitButton).Build().Perform();
        }

    }
}
