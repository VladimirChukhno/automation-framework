using NUnit.Framework;
using SeleniumTests.common;
using SeleniumTests.common.Login;
using SeleniumTests.interfaces.pages;

namespace SeleniumTests.interfaces.tests
{
    public class ProductOrderTest : BaseTest
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();
        }

        [Test]
        public void TestAddProductToCart()
        {
            try
            {
                // Get credentials from config first
                var testUser = Config.GetTestUserCredentials();
                
                // Navigate to login page
                var siteUrl = Config.GetSiteUrl("local");
                LoginPages loginPage = new LoginPages(driver);
                loginPage.GoToUrl(siteUrl); 

                // Login to site
                loginPage.Login(testUser.Login ?? "", testUser.Password ?? "");
                loginPage.WaitForLoading(200);

                // Get product info from products page
                ProductsPage productsPage = new ProductsPage(driver);
                productsPage.SelectFilterOption("Price (High to low)");
                var (productName, productPrice) = productsPage.GetProductInfoByName("Sauce Labs Bike Light");

                // Select product and navigate to detail page
                productsPage.SelectProduct("Sauce Labs Bike Light");
                DetailProductPage detailProductPage = new DetailProductPage(driver);
                loginPage.WaitForLoading(200);

                // Create detail page object and get product info
                var (detailName, detailPrice) = detailProductPage.GetProductInfo();

                // Verify product info matches between pages
                Assert.That(productName.Trim(), Is.EqualTo(detailName.Trim()), "Product name should match between pages");
                Assert.That(productPrice.Trim(), Is.EqualTo(detailPrice.Trim()), "Product price should match between pages");

                // Add product to cart and verify
                detailProductPage.AddToCart();
                
                // Check count in cart badge
                var cartCount = detailProductPage.GetCartItemCount();
                Assert.That(cartCount, Is.EqualTo(1), "Cart item count should be 1 after adding product");

                // Go to cart and verify product info
                detailProductPage.GoToCart();
                CartPage cartPage = new CartPage(driver);
                loginPage.WaitForLoading(200);

                var (cartName, cartPrice) = cartPage.GetProductInfoInCart("Sauce Labs Bike Light");
                Assert.That(cartName.Trim(), Is.EqualTo(detailName.Trim()), "Product name in cart should match detail page");
                Assert.That(cartPrice.Trim(), Is.EqualTo(detailPrice.Trim()), "Product price in cart should match detail page");

            }
            catch (Exception ex)
            {
                Assert.Fail($"Test failed with error: {ex.Message}");
            }
        }
    }
}
