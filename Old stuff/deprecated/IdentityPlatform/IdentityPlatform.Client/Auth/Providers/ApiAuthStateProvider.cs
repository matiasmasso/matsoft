using System.Security.Claims;
using System.Text.Json;
using IdentityPlatform.Client.Auth.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace IdentityPlatform.Client.Auth.Providers;

public class ApiAuthStateProvider : AuthenticationStateProvider
{
    private readonly ITokenStorage _tokens;

    public ApiAuthStateProvider(ITokenStorage tokens)
    {
        _tokens = tokens;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var accessToken = await _tokens.GetAccessTokenAsync();

        if (string.IsNullOrWhiteSpace(accessToken))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var claims = ParseClaimsFromJwt(accessToken);
        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }

    public void NotifyAuthStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var json = Base64UrlDecode(payload);

        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

        var claims = new List<Claim>();

        if (keyValuePairs is null)
            return claims;

        foreach (var kvp in keyValuePairs)
        {
            if (kvp.Value is JsonElement element && element.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in element.EnumerateArray())
                    claims.Add(new Claim(kvp.Key, item.ToString()));
            }
            else
            {
                claims.Add(new Claim(kvp.Key, kvp.Value.ToString()!));
            }
        }

        return claims;
    }

    private static string Base64UrlDecode(string input)
    {
        input = input.Replace('-', '+').Replace('_', '/');
        switch (input.Length % 4)
        {
            case 2: input += "=="; break;
            case 3: input += "="; break;
        }
        var bytes = Convert.FromBase64String(input);
        return System.Text.Encoding.UTF8.GetString(bytes);
    }
}