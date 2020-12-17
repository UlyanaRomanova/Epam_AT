using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumWebDriverAdvanced.PageObjects
{
    class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private IWebElement UserNameInput => driver.FindElement(By.Id("Name"));
        private IWebElement PasswordInput => driver.FindElement(By.Id("Password"));
        private IWebElement ButtonSend => driver.FindElement(By.CssSelector(".btn"));

        public HomePage Autorization(string name, string password)
        {
            new Actions(driver).SendKeys(UserNameInput, name).Build().Perform();
            new Actions(driver).SendKeys(PasswordInput, password).Build().Perform();
            new Actions(driver).MoveToElement(ButtonSend).Click(ButtonSend).Build().Perform();
            return new HomePage(driver);
        }

        public By GetNameSelector()
        {
            return By.Id("Name");
        }
        public By GetPasswordSelector()
        {
            return By.Id("Password");
        }
    }

}
