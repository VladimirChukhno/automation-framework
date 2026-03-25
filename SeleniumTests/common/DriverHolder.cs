using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SeleniumTests.common
{
    public class DriverHolder : BaseElements
    {
        private const string DefaultLoadingLocator = "//div[@class='loading']";

        public DriverHolder(IWebDriver driver) : base(driver)
        {
        }

        #region Navigation

        public void GoToUrl(string url)
        {
            driver.Navigate().GoToUrl(url);
            WaitForSlide();
            WaitForLoading();
        }

        #endregion

        #region Element Finding

        public IWebElement? Element(string xpath, bool isWantVisible = true)
        {
            try
            {
                return isWantVisible ? WaitForElementVisible(xpath, 20) : WaitForElementExist(xpath, 20);
            }
            catch
            {
                return null;
            }
        }
             
        public List<IWebElement> WElements(string xpath)
        {
            return driver.FindElements(By.XPath(xpath)).ToList();
        }

        public List<IWebElement> ElementsInElement(IWebElement parentElement, string childElementLocator)
        {
            return parentElement == null 
                ? WElements(childElementLocator) 
                : parentElement.FindElements(By.XPath("." + childElementLocator)).ToList();
        }

        #endregion

        #region Element Checks

        public bool IsElementVisible(string xpath)
        {
            try
            {
                return driver.FindElement(By.XPath(xpath)).Displayed;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Wait Methods

        public IWebElement WaitForElementVisible(string locator, int seconds = 20)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            return wait.Until(d =>
            {
                try
                {
                    var element = d.FindElement(By.XPath(locator));
                    return element.Displayed ? element : null;
                }
                catch
                {
                    return null;
                }
            });
        }

        public IWebElement WaitForElementExist(string locator, int seconds = 20)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            return wait.Until(d => d.FindElement(By.XPath(locator)));
        }

        public void WaitForElementToDisappear(string locator, int seconds = 20)
        {
            for (int i = 0; i < seconds; i++)
            {
                if (!IsElementVisible(locator))
                    return;
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            throw new Exception($"Element did not disappear during {seconds} seconds");
        }

        public void WaitForLoading(int milliseconds)
        {
            try
            {
                Thread.Sleep(milliseconds);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void WaitForLoading()
        {
            if (IsElementVisible(DefaultLoadingLocator))
                WaitForElementToDisappear(DefaultLoadingLocator);
            else
                WaitForSlide();
        }

        public void WaitForSlide()
        {
            WaitForLoading(500);
        }

        #endregion

        #region Text & Value Methods

        public string GetElementText(string locator)
        {
            var el = Element(locator);
            return el == null ? string.Empty : GetElementText(el);
        }

        public string GetElementText(IWebElement el)
        {
            string tag = el.TagName.ToLower();
            
            return tag switch
            {
                "input" => el.GetAttribute("value") ?? string.Empty,
                "textarea" => el.GetAttribute("value") ?? string.Empty,
                _ => el.Text
            };
        }

        #endregion

        #region Fill Methods

        public void FillInput(string input, string value)
        {
            if (value != null && !GetElementText(input).Equals(value))
            {
                var element = Element(input);
                element?.Clear();
                element?.SendKeys(value);
            }
        }

        #endregion
      
    }
}
