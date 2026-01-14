using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Wasm.Auth;

namespace Wasm.Auth;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ITokenStorage _tokens;
    private readonly JwtSecurityTokenHandler _handler = new();

    public CustomAuthStateProvider(ITokenStorage tokens)
    {
        _tokens = tokens;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var (access, _) = await _tokens.GetTokensAsync();

        if (string.IsNullOrEmpty(access))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var principal = BuildClaimsPrincipal(access);
        return new AuthenticationState(principal);
    }

    private ClaimsPrincipal BuildClaimsPrincipal(string jwt)
    {
        var token = _handler.ReadJwtToken(jwt);
        var identity = new ClaimsIdentity(token.Claims, "jwt");
        return new ClaimsPrincipal(identity);
    }

    public async Task MarkUserAsAuthenticated()
    {
        var (access, _) = await _tokens.GetTokensAsync();
        var principal = string.IsNullOrEmpty(access)
            ? new ClaimsPrincipal(new ClaimsIdentity())
            : BuildClaimsPrincipal(access);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }

    public Task MarkUserAsLoggedOut()
    {
        var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
        return Task.CompletedTask;
    }
}
