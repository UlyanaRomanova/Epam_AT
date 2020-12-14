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
        
        // Проверка наличия элемента на странице. Метод.
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
            // Заполнение полей авторизации.
            driver.FindElement(By.Id("Name")).SendKeys("user");
            driver.FindElement(By.Id("Password")).SendKeys("user");
            driver.FindElement(By.CssSelector(".btn")).Click();

            // Проверка наличия элемента "Logout" на странице.
            bool ElementLogout = IsElementPresent(By.XPath("//header//*[@href='/Account/Logout']"));
            Assert.IsTrue(ElementLogout);
            
        }

        [Test]
        public void AddNewProduct()
        {
            // Авторизация.
            driver.FindElement(By.Id("Name")).SendKeys("user");
            driver.FindElement(By.Id("Password")).SendKeys("user");
            driver.FindElement(By.CssSelector(".btn")).Click();

            // Переход по ссылке "All Products".
            driver.FindElement(By.XPath("(//a[contains(@href,'/Product')])[2]")).Click();

            // Проверка наличия элемента "Create new" на странице. 
            bool ButtonCreate = IsElementPresent(By.XPath("//a[contains(text(),'Create new')]"));
            Assert.IsTrue(ButtonCreate);

            // Создание нового продукта.
            driver.FindElement(By.LinkText("Create new")).Click();
            
            // Заполнение полей.
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

            // Нажатие кнопки "отправить". Отправка данных.
            driver.FindElement(By.CssSelector(".btn")).Click();

            // Проверка закрытия формы после создания продукта. Отсутствие элемента "кнопка "отправить".
            bool ButtonSend = IsElementPresent(By.XPath("//input[@type='submit']"));
            Assert.IsFalse(ButtonSend);
        }

        [Test]
        public void OpenProduct()
        {
            
            // Авторизация и переход по ссылке "All Products".
            driver.FindElement(By.Id("Name")).SendKeys("user");
            driver.FindElement(By.Id("Password")).SendKeys("user");
            driver.FindElement(By.CssSelector(".btn")).Click();
            driver.FindElement(By.XPath("(//a[contains(@href,'/Product')])[2]")).Click();

            // Проверка наличия созданного продукта "Coffee". Если нет, создать.
            bool CoffeElementPresent = IsElementPresent(By.LinkText("Coffee"));
            if (!CoffeElementPresent)
            {
                // Создание нового объекта "Coffee"
                driver.FindElement(By.LinkText("Create new")).Click();
                // Заполнение полей.
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
                // Отправка данных формы.
                driver.FindElement(By.CssSelector(".btn")).Click();
            }

            // Выбор продукта.
            driver.FindElement(By.LinkText("Coffee")).Click();
            
            // Проверки заполненности полей.
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

            // Авторизация и переход по ссылке "All Products".
            driver.FindElement(By.Id("Name")).SendKeys("user");
            driver.FindElement(By.Id("Password")).SendKeys("user");
            driver.FindElement(By.CssSelector(".btn")).Click();
            driver.FindElement(By.XPath("(//a[contains(@href,'/Product')])[2]")).Click();

            bool CoffeElementPresent = IsElementPresent(By.LinkText("Coffee"));
            if (CoffeElementPresent == false) 
            {
                // Создание нового объекта "Coffee"
                driver.FindElement(By.LinkText("Create new")).Click();
                // Заполнение полей.
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
                // Отправка данных формы.
                driver.FindElement(By.CssSelector(".btn")).Click();
            }

            // Для сравнения по ProductID при наличии нескольких элементов "Coffee".
            string StrProductIDBefore = driver.FindElement(By.XPath("(//*[a='Coffee']/preceding-sibling::td)[1]")).Text;
            int ProductIdBefore = int.Parse(StrProductIDBefore);

            // Проверка наличия элемента "Remove" для элемента "Coffee"
            bool RemoveCoffeElement = IsElementPresent(By.XPath("(//*[a='Coffee']/following-sibling::*[a='Remove']/a)[1]"));
            Assert.IsTrue(RemoveCoffeElement);
            
            // Выбрать Удалить элемент. 
            driver.FindElement(By.XPath("(//*[a='Coffee']/following-sibling::*[a='Remove']/a)[1]")).Click();
            // "Ok" при всплывающем предeпреждении.
            driver.SwitchTo().Alert().Accept();

            driver.Navigate().Refresh();
            driver.Navigate().Refresh();
            // Проверка наличия продукта "Coffee" на странице.

            // Если эементов "Coffee" несколько, то сравнить удаленный по ProductID.
            bool CoffeElementAfter = IsElementPresent(By.LinkText("Coffee"));
            if (CoffeElementAfter)
            {
                string StrProductIDAfter = driver.FindElement(By.XPath("(//*[a='Coffee']/preceding-sibling::td)[1]")).Text;
                int ProductIdAfter = int.Parse(StrProductIDAfter);
                Assert.AreNotEqual(ProductIdAfter, ProductIdBefore);
            }
            else 
            {
                // Проверка на отсутствие элемента "Coffee"
                Assert.IsFalse(CoffeElementAfter);
            }
            

        }

        [Test]
        public void LogOutTest()
        {
            // Авторизация и переход по ссылке "All Products".
            driver.FindElement(By.Id("Name")).SendKeys("user");
            driver.FindElement(By.Id("Password")).SendKeys("user");
            driver.FindElement(By.CssSelector(".btn")).Click();

            // Проверка наличия элемента "Logout" на странице.
            bool ElementLogout = IsElementPresent(By.XPath("//header//*[@href='/Account/Logout']"));
            Assert.IsTrue(ElementLogout);

            // Logout.
            driver.FindElement(By.LinkText("Logout")).Click();

            // Проверка наличия элементов ввода "Name" и "Password" на странице. Начальная страница авторизации.
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