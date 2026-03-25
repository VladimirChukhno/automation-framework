using OpenQA.Selenium;
using SeleniumTests.common;

namespace SeleniumTests.interfaces.elements
{
    public class DetailProductPageElements : BaseElements
    {
        // Локатори як публічні поля
        public string
            productName = "//*[@data-test=\"inventory-item-name\"]",
            productPrice = "//*[@data-test=\"inventory-item-price\"]",
            addToCartButton = "//*[@data-test=\"add-to-cart\"]",
            shoppingCartBtn = "//*[@data-test=\"shopping-cart-link\"]",
            shoppingCartBadge = "//*[@data-test=\"shopping-cart-badge\"]";

        public DetailProductPageElements(IWebDriver driver) : base(driver)
        {
        }
    }
}
