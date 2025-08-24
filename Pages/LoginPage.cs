using Microsoft.Playwright;
using PlaywrightTests.Helpers;
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
            await _page.GotoAsync(ConfigManager.BaseUrl, new PageGotoOptions
            {
                Timeout = 60000, // 60 seconds
                WaitUntil = WaitUntilState.Load
            });
        }

        public async Task EnterCredentials(string username, string password)
        {
            await _page.FillAsync("input[name='username']", username);
            await _page.FillAsync("input[name='password']", password);
            await _page.PauseAsync();

            await _page.ClickAsync("button[type='submit']", new PageClickOptions
            {
                Timeout = 60000
            });

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

        public async Task<bool> IsErrorVisible()
        {
            try
            {
                await _page.GetByText("Invalid credentials").WaitForAsync(new()
                {
                    Timeout = 10000,
                    State = WaitForSelectorState.Visible
                });


                return await _page.Locator("text=Invalid credentials").IsVisibleAsync();
                // Wait up to 10 seconds for the Dashboard heading to appear

            }
            catch
            {
                return false;
            }
        }



    }
}
