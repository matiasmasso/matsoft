using System.Security.Claims;
using DTO;
using System.IdentityModel.Tokens.Jwt;
using DTO.Helpers;
using Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.VisualStudio.Threading;
using System.Net;

namespace Winforms.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private HttpClient http;
        private readonly ICookie cookie;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        public AsyncEventHandler? TokenChangedAsync;
        private string? token;


        public CustomAuthenticationStateProvider(HttpClient http, ICookie cookie)
        {
            this.http = http;
            this.cookie = cookie;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //get claimsPrincipal from stored user if within a session
            //or get it from cookie if a new session
            //return AuthenticationState from claimsPrincipal
            //or return anonymous if user throws error on modifying sessionstore by hand
            try
            {
                if (token == null) token = await TryGetTokenFromPersistedCookieAsync();
                var claimsPrincipal = IdentityFromToken(token);
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
            ClaimsPrincipal claimsPrincipal = IdentityFromToken(token);
            await cookie.SetValue("token", token ?? "");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
            await TokenChangedAsync.InvokeAsync(this, new MatEventArgs<string>(token));
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


        //if user persisted is true, a username/password hash is stored on a cookie
        //which can be retrieved on a new session and skip login process if validated successfully
        private async Task<string?> TryGetTokenFromPersistedCookieAsync()
        {
            string? retval = null;
            var previousToken = await cookie.GetValue("token");
            if (previousToken != null)
            {
                var previousUser = UserModel.FromToken(previousToken);
                if (previousUser != null)
                {
                    //refresh user in case rol has changed opr user changed his password
                    var apiResponse = await HttpService.PostAsync<string, string>(http, previousUser.Hash, "Token", ((int)EmpModel.EmpIds.MatiasMasso).ToString());
                    if (apiResponse.Success())
                    {
                        retval = apiResponse.Value;
                        await UpdateAuthenticationStateAsync(retval);
                    }
                }
            }
            return retval;
        }

    }
}


