using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using Identity.Client.Wasm.Authentication;
using Identity.Shared.DTOs;
using Microsoft.AspNetCore.Components.Authorization;

namespace Identity.Client.Wasm.Authentication;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly BrowserStorage _storage;
    private readonly CustomAuthStateProvider _authStateProvider;

    private const string TokenKey = "access_token";

    public AuthService(
        HttpClient http,
        BrowserStorage storage,
        CustomAuthStateProvider authStateProvider)
    {
        _http = http;
        _storage = storage;
        _authStateProvider = authStateProvider;
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        var response = await _http.PostAsJsonAsync("auth/login", new { email, password });

        if (!response.IsSuccessStatusCode)
            return false;

        var result = await response.Content.ReadFromJsonAsync<TokenDto>();
        if (result is null || string.IsNullOrWhiteSpace(result.AccessToken))
            return false;

        await _storage.SetAsync(TokenKey, result.AccessToken);
        await _authStateProvider.SetTokenAsync(result.AccessToken);

        return true;
    }

    public async Task LogoutAsync()
    {
        await _storage.RemoveAsync(TokenKey);
        await _authStateProvider.SetTokenAsync(null);
    }

    public async Task<string?> GetTokenAsync()
    {
        return await _storage.GetAsync(TokenKey);
    }

    private const string RefreshKey = "refresh_token";

    public async Task<bool> LoginAsync(string email, string password)
    {
        var response = await _http.PostAsJsonAsync("auth/login", new { email, password });
        if (!response.IsSuccessStatusCode) return false;

        var json = await response.Content.ReadFromJsonAsync<LoginResultDto>();
        if (json is null) return false;

        await _storage.SetAsync(TokenKey, json.AccessToken);
        await _storage.SetAsync(RefreshKey, json.RefreshToken);
        await _authStateProvider.SetTokenAsync(json.AccessToken);

        return true;
    }

    public async Task<bool> TryRefreshAsync()
    {
        var refresh = await _storage.GetAsync(RefreshKey);
        if (string.IsNullOrWhiteSpace(refresh)) return false;

        var response = await _http.PostAsJsonAsync("auth/refresh", new { refreshToken = refresh });
        if (!response.IsSuccessStatusCode) return false;

        var json = await response.Content.ReadFromJsonAsync<LoginResultDto>();
        if (json is null) return false;

        await _storage.SetAsync(TokenKey, json.AccessToken);
        await _storage.SetAsync(RefreshKey, json.RefreshToken);
        await _authStateProvider.SetTokenAsync(json.AccessToken);

        return true;
    }

    public record LoginResultDto(string AccessToken, string RefreshToken);

    private const string TokenKey = "access_token";
    private const string RefreshKey = "refresh_token";
    private const string OriginalTokenKey = "original_access_token";

    public async Task SetTokenDirectAsync(string accessToken, bool isImpersonation = false)
    {
        if (isImpersonation)
        {
            var current = await _storage.GetAsync(TokenKey);
            if (!string.IsNullOrWhiteSpace(current))
            {
                await _storage.SetAsync(OriginalTokenKey, current);
            }

            // When impersonating, we do NOT keep refresh token
            await _storage.RemoveAsync(RefreshKey);
        }

        await _storage.SetAsync(TokenKey, accessToken);
        await _authStateProvider.SetTokenAsync(accessToken);
    }

    public async Task<bool> RestoreOriginalTokenAsync()
    {
        var original = await _storage.GetAsync(OriginalTokenKey);
        if (string.IsNullOrWhiteSpace(original))
            return false;

        await _storage.SetAsync(TokenKey, original);
        await _storage.RemoveAsync(OriginalTokenKey);

        // No refresh token change here: original admin refresh token is still in place
        await _authStateProvider.SetTokenAsync(original);
        return true;
    }
}