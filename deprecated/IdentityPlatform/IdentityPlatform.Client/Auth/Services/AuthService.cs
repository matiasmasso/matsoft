using System.Net.Http.Json;
using IdentityPlatform.Client.Dtos;

namespace IdentityPlatform.Client.Auth.Services;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly ITokenStorage _tokens;

    public AuthService(HttpClient http, ITokenStorage tokens)
    {
        _http = http;
        _tokens = tokens;
    }

    public async Task<AuthResult?> LoginAsync(UserLoginRequest request)
    {
        var result = await _http.PostAsJsonAsync("auth/login", request);
        if (!result.IsSuccessStatusCode)
            return null;

        var auth = await result.Content.ReadFromJsonAsync<AuthResult>();
        if (auth is not null)
            await _tokens.SaveTokensAsync(auth.AccessToken, auth.RefreshToken);

        return auth;
    }

    public async Task<AuthResult?> RegisterAsync(UserRegisterRequest request)
    {
        var result = await _http.PostAsJsonAsync("auth/register", request);
        if (!result.IsSuccessStatusCode)
            return null;

        var auth = await result.Content.ReadFromJsonAsync<AuthResult>();
        if (auth is not null)
            await _tokens.SaveTokensAsync(auth.AccessToken, auth.RefreshToken);

        return auth;
    }

    public async Task LogoutAsync()
    {
        await _tokens.ClearAsync();
    }

    public async Task<AuthResult?> GoogleLoginAsync(string idToken, Guid appId)
    {
        var result = await _http.PostAsJsonAsync("auth/google",
            new { IdToken = idToken, AppId = appId });

        if (!result.IsSuccessStatusCode)
            return null;

        var auth = await result.Content.ReadFromJsonAsync<AuthResult>();
        if (auth is not null)
            await _tokens.SaveTokensAsync(auth.AccessToken, auth.RefreshToken);

        return auth;
    }

    public async Task<AuthResult?> AppleLoginAsync(string idToken, Guid appId)
    {
        var result = await _http.PostAsJsonAsync("auth/apple",
            new { IdToken = idToken, AppId = appId });

        if (!result.IsSuccessStatusCode)
            return null;

        var auth = await result.Content.ReadFromJsonAsync<AuthResult>();
        if (auth is not null)
            await _tokens.SaveTokensAsync(auth.AccessToken, auth.RefreshToken);

        return auth;
    }
}