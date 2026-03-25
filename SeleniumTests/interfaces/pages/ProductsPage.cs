using OpenQA.Selenium;
using SeleniumTests.common;
using SeleniumTests.interfaces.elements;

namespace SeleniumTests.interfaces.pages
{
    public class ProductsPage : DriverHolder
    {
        private ProductsPageElements productsElements;

        public ProductsPage(IWebDriver driver) : base(driver)
        {
            productsElements = new ProductsPageElements(driver);
        }
        
        public void SelectProduct(string productName)
        {
            // Get all product names and find the matching one
            var productNameElements = WElements(productsElements.productName);
            
            for (int i = 0; i < productNameElements.Count; i++)
            {
                if (productNameElements[i].Text.Equals(productName, StringComparison.OrdinalIgnoreCase))
                {
                    productNameElements[i].Click();
                    return;
                }
            }
            
            throw new Exception($"Product '{productName}' not found");
        }

        public void OpenFilter()
        {
            Element(productsElements.filterButton)?.Click();
        }
        public void SelectFilterOption(string option)
        {
            OpenFilter();

            var options = WElements(productsElements.filterValue);
            foreach (var opt in options)
            {
                if (opt.Text.Equals(option, StringComparison.OrdinalIgnoreCase))
                {
                    opt.Click();
                    break;
                }
            }
        }

        public (string name, string price) GetProductInfoByName(string productName)
        {
            // Get all product names and find the matching one
            var productNameElements = WElements(productsElements.productName);
            var productPriceElements = WElements(productsElements.productPrice);

            if (productNameElements.Count == 0)
            {
                throw new Exception("No products found on the page");
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

            throw new Exception($"Product '{productName}' not found");
        }
    }
}
