using Microsoft.Playwright;
using PlaywrightTests.Helpers;
using System.Threading.Tasks;

namespace PlaywrightTests.Drivers
{
    public class PlaywrightDriver
    {
        public IPlaywright Playwright { get; private set; }
        public IBrowser Browser { get; private set; }
        public IPage Page { get; private set; }

        public async Task InitializeAsync()
        {
            Environment.SetEnvironmentVariable("PLAYWRIGHT_BROWSERS_PATH", "./WebDriver");

            var args = ConfigManager.UseKioskMode ? new[] { "--kiosk" } : new[] { "--start-maximized" };

            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            if (ConfigManager.Browser.Equals("Firefox", StringComparison.OrdinalIgnoreCase))
            {
                Browser = await Playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = ConfigManager.Headless,
                    Args = args
                });
            }
            else if (ConfigManager.Browser.Equals("Edge", StringComparison.OrdinalIgnoreCase))
            {
                Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    ExecutablePath = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe",
                    Headless = ConfigManager.Headless,
                    Args = new[] { "--start-maximized" }
                });
            }
            else
            {
                Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = ConfigManager.Headless,
                    Args = args
                });
            }


            var context = await Browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize { Width = 1920, Height = 1080 },
                IgnoreHTTPSErrors = true
            });

            Page = await context.NewPageAsync();
        }

        public async Task CleanupAsync()
        {
            try
            {
                if (Browser != null)
                {
                    await Browser.CloseAsync();
                }

                Playwright?.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PlaywrightDriver Cleanup Error] {ex.Message}");
            }
        }
    }
}
