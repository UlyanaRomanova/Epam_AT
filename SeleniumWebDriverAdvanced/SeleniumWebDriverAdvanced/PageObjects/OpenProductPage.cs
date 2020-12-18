using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumWebDriverAdvanced.PageObjects
{
    class OpenProductPage : ProductEditingPage
    {
        private IWebDriver driver;
        public OpenProductPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }
        public List<string> GetAttributeFields()
        {
            string textId = idProduct.GetAttribute("value");
            string textproductName = productNameInput.GetAttribute("value");
            string textCategory = categoryInput.GetAttribute("value");
            string textSupplier = supplierInput.GetAttribute("value");
            string textUnitPrice = unitPriceInput.GetAttribute("value");
            string textQuantity = quantityPerUnitInput.GetAttribute("value");
            string textUnitsInStock = unitsInStockInput.GetAttribute("value");
            string textUnitsOrder = unitsOnOrderInput.GetAttribute("value");
            string textReorderLevel = reorderLevelInput.GetAttribute("value");

            bool enableDiscontinued = discontinuedCheck.Enabled;
            string strEnableDiscontinued = Convert.ToString(enableDiscontinued);

            List<string> Attributes = new List<string>() {textId, textproductName, textCategory, textSupplier,
                textUnitPrice, textQuantity, textUnitsInStock, textUnitsOrder, textReorderLevel, strEnableDiscontinued};

            return Attributes;
        }

        public void CloseForm(string dataUnitPrice) 
        {
            new Actions(driver).DoubleClick(unitPriceInput).SendKeys(Keys.Clear).Build().Perform();
            new Actions(driver).SendKeys(unitPriceInput, dataUnitPrice).Build().Perform();
            new Actions(driver).Click(submitButton).Build().Perform();
        }

    }
}
