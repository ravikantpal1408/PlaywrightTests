using Microsoft.Playwright;
using PlaywrightTests.Pages;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlaywrightTests.Helpers;

namespace PlaywrightTests.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly IPage _page;
        private readonly LoginPage _loginPage;
        private string _username;


        public LoginSteps(ScenarioContext scenarioContext)
        {
            _page = (IPage)scenarioContext["page"];
            _loginPage = new LoginPage(_page);
        }

        [Given(@"I navigate to OrangeHRM login page")]
        public async Task GivenINavigateToOrangeHRMLoginPage()
        {
            _username = await McpManager.CallToolAsync("generateTestData", new { });
            await _loginPage.NavigateAsync();
        }

        [When(@"I login with valid OrangeHRM credentials")]
        public async Task WhenILoginWithValidOrangeHRMCredentials()
        {
            await _loginPage.EnterCredentials("Admin", "admin123");
        }

        [When(@"I login with invalid OrangeHRM credentials")]
        public async Task WhenILoginWithInValidOrangeHRMCredentials()
        {
            await _loginPage.EnterCredentials("Admin", "test123");
        }

        [Then(@"I should be error message")]
        public async Task TheISeeErrorMessage()
        {
            var visible = await _loginPage.IsErrorVisible();

        }

        [Then(@"I should be redirected to the OrangeHRM dashboard")]
        public async Task ThenIShouldBeRedirectedToTheOrangeHRMDashboard()
        {
            var visible = await _loginPage.IsDashboardVisible();
            Assert.IsTrue(visible, "Dashboard is not visible. Login may have failed.");
        }
    }
}
