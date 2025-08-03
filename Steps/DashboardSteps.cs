using TechTalk.SpecFlow;
using PlaywrightTests.Pages;
using Microsoft.Playwright;

namespace PlaywrightTests.Steps
{
    [Binding]
    public class DashboardSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly DashboardPage _dashboardPage;

        public DashboardSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            var page = _scenarioContext["page"] as IPage;
            _dashboardPage = new DashboardPage(page);
        }

        [Then(@"I should see key dashboard widgets")]
        public async Task ThenIShouldSeeKeyDashboardWidgets()
        {
            Console.WriteLine(await _dashboardPage.IsDashboardVisible());
            Assert.IsTrue(await _dashboardPage.IsDashboardVisible(), "Dashboard header not visible.");
        }

        // [Then(@"I should see the user profile dropdown")]
        // public async Task ThenIShouldSeeTheUserProfileDropdown()
        // {
        //     Assert.IsTrue(await _dashboardPage.IsUserDropdownVisible(), "User dropdown not visible.");
        // }

        // [Then(@"I should see the quick launch panel")]
        // public async Task ThenIShouldSeeTheQuickLaunchPanel()
        // {
        //     Assert.IsTrue(await _dashboardPage.IsQuickLaunchVisible(), "Quick launch panel not visible.");
        // }


    }
}
