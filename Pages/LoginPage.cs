using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightTests.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;

        public LoginPage(IPage page)
        {
            _page = page;
        }

        public async Task NavigateAsync()
        {
            await _page.GotoAsync("https://opensource-demo.orangehrmlive.com/");
        }

        public async Task EnterCredentials(string username, string password)
        {
            await _page.FillAsync("input[name='username']", username);
            await _page.FillAsync("input[name='password']", password);
            await _page.ClickAsync("button[type='submit']");
        }

        public async Task<bool> IsDashboardVisible()
        {
            try
            {
                // Wait up to 10 seconds for the Dashboard heading to appear
                await _page.WaitForSelectorAsync("h6:has-text('Dashboard')", new() { Timeout = 10000 });
                return await _page.Locator("h6:has-text('Dashboard')").IsVisibleAsync();
            }
            catch
            {
                return false;
            }
        }



    }
}
