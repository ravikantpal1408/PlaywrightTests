using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightTests.Pages
{
    public class DashboardPage
    {
        private readonly IPage _page;

        public DashboardPage(IPage page)
        {
            _page = page;
        }

        public ILocator DashboardHeader => _page.Locator("//h6[contains(normalize-space(), 'Dashboard')]");
        public ILocator UserDropdown => _page.Locator("img[alt='profile picture']");
        public ILocator QuickLaunchPanel => _page.Locator("p:has-text('Quick Launch')");

        public async Task<bool> IsDashboardVisible()
        {
            // var header = DashboardHeader.GetByRole(AriaRole.Heading, new() { Name = "Dashboard" });

            try
            {
                await DashboardHeader.WaitForAsync(new() { Timeout = 8000, State = WaitForSelectorState.Visible });
                return await DashboardHeader.IsVisibleAsync();
            }
            catch
            {
                await _page.ScreenshotAsync(new() { Path = "Dashboard_Failure.png", FullPage = true });
                return false;
            }
            //await DashboardHeader.GetByRole(AriaRole.Heading, new() { Name = "Dashboard" })
            // return await header.IsVisibleAsync();
        }


        public async Task<bool> IsUserDropdownVisible()
        {
            await UserDropdown.WaitForAsync(new() { Timeout = 5000 });
            return await UserDropdown.IsVisibleAsync();
        }

        public async Task<bool> IsQuickLaunchVisible()
        {
            await QuickLaunchPanel.WaitForAsync(new() { Timeout = 5000 });
            return await QuickLaunchPanel.IsVisibleAsync();
        }
    }
}
