using TechTalk.SpecFlow;
using PlaywrightTests.Drivers;
using System.Threading.Tasks;

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

            if (driver.Page != null)
            {
                _scenarioContext["page"] = driver.Page;
                _scenarioContext["driver"] = driver; // store driver for cleanup
            }
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            try
            {
                if (_scenarioContext.TryGetValue("driver", out var driverObj) &&
                    driverObj is PlaywrightDriver driver)
                {
                    await driver.CleanupAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AfterScenario Cleanup Error] {ex.Message}");
                // optionally log more info or rethrow
            }
        }
    }
}
