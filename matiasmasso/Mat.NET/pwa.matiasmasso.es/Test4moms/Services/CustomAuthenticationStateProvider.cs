using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;
using DTO;

namespace Test4moms.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private readonly HttpClient _http;
        private readonly ICookie _cookie;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage, HttpClient http, ICookie cookie)
        {
            _sessionStorage = sessionStorage;
            _http = http;
            _cookie = cookie;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //get claimsPrincipal from stored user if within a session
            //or get it from cookie if a new session
            //return AuthenticationState from claimsPrincipal
            //or return anonymous if user throws error on modifying sessionstore by hand
            try
            {
                var storedUser = await TryGetUserFromBrowserStorage();
                if (storedUser == null) storedUser = await TryGetUserFromPersistedCookie();
                var claimsPrincipal = IdentityFromUserAccount(storedUser);
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                //silently fails on prerrendering since JS is not available yet
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }



        public async Task UpdateAuthenticationState(UserModel? userAccount = null)
        {
            ClaimsPrincipal claimsPrincipal = IdentityFromUserAccount(userAccount);
            if (userAccount == null)
                await _sessionStorage.DeleteAsync("UserAccount");
            else
                await _sessionStorage.SetAsync("UserAccount", userAccount);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task Logout()
        {
            await UpdateAuthenticationState(null);
        }

        private ClaimsPrincipal IdentityFromUserAccount(UserModel? userAccount)
        {
            ClaimsPrincipal retval = _anonymous;
            if (userAccount != null)
            {
                try
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Hash, userAccount.Hash ?? ""),
                new Claim(ClaimTypes.Name, userAccount.EmailAddress ?? ""),
                new Claim(ClaimTypes.Role, ((UserModel.Rols)userAccount.Rol).ToString())
            };
                    retval = new ClaimsPrincipal(new ClaimsIdentity(claims, "CustomAuth"));
                }
                catch { }
            }
            return retval;
        }

        //during a session, user is serialized onProtectedBrowserStorage
        private async Task<UserModel?> TryGetUserFromBrowserStorage()
        {
            var userStorageResult = await _sessionStorage.GetAsync<UserModel>("UserAccount");
            var retval = userStorageResult.Success ? userStorageResult.Value : null;
            return retval;
        }

        //if user persisted is true, a username/password hash is stored on a cookie
        //which can be retrieved on a new session and skip login process if validated successfully
        private async Task<UserModel?> TryGetUserFromPersistedCookie()
        {
            UserModel? retval = null;
            var hash = await _cookie.GetValue("hash");
            if (hash != null)
            {
                var apiResponse = await Components.HttpService.PostAsync<string, UserModel>(_http, hash, "User/fromHash",((int)EmpModel.EmpIds.MatiasMasso).ToString());
                if (apiResponse.Success())
                {
                    retval = apiResponse.Value;
                    await UpdateAuthenticationState(retval);
                }
            }
            return retval;
        }

    }
}


