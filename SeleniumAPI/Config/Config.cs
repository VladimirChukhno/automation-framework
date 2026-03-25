using Microsoft.Extensions.Configuration;
using System.IO;

namespace SeleniumAPI.Config
{
    public static class Config
    {
        private static IConfigurationRoot configuration;

        static Config()
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Config/appsettings.json")
                .Build();
        }

        public static string BaseUrl => configuration["BaseUrl"];
    }
}