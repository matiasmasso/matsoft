using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly JwtSecurityTokenHandler _tokenHandler = new();

    private const string AccessTokenKey = "accessToken";

    public ApiAuthenticationStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    // ------------------------------------------------------------
    // MAIN AUTH STATE METHOD
    // ------------------------------------------------------------
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsync<string>(AccessTokenKey);

        if (string.IsNullOrWhiteSpace(token))
            return AnonymousState();

        var principal = CreateClaimsPrincipalFromToken(token);
        return new AuthenticationState(principal);
    }

    // ------------------------------------------------------------
    // NOTIFY LOGIN
    // ------------------------------------------------------------
    public void NotifyUserAuthentication(string token)
    {
        var principal = CreateClaimsPrincipalFromToken(token);
        var authState = Task.FromResult(new AuthenticationState(principal));

        NotifyAuthenticationStateChanged(authState);
    }

    // ------------------------------------------------------------
    // NOTIFY LOGOUT
    // ------------------------------------------------------------
    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(AnonymousState());
        NotifyAuthenticationStateChanged(authState);
    }

    // ------------------------------------------------------------
    // HELPERS
    // ------------------------------------------------------------
    private ClaimsPrincipal CreateClaimsPrincipalFromToken(string token)
    {
        try
        {
            var jwt = _tokenHandler.ReadJwtToken(token);

            var identity = new ClaimsIdentity(
                jwt.Claims,
                authenticationType: "jwt"
            );

            return new ClaimsPrincipal(identity);
        }
        catch
        {
            // If token is invalid or unreadable, treat as anonymous
            return new ClaimsPrincipal(new ClaimsIdentity());
        }
    }

    private AuthenticationState AnonymousState()
    {
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }
}