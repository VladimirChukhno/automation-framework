using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace SeleniumTests.common
{
    public class BaseTest
    {
        protected IWebDriver driver;

        [SetUp]
        public virtual void Setup()
        {
            // Initialize Chrome driver with options
            var options = new ChromeOptions();
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            
            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public virtual void TearDown()
        {
            // Close the browser
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
