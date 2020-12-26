using System;
using System.Collections.Generic;
using System.Text;

namespace WebDriverFramework.BusinessObjects
{
    class Product
    {
        public Product(string productName, string category, string supplier, string unitPrice, 
            string quantity, string unitsInStock, string unitsOrder, string reorderLevel, 
            bool discontinuedCheck)
        {
            this.productName = productName;
            Category = category;
            Supplier = supplier;
            UnitPrice = unitPrice;
            Quantity = quantity;
            UnitsInStock = unitsInStock;
            UnitsOrder = unitsOrder;
            ReorderLevel = reorderLevel;
            this.discontinued = discontinuedCheck;
        }

        public string productName { get; set; }
        public string Category { get; set; }
        public string Supplier { get; set; }
        public string UnitPrice { get; set; }
        public string Quantity { get; set; }
        public string UnitsInStock { get; set; }
        public string UnitsOrder { get; set; }
        public string ReorderLevel { get; set; }
        public bool discontinued { get; set; }
        public string productId { get; }

    }
}
