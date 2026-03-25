using OpenQA.Selenium;
using SeleniumTests.common;

namespace SeleniumTests.common.Login
{
    public class LoginElements : BaseElements
    {
        // Локатори як публічні поля
        public string
            usernameInput = "//*[@data-test=\"username\"]",
            passwordInput = "//*[@data-test=\"password\"]",
            loginButton = "//*[@data-test=\"login-button\"]";

        public LoginElements(IWebDriver driver) : base(driver)
        {
        }
    }
}
