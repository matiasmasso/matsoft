using Components;
using DTO;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.VisualStudio.Threading;
using static System.Net.WebRequestMethods;

namespace Web.Services
{
    public class NavService : IDisposable
    {

        public NavDTO? Nav { get; private set; }

        public List<NavDTO.Model> SelectedMenuItems = new();

        public bool IsLoaded { get; private set; }
        public AsyncEventHandler<DTO.Helpers.MatEventArgs<ProblemDetails>>? Loaded { get; set; }

        private HttpClient http;
        private CustomAuthenticationStateProvider authStateProvider;
        private UserModel? user;

        public NavService(HttpClient http, AuthenticationStateProvider authStateProvider)
        {
            this.http = http;
            this.authStateProvider = (CustomAuthenticationStateProvider)authStateProvider;
            authStateProvider.AuthenticationStateChanged += AuthenticationStateChanged;
            Task task = Task.Run(async () => await LoadNavAsync());
        }

        public List<NavDTO.Model> DisplayItems()
        {
            List<NavDTO.Model> retval;
            if (SelectedMenuItems.Any())
            {
                var parent = SelectedMenuItems.Last();
                retval = Nav?.Items.Where(x => x.Parent == parent.Guid).OrderBy(x => x.Ord).ToList() ?? new();
            }
            else
                retval = Nav?.Items.Where(x => x.Parent == null).OrderBy(x => x.Ord).ToList() ?? new();
            return retval;
        }

        public async Task LoadNavAsync(UserModel? usr = null)
        {
            IsLoaded = false;
            ApiResponse<NavDTO> apiResponse;

            if (usr != user)
            {
                user = usr;
                if (user == null)
                    apiResponse = await HttpService.GetAsync<NavDTO>(http, "navs/custom", ((int)EmpModel.EmpIds.MatiasMasso).ToString());
                else
                    apiResponse = await HttpService.GetAsync<NavDTO>(http, "navs/custom", ((int)EmpModel.EmpIds.MatiasMasso).ToString(), this.user.Guid.ToString());

                if (apiResponse.Success())
                {
                    Nav = apiResponse.Value;

                    IsLoaded = true;
                    Loaded?.DynamicInvoke(this, new DTO.Helpers.MatEventArgs<ProblemDetails>(apiResponse.ProblemDetails));
                }
                else
                    throw apiResponse.ProblemDetails?.ToException() ?? new Exception("Error");
            }

        }

        async void AuthenticationStateChanged(Task<AuthenticationState> task)
        {
            await LoadNavAsync();
        }

        public void Dispose()
        {
            authStateProvider.AuthenticationStateChanged -= AuthenticationStateChanged;
        }
    }
}
