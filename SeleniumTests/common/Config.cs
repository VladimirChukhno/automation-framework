using System.Collections.Generic;

namespace SeleniumTests.common
{
    public class BaseUser
    {
        public string? Login { get; set; }
        public string? Password { get; set; }
    }

    public class Config
    {
        public static readonly Dictionary<string, string> SiteUrls = new Dictionary<string, string>()
        {
            { "local", "https://www.saucedemo.com/" }
        };

        // Default credentials
        private static readonly string DefaultPassword = "secret_sauce";

        public static readonly BaseUser TestUser = new BaseUser()
        {
            Login = "standard_user",
            Password = DefaultPassword
        };

        // Helper method to get URL by environment key
        public static string GetSiteUrl(string environment = "local")
        {
            return SiteUrls.ContainsKey(environment) ? SiteUrls[environment] : SiteUrls["local"];
        }

        // Helper method to get test user credentials
        public static BaseUser GetTestUserCredentials()
        {
            return TestUser;
        }
    }
}
