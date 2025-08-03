using Microsoft.Playwright;
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
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                Args = new[] { "--kiosk" }
            });

            var context = await Browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = null, // Use full screen
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
