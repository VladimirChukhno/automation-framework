using OpenQA.Selenium;
using SeleniumTests.common;
using SeleniumTests.interfaces.elements;

namespace SeleniumTests.interfaces.pages
{
    public class DetailProductPage : DriverHolder
    {
        private DetailProductPageElements elements;

        public DetailProductPage(IWebDriver driver) : base(driver)
        {
            elements = new DetailProductPageElements(driver);
        }

        public void AddToCart()
        {
            var addButton = Element(elements.addToCartButton);
            if (addButton != null)
            {
                addButton.Click();
            }
        }
        
        public string GetProductName()
        {
            return GetElementText(elements.productName) ?? "";
        }
        
        public string GetProductPrice()
        {
            return GetElementText(elements.productPrice) ?? "";
        }
        
        public (string name, string price) GetProductInfo()
        {
            var name = GetProductName();
            var price = GetProductPrice();
            return (name, price);
        }
        
        public void GoToCart()
        {
            var cartBtn = Element(elements.shoppingCartBtn);
            if (cartBtn != null)
            {
                cartBtn.Click();
            }
        }

        public int GetCartItemCount()
        {
            var cartBadge = Element(elements.shoppingCartBadge);
            if (cartBadge != null)
            {
                var countText = cartBadge.Text;
                if (int.TryParse(countText, out int count))
                {
                    return count;
                }
            }
            return 0;
        }
    }
}
