using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;
using WebDriverFramework.BusinessObjects;

namespace WebDriverFramework.PageObjects
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

        private bool IsElementPresent(IWebElement webElement)
        {
            try
            {
                return webElement.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        }

        public HomePage Autorization(User user)
        {
            new Actions(driver).SendKeys(UserNameInput, user.UserName).Build().Perform();
            new Actions(driver).SendKeys(PasswordInput, user.UserPassword).Build().Perform();
            new Actions(driver).MoveToElement(ButtonSend).Click(ButtonSend).Build().Perform();
            return new HomePage(driver);
        }

        public bool GetElementLogInName()
        {
            return IsElementPresent(UserNameInput);
        }
        public bool GetElementLogInPassword()
        {
            return IsElementPresent(PasswordInput);
        }

        public void TypeLogInData(User user)
        {
            UserNameInput.SendKeys(user.UserName);
            PasswordInput.SendKeys(user.UserPassword);
        }

        public void ClickButtonSend()
        {
            ButtonSend.Click();
        }

    }

}
