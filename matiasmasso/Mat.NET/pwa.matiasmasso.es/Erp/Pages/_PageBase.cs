using Microsoft.AspNetCore.Components;
using Erp.Shared;

namespace Erp.Pages
{
    public class _PageBase : _ComponentBase
    {
        [Inject] public NavigationManager? NavigationManager { get; set; }
        [CascadingParameter] public SessionProvider? SessionProvider { get; set; }

        protected override void OnInitialized()
        {
            var url = NavigationManager!.Uri.ToString().Replace("https://0.0.0.0", "");
            SessionProvider!.AddToBrowserHistory(url);
            base.OnInitialized();
        }

        protected void BackToLastPage()
        {
            SessionProvider!.RemoveLastBrowserHistory();
            var url = SessionProvider.GetLastBrowserHistory();
            NavigationManager?.NavigateTo(url);

        }
    }
}
