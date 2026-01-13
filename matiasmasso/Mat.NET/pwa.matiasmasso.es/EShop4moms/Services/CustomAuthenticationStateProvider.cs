using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using DTO;
using System.IdentityModel.Tokens.Jwt;
using Components;
using DTO.Helpers;
using Microsoft.VisualStudio.Threading;

//specific for Blazor Server:
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Shop4moms.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private HttpClient http;
        private readonly ICookie _cookie;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        public AsyncEventHandler? LoginChanged;
        public UserModel? CurrentUser { get; private set; }

        private readonly ProtectedSessionStorage _sessionStorage;

        //constructor specific for Blazor Server side due to sessionStorage
        public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage, HttpClient http, ICookie cookie)
        {
            _sessionStorage = sessionStorage;
            this.http = http;
            _cookie = cookie;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                //only succeeds since JS is available, other else silently fails on first turn
                var claimsPrincipal = await ClaimsPrincipalAsync();
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                //silently fails on prerrendering since JS is not available yet
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        public async Task LoginAsync(UserModel? user = null)
        {
            await PersistAsync(user);
            ClaimsPrincipal claimsPrincipal = user?.ClaimsPrincipal() ?? _anonymous;
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
            await LoginChanged.InvokeAsync(null, new System.EventArgs());
        }

        public async Task LogoutAsync()
        {
            await LoginAsync();
        }


        #region Utilities

        private async Task<ClaimsPrincipal> ClaimsPrincipalAsync()
        {
            CurrentUser = await SessionUserAsync() ?? await PersistedUserAsync();
            var retval = CurrentUser == null ? _anonymous : CurrentUser.ClaimsPrincipal();
            return retval;
        }

        private async Task<UserModel?> SessionUserAsync()
        {
            var userSessionStorageResult = await _sessionStorage.GetAsync<UserModel>("CurrentUser");
            var retval = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
            return retval;
        }

        private async Task<UserModel?> PersistedUserAsync()
        {
            ApiResponse<UserModel>? apiResponse = null;
            var hash = await _cookie.GetValue("hash");
            if (hash != null) apiResponse = await HttpService.PostAsync<string, UserModel>(http, hash, "User/fromHash", ((int)EmpModel.EmpIds.MatiasMasso).ToString());
            return apiResponse?.Value;
        }

        private async Task PersistAsync(UserModel? user = null)
        {
            CurrentUser = user;
            await _cookie.SetValue("hash", user?.Hash ?? "");
            if (user == null)
                await _sessionStorage.DeleteAsync("CurrentUser");
            else
                await _sessionStorage.SetAsync("CurrentUser", user);
        }

        #endregion


    }
}


