using OpenQA.Selenium;
using SeleniumTests.common;
using SeleniumTests.interfaces.elements;

namespace SeleniumTests.interfaces.pages
{
    public class CartPage : DriverHolder
    {
        private CartPageElements cartElements;

        public CartPage(IWebDriver driver) : base(driver)
        {
            cartElements = new CartPageElements(driver);
        }

        public (string name, string price) GetProductInfoInCart(string productName)
        {
            // Get all product names and prices in cart using simple locators
            var productNameElements = WElements(cartElements.productName);
            var productPriceElements = WElements(cartElements.productPrice);

            if (productNameElements.Count == 0)
            {
                throw new Exception("No products found in cart");
            }

            // Find the product by exact name
            for (int i = 0; i < productNameElements.Count; i++)
            {
                var currentProductName = productNameElements[i].Text;
                
                if (currentProductName.Equals(productName, StringComparison.OrdinalIgnoreCase))
                {
                    // Return the matching product name and corresponding price
                    var name = currentProductName;
                    var price = productPriceElements[i].Text;
                    return (name, price);
                }
            }

            throw new Exception($"Product '{productName}' not found in cart");
        }
    }
}
