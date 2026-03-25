using OpenQA.Selenium;
using SeleniumTests.common;

namespace SeleniumTests.interfaces.elements
{
    public class ProductsPageElements : BaseElements
    {
        // Локатори як публічні поля
        public string
            filterButton = "//*[@data-test=\"product-sort-container\"]",
            filterValue = "//*[@data-test=\"product-sort-container\"]/option",
            productName = "//*[@data-test=\"inventory-item-name\"]",
            productPrice = "//*[@data-test=\"inventory-item-price\"]";

        public ProductsPageElements(IWebDriver driver) : base(driver)
        {
        }
    }
}
