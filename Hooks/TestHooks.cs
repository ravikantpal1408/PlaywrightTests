using TechTalk.SpecFlow;
using PlaywrightTests.Drivers;
using System.Threading.Tasks;
using PlaywrightTests.Helpers;

namespace PlaywrightTests.Hooks
{
    [Binding]
    public class TestHooks
    {
        private readonly ScenarioContext _scenarioContext;

        public TestHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            var driver = new PlaywrightDriver();
            await driver.InitializeAsync();

            _scenarioContext["page"] = driver.Page;
            _scenarioContext["driver"] = driver;

        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            if (_scenarioContext.TryGetValue("page", out var pageObj) &&
                pageObj is Microsoft.Playwright.IPage page)
            {
                if (_scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError)
                {
                    var screenshotsDir = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");
                    Directory.CreateDirectory(screenshotsDir);

                    var fileName = $"{_scenarioContext.ScenarioInfo.Title}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                    var filePath = Path.Combine(screenshotsDir, fileName);

                    await page.ScreenshotAsync(new Microsoft.Playwright.PageScreenshotOptions
                    {
                        Path = filePath,
                        FullPage = true
                    });

                    Console.WriteLine($"🖼 Screenshot saved: {filePath}");
                }
            }

            if (_scenarioContext.TryGetValue("driver", out var driverObj) &&
                driverObj is PlaywrightDriver driver)
            {
                await driver.CleanupAsync();
            }

            if (_scenarioContext.TestError != null)
            {
                Console.WriteLine($"❌ Scenario failed: {_scenarioContext.ScenarioInfo.Title}");
                Console.WriteLine($"   Error: {_scenarioContext.TestError.Message}");
            }

        }
    }
}
