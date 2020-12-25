using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using WebDriverFramework.BusinessObjects;

namespace WebDriverFramework.Tests
{
    public class BaseTest
    {
        protected static IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:5000");
            driver.Manage().Window.Maximize();
           
        }

        [OneTimeTearDown]
        public void CloseDriver()
        {
            driver.Close();
            driver.Quit();
        }

    }
}
