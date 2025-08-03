using Microsoft.Extensions.Configuration;

namespace PlaywrightTests.Helpers
{
    public static class ConfigManager
    {
        private static IConfigurationRoot config;

        static ConfigManager()
        {
            config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static string BaseUrl => config["Playwright:BaseUrl"] ?? string.Empty;
        public static string Browser => config["Playwright:Browser"];
        public static bool Headless => bool.Parse(config["Playwright:Headless"]);
        public static int Timeout => int.Parse(config["Playwright:Timeout"]);
        public static string Username => config["Credentials:Username"];
        public static string Password => config["Credentials:Password"];
    }
}
