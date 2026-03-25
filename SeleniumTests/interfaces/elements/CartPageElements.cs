using OpenQA.Selenium;
using SeleniumTests.common;

namespace SeleniumTests.interfaces.elements
{
    public class CartPageElements : BaseElements
    {
        // Локатори як публічні поля
        public string
            productName = "//*[@data-test=\"inventory-item-name\"]",
            productPrice = "//*[@data-test=\"inventory-item-price\"]";

        public CartPageElements(IWebDriver driver) : base(driver)
        {
        }
    }
}
