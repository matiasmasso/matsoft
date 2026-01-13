using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;
using DTO;
using System.IdentityModel.Tokens.Jwt;
using DTO.Helpers;
using Microsoft.VisualStudio.Threading;
using Components;
using TestAuth;

namespace TestAuth
{


    public class CustomAuthenticationStateProvider : AuthenticationStateProvider, IAuthenticationStateProvider
    {
        private HttpClient http;
        private readonly ProtectedSessionStorage _sessionStorage;
        private readonly ICookie _cookie;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        public string? Token { get; set; }
        public AsyncEventHandler? TokenChangedAsync;


        public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage, HttpClient http, ICookie cookie)
        {
            _sessionStorage = sessionStorage;
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
                Token = await TryGetTokenFromBrowserStorageAsync();
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
                await _cookie.SetValue("hash","");
                await _sessionStorage.DeleteAsync("accounttoken");
            }
            else
                await _sessionStorage.SetAsync("accounttoken", token);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
            await TokenChangedAsync.InvokeAsync(this, new MatEventArgs<string>(token));
        }


        public async Task LogoutAsync()
        {
            await UpdateAuthenticationStateAsync();
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




        private async Task<string?> TryGetTokenFromBrowserStorageAsync()
        {
            var storageResult = await _sessionStorage.GetAsync<string>("accounttoken");
            var retval = storageResult.Success ? storageResult.Value : null;
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


