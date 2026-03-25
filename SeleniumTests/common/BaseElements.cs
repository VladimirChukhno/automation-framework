using OpenQA.Selenium;

namespace SeleniumTests.common
{
    public class BaseElements
    {
        protected IWebDriver driver;

        public BaseElements(IWebDriver driver)
        {
            this.driver = driver;
        }
    }
}
