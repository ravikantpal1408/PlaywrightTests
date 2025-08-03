using Microsoft.Playwright;
using PlaywrightTests.Pages;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlaywrightTests.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly IPage _page;
        private readonly LoginPage _loginPage;

        public LoginSteps(ScenarioContext scenarioContext)
        {
            _page = (IPage)scenarioContext["page"];
            _loginPage = new LoginPage(_page);
        }

        [Given(@"I navigate to OrangeHRM login page")]
        public async Task GivenINavigateToOrangeHRMLoginPage()
        {
            await _loginPage.NavigateAsync();
        }

        [When(@"I login with valid OrangeHRM credentials")]
        public async Task WhenILoginWithValidOrangeHRMCredentials()
        {
            await _loginPage.EnterCredentials("Admin", "admin123");
        }

        [Then(@"I should be redirected to the OrangeHRM dashboard")]
        public async Task ThenIShouldBeRedirectedToTheOrangeHRMDashboard()
        {
            var visible = await _loginPage.IsDashboardVisible();
            Assert.IsTrue(visible, "Dashboard is not visible. Login may have failed.");
        }
    }
}
