using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using DTO;

namespace Cash.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private AuthenticationState authenticationState;
        private readonly CookieService cookieService;

        public CustomAuthenticationStateProvider(AuthenticationService service, CookieService cookieService)
        {
            authenticationState = new AuthenticationState(service.ClaimsPrincipal);
            this.cookieService = cookieService; 

            service.UserChanged += (newUser) =>
            {
                authenticationState = new AuthenticationState(newUser);

                NotifyAuthenticationStateChanged(
                    Task.FromResult(new AuthenticationState(newUser)));
                //NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            };
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync() {
            return Task.FromResult(authenticationState);

        }


    }

}



