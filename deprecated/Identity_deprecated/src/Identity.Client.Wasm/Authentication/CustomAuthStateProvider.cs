using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Identity.Client.Wasm.Authentication;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly BrowserStorage _storage;
    private const string TokenKey = "access_token";

    private ClaimsPrincipal Anonymous => new(new ClaimsIdentity());

    public CustomAuthStateProvider(BrowserStorage storage)
    {
        _storage = storage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _storage.GetAsync(TokenKey);

        if (string.IsNullOrWhiteSpace(token))
            return new AuthenticationState(Anonymous);

        if (IsExpired(token))
        {
            await LogoutInternal();
            return new AuthenticationState(Anonymous);
        }

        var principal = BuildPrincipal(token);
        return new AuthenticationState(principal);
    }

    public async Task SetTokenAsync(string? token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            await LogoutInternal();
            return;
        }

        await _storage.SetAsync(TokenKey, token);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(BuildPrincipal(token))));
    }

    private ClaimsPrincipal BuildPrincipal(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        var identity = new ClaimsIdentity(jwt.Claims, "jwt");
        return new ClaimsPrincipal(identity);
    }

    private bool IsExpired(string token)
    {
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
        return jwt.ValidTo < DateTime.UtcNow;
    }

    private async Task LogoutInternal()
    {
        await _storage.RemoveAsync(TokenKey);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(Anonymous)));
    }

    private bool IsImpersonating(ClaimsPrincipal user)
    => user.HasClaim(c => c.Type == "impersonator");
}