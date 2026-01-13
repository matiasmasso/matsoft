using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using DTO;
using System.IdentityModel.Tokens.Jwt;
using Components;
using DTO.Helpers;
using Microsoft.VisualStudio.Threading;

//specific for Blazor Server:
//using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Erp.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private HttpClient http;
        private readonly ICookie _cookie;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        public string? Token { get; set; }
        public AsyncEventHandler? TokenChangedAsync;

        //specific for Blazor Server:
        //private readonly ProtectedSessionStorage _sessionStorage;

        public CustomAuthenticationStateProvider(HttpClient http, ICookie cookie) //, ProtectedSessionStorage sessionStorage)
        {
            //specific for Blazor Server:
            //_sessionStorage = sessionStorage;
            this.http = http;
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
                Token = await TryGetTokenFromBrowserStorageAsync(); // TODO: check if outdated since it will always persist
                if (Token == null) Token = await TryGetTokenFromPersistedCookieAsync();
                var claimsPrincipal = IdentityFromToken(Token);
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                //silently fails on prerrendering since JS is not available yet
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        public async Task UpdateAuthenticationStateAsync(string? token = null)
        {
            Token = token;

            ClaimsPrincipal claimsPrincipal = IdentityFromToken(token);
            if (string.IsNullOrEmpty(token))
            {
                await _cookie.SetValue("hash", "");

                //specific for Blazor Server:
                //await _sessionStorage.DeleteAsync("accounttoken");

                //specific for Maui
                SecureStorage.Remove("accounttoken");
            }
            else
            {
                //specific for Blazor Server:
                //await _sessionStorage.SetAsync("accounttoken", token);

                //specific for Maui:
                await SecureStorage.SetAsync("accounttoken", token);

            }

            await TokenChangedAsync.InvokeAsync(this, new MatEventArgs<string>(token));
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }


        private List<Claim> TokenClaims(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            return jwtSecurityToken.Claims.ToList();
        }

        private ClaimsPrincipal IdentityFromToken(string? token)
        {
            ClaimsPrincipal retval = _anonymous;
            if (!string.IsNullOrEmpty(token))
            {
                var claims = TokenClaims(token);
                retval = new ClaimsPrincipal(new ClaimsIdentity(claims, "CustomAuth"));
            }
            return retval;
        }



        public async Task LogoutAsync()
        {
            await UpdateAuthenticationStateAsync();
        }

        public async Task<string?> TryGetTokenFromBrowserStorageAsync()
        {
            //specific for Blazor Server:
            //var storageResult = await _sessionStorage.GetAsync<string>("accounttoken");
            //var retval = storageResult.Success ? storageResult.Value : null;

            //specific for Blazor Server:
            var retval = await SecureStorage.GetAsync("accounttoken");
            return retval;
        }

        //if user persisted is true, a username/password hash is stored on a cookie
        //which can be retrieved on a new session and skip login process if validated successfully
        private async Task<string?> TryGetTokenFromPersistedCookieAsync()
        {
            string? retval = null;
            var hash = await _cookie.GetValue("hash");
            if (hash != null)
            {
                var apiResponse = await Components.HttpService.PostAsync<string, string>(http, hash, "Token", ((int)EmpModel.EmpIds.MatiasMasso).ToString());
                if (apiResponse.Success())
                {
                    retval = apiResponse.Value;
                    await UpdateAuthenticationStateAsync(retval);
                }
            }
            return retval;
        }

    }
}


