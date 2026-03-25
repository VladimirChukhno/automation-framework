using OpenQA.Selenium;
using SeleniumTests.common;

namespace SeleniumTests.common.Login
{
    public class LoginPages : DriverHolder
    {
        private LoginElements loginElements;

        public LoginPages(IWebDriver driver) : base(driver)
        {
            this.loginElements = new LoginElements(driver);
        }

        public void EnterUsername(string username)
        {
            FillInput(loginElements.usernameInput, username);
        }

        public void EnterPassword(string password)
        {
            FillInput(loginElements.passwordInput, password);
        }

        public void ClickLoginButton()
        {
            Element(loginElements.loginButton)?.Click();
        }

        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLoginButton();
        }
        
    }
}
