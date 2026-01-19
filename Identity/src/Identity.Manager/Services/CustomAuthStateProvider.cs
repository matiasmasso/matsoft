using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Identity.Manager.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly SessionStorageService _storage;
    private readonly JwtSecurityTokenHandler _handler = new();

    private const string AccessTokenKey = "access_token";

    public CustomAuthStateProvider(SessionStorageService storage)
    {
        _storage = storage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _storage.GetAsync(AccessTokenKey);

        if (string.IsNullOrWhiteSpace(token))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var principal = CreatePrincipal(token);
        return new AuthenticationState(principal);
    }

    public async Task NotifyUserAuthenticationAsync(string token)
    {
        await _storage.SetAsync(AccessTokenKey, token);
        var principal = CreatePrincipal(token);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }

    public async Task NotifyUserLogoutAsync()
    {
        await _storage.RemoveAsync(AccessTokenKey);
        var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
    }

    private ClaimsPrincipal CreatePrincipal(string jwt)
    {
        var token = _handler.ReadJwtToken(jwt);

        var identity = new ClaimsIdentity(
            token.Claims,
            authenticationType: "jwt",
            nameType: ClaimTypes.Name,
            roleType: ClaimTypes.Role
        );

        return new ClaimsPrincipal(identity);
    }
}