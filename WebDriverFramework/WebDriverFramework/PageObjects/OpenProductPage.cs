using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;
using WebDriverFramework.BusinessObjects;

namespace WebDriverFramework.PageObjects
{
    class OpenProductPage : ProductEditingPage
    {
        private IWebDriver driver;
        public OpenProductPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }
        public Product GetAttributeFields()
        {
            string textId = idProduct.GetAttribute("value");
            string textproductName = productNameInput.GetAttribute("value");
            string textCategory = categoryInputGetValue.Text;
            string textSupplier = supplierInputGetValue.Text;
            string textUnitPrice = Convert.ToString(Convert.ToInt64(Convert.ToDecimal(unitPriceInput.GetAttribute("value"))));
            string textQuantity = quantityPerUnitInput.GetAttribute("value");
            string textUnitsInStock = unitsInStockInput.GetAttribute("value");
            string textUnitsOrder = unitsOnOrderInput.GetAttribute("value");
            string textReorderLevel = reorderLevelInput.GetAttribute("value");

            bool enableDiscontinued = discontinuedCheck.Enabled;
            string strEnableDiscontinued = Convert.ToString(enableDiscontinued);

            Product openTestProduct = new Product(textproductName, textCategory, textSupplier, textUnitPrice, textQuantity,
                textUnitsInStock, textUnitsOrder, textReorderLevel, enableDiscontinued); ;

            return openTestProduct;
        }

        public void CloseForm(Product product) 
        {
            new Actions(driver).DoubleClick(unitPriceInput).SendKeys(Keys.Clear).Build().Perform();
            new Actions(driver).SendKeys(unitPriceInput, product.UnitPrice).Build().Perform();
            new Actions(driver).Click(submitButton).Build().Perform();
        }

    }
}
