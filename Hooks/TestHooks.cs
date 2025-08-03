using TechTalk.SpecFlow;
using PlaywrightTests.Drivers;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightTests.Hooks
{
    [Binding]
    public class TestHooks
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly PlaywrightDriver _driver;

        public TestHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _driver = new PlaywrightDriver(); // moved to constructor for stability
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            await _driver.InitializeAsync();
            _scenarioContext["page"] = _driver.Page; // ✅ set the page object
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            await _driver.CleanupAsync();
        }
    }
}
