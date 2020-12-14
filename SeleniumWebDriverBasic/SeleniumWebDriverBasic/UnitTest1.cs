using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumWebDriverBasic
{
    public class Tests
    {
        private static IWebDriver driver;
        
        // �������� ������� �������� �� ��������. �����.
        public static bool IsElementPresent(By locator) 
        {
            try
            {
                return driver.FindElement(locator).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
                
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:5000");
            driver.Manage().Window.Maximize();
            
        }

        [Test]
        public void LoginTest()
        {
            // ���������� ����� �����������.
            driver.FindElement(By.Id("Name")).SendKeys("user");
            driver.FindElement(By.Id("Password")).SendKeys("user");
            driver.FindElement(By.CssSelector(".btn")).Click();

            // �������� ������� �������� "Logout" �� ��������.
            bool ElementLogout = IsElementPresent(By.XPath("//header//*[@href='/Account/Logout']"));
            Assert.IsTrue(ElementLogout);
            
        }

        [Test]
        public void AddNewProduct()
        {
            // �����������.
            driver.FindElement(By.Id("Name")).SendKeys("user");
            driver.FindElement(By.Id("Password")).SendKeys("user");
            driver.FindElement(By.CssSelector(".btn")).Click();

            // ������� �� ������ "All Products".
            driver.FindElement(By.XPath("(//a[contains(@href,'/Product')])[2]")).Click();

            // �������� ������� �������� "Create new" �� ��������. 
            bool ButtonCreate = IsElementPresent(By.XPath("//a[contains(text(),'Create new')]"));
            Assert.IsTrue(ButtonCreate);

            // �������� ������ ��������.
            driver.FindElement(By.LinkText("Create new")).Click();
            
            // ���������� �����.
            driver.FindElement(By.Id("ProductName")).SendKeys("Coffee");
            
            IWebElement CategoryElement = driver.FindElement(By.Id("CategoryId"));
            SelectElement categorySelect = new SelectElement(CategoryElement);
            categorySelect.SelectByText("Beverages");

            IWebElement SupplierElement = driver.FindElement(By.Id("SupplierId"));
            SelectElement supplierSelect = new SelectElement(SupplierElement);
            supplierSelect.SelectByText("Exotic Liquids");

            driver.FindElement(By.Id("UnitPrice")).SendKeys("25");
            driver.FindElement(By.Id("QuantityPerUnit")).SendKeys("5 boxes x 10 bags");
            driver.FindElement(By.Id("UnitsInStock")).SendKeys("32");
            driver.FindElement(By.Id("UnitsOnOrder")).SendKeys("3");
            driver.FindElement(By.Id("ReorderLevel")).SendKeys("5");
            driver.FindElement(By.Id("Discontinued")).Click();

            // ������� ������ "���������". �������� ������.
            driver.FindElement(By.CssSelector(".btn")).Click();

            // �������� �������� ����� ����� �������� ��������. ���������� �������� "������ "���������".
            bool ButtonSend = IsElementPresent(By.XPath("//input[@type='submit']"));
            Assert.IsFalse(ButtonSend);
        }

        [Test]
        public void OpenProduct()
        {
            
            // ����������� � ������� �� ������ "All Products".
            driver.FindElement(By.Id("Name")).SendKeys("user");
            driver.FindElement(By.Id("Password")).SendKeys("user");
            driver.FindElement(By.CssSelector(".btn")).Click();
            driver.FindElement(By.XPath("(//a[contains(@href,'/Product')])[2]")).Click();

            // �������� ������� ���������� �������� "Coffee". ���� ���, �������.
            bool CoffeElementPresent = IsElementPresent(By.LinkText("Coffee"));
            if (!CoffeElementPresent)
            {
                // �������� ������ ������� "Coffee"
                driver.FindElement(By.LinkText("Create new")).Click();
                // ���������� �����.
                driver.FindElement(By.Id("ProductName")).SendKeys("Coffee");
                IWebElement CategoryElement = driver.FindElement(By.Id("CategoryId"));
                SelectElement categorySelect = new SelectElement(CategoryElement);
                categorySelect.SelectByText("Beverages");
                IWebElement SupplierElement = driver.FindElement(By.Id("SupplierId"));
                SelectElement supplierSelect = new SelectElement(SupplierElement);
                supplierSelect.SelectByText("Exotic Liquids");
                driver.FindElement(By.Id("UnitPrice")).SendKeys("25");
                driver.FindElement(By.Id("QuantityPerUnit")).SendKeys("5 boxes x 10 bags");
                driver.FindElement(By.Id("UnitsInStock")).SendKeys("32");
                driver.FindElement(By.Id("UnitsOnOrder")).SendKeys("3");
                driver.FindElement(By.Id("ReorderLevel")).SendKeys("5");
                driver.FindElement(By.Id("Discontinued")).Click();
                // �������� ������ �����.
                driver.FindElement(By.CssSelector(".btn")).Click();
            }

            // ����� ��������.
            driver.FindElement(By.LinkText("Coffee")).Click();
            
            // �������� ������������� �����.
            Assert.IsNotEmpty(driver.FindElement(By.Id("ProductId")).GetAttribute("value"));
            Assert.AreEqual("Coffee", driver.FindElement(By.Id("ProductName")).GetAttribute("value"));
            Assert.AreEqual("1", driver.FindElement(By.Id("CategoryId")).GetAttribute("value"));
            Assert.AreEqual("1", driver.FindElement(By.Id("SupplierId")).GetAttribute("value"));
            Assert.AreEqual("25,0000", driver.FindElement(By.Id("UnitPrice")).GetAttribute("value"));
            Assert.AreEqual("5 boxes x 10 bags", driver.FindElement(By.Id("QuantityPerUnit")).GetAttribute("value"));
            Assert.AreEqual("32", driver.FindElement(By.Id("UnitsInStock")).GetAttribute("value"));
            Assert.AreEqual("3", driver.FindElement(By.Id("UnitsOnOrder")).GetAttribute("value"));
            Assert.AreEqual("5", driver.FindElement(By.Id("ReorderLevel")).GetAttribute("value"));
            Assert.IsTrue(driver.FindElement(By.Id("Discontinued")).Enabled);

        }

        [Test]
        public void DeleteProductTest()
        {

            // ����������� � ������� �� ������ "All Products".
            driver.FindElement(By.Id("Name")).SendKeys("user");
            driver.FindElement(By.Id("Password")).SendKeys("user");
            driver.FindElement(By.CssSelector(".btn")).Click();
            driver.FindElement(By.XPath("(//a[contains(@href,'/Product')])[2]")).Click();

            bool CoffeElementPresent = IsElementPresent(By.LinkText("Coffee"));
            if (CoffeElementPresent == false) 
            {
                // �������� ������ ������� "Coffee"
                driver.FindElement(By.LinkText("Create new")).Click();
                // ���������� �����.
                driver.FindElement(By.Id("ProductName")).SendKeys("Coffee");
                IWebElement CategoryElement = driver.FindElement(By.Id("CategoryId"));
                SelectElement categorySelect = new SelectElement(CategoryElement);
                categorySelect.SelectByText("Beverages");
                IWebElement SupplierElement = driver.FindElement(By.Id("SupplierId"));
                SelectElement supplierSelect = new SelectElement(SupplierElement);
                supplierSelect.SelectByText("Exotic Liquids");
                driver.FindElement(By.Id("UnitPrice")).SendKeys("25");
                driver.FindElement(By.Id("QuantityPerUnit")).SendKeys("5 boxes x 10 bags");
                driver.FindElement(By.Id("UnitsInStock")).SendKeys("32");
                driver.FindElement(By.Id("UnitsOnOrder")).SendKeys("3");
                driver.FindElement(By.Id("ReorderLevel")).SendKeys("5");
                driver.FindElement(By.Id("Discontinued")).Click();
                // �������� ������ �����.
                driver.FindElement(By.CssSelector(".btn")).Click();
            }

            // ��� ��������� �� ProductID ��� ������� ���������� ��������� "Coffee".
            string StrProductIDBefore = driver.FindElement(By.XPath("(//*[a='Coffee']/preceding-sibling::td)[1]")).Text;
            int ProductIdBefore = int.Parse(StrProductIDBefore);

            // �������� ������� �������� "Remove" ��� �������� "Coffee"
            bool RemoveCoffeElement = IsElementPresent(By.XPath("(//*[a='Coffee']/following-sibling::*[a='Remove']/a)[1]"));
            Assert.IsTrue(RemoveCoffeElement);
            
            // ������� ������� �������. 
            driver.FindElement(By.XPath("(//*[a='Coffee']/following-sibling::*[a='Remove']/a)[1]")).Click();
            // "Ok" ��� ����������� ����e���������.
            driver.SwitchTo().Alert().Accept();

            driver.Navigate().Refresh();
            driver.Navigate().Refresh();
            // �������� ������� �������� "Coffee" �� ��������.

            // ���� �������� "Coffee" ���������, �� �������� ��������� �� ProductID.
            bool CoffeElementAfter = IsElementPresent(By.LinkText("Coffee"));
            if (CoffeElementAfter)
            {
                string StrProductIDAfter = driver.FindElement(By.XPath("(//*[a='Coffee']/preceding-sibling::td)[1]")).Text;
                int ProductIdAfter = int.Parse(StrProductIDAfter);
                Assert.AreNotEqual(ProductIdAfter, ProductIdBefore);
            }
            else 
            {
                // �������� �� ���������� �������� "Coffee"
                Assert.IsFalse(CoffeElementAfter);
            }
            

        }

        [Test]
        public void LogOutTest()
        {
            // ����������� � ������� �� ������ "All Products".
            driver.FindElement(By.Id("Name")).SendKeys("user");
            driver.FindElement(By.Id("Password")).SendKeys("user");
            driver.FindElement(By.CssSelector(".btn")).Click();

            // �������� ������� �������� "Logout" �� ��������.
            bool ElementLogout = IsElementPresent(By.XPath("//header//*[@href='/Account/Logout']"));
            Assert.IsTrue(ElementLogout);

            // Logout.
            driver.FindElement(By.LinkText("Logout")).Click();

            // �������� ������� ��������� ����� "Name" � "Password" �� ��������. ��������� �������� �����������.
            bool ElementLogInName = IsElementPresent(By.Id("Name"));
            bool ElementLogInPassword = IsElementPresent(By.Id("Password"));
            Assert.IsTrue(ElementLogInName);
            Assert.IsTrue(ElementLogInPassword);
        }



            [TearDown]
        public void CloseDriver()
        {
            driver.Close();
            driver.Quit();

        }

    }
}