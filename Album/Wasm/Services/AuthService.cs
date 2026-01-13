using Blazored.LocalStorage;
using DTO;
using System.Net.Http.Json;
using Wasm.Services;

public class AuthService
{
    private readonly ApiHttpClient _api;
    private readonly TokenStore _tokenStore;
    private readonly ILocalStorageService _localStorage;
    private readonly JwtAuthStateProvider _authStateProvider;

    public AuthService(
        ApiHttpClient api,
        TokenStore tokenStore,
        ILocalStorageService localStorage,
        JwtAuthStateProvider authStateProvider)
    {
        _api = api;
        _tokenStore = tokenStore;
        _localStorage = localStorage;
        _authStateProvider = authStateProvider;
    }

    // ---------------------------------------------------------
    // LOGIN
    // ---------------------------------------------------------
    public async Task<bool> LoginAsync(LoginRequest request)
    {
        var http = _api.CreateUnauthenticated(); // IMPORTANT

        var response = await http.PostAsJsonAsync("auth/login", request);
        if (!response.IsSuccessStatusCode)
            return false;

        var token = await response.Content.ReadFromJsonAsync<TokenResponse>();
        if (token is null)
            return false;

        var bundle = new TokenBundle(
            token.Token,
            token.RefreshToken,
            token.ExpiresAt
        );

        await _tokenStore.SetBundleAsync(bundle);
        await _authStateProvider.MarkUserAsAuthenticatedAsync(bundle);

        return true;
    }

    // ---------------------------------------------------------
    // AUTO-REFRESH ON APP START
    // ---------------------------------------------------------
    public async Task<bool> TryAutoRefreshAsync()
    {
        var bundle = await _tokenStore.GetBundleAsync();
        if (bundle is null)
            return false;

        if (!bundle.IsExpired())
            return true;

        var ok = await TryRefreshAsync();
        if (!ok)
        {
            await _tokenStore.ClearAsync();
            await _authStateProvider.MarkUserAsLoggedOutAsync();
            return false;
        }

        return true;
    }

    // ---------------------------------------------------------
    // REFRESH TOKEN
    // ---------------------------------------------------------
    public async Task<bool> TryRefreshAsync()
    {
        var bundle = await _tokenStore.GetBundleAsync();
        if (bundle is null || string.IsNullOrWhiteSpace(bundle.RefreshToken))
            return false;

        var http = _api.CreateUnauthenticated(); // IMPORTANT

        var payload = new
        {
            refreshToken = bundle.RefreshToken
        };

        HttpResponseMessage response;
        try
        {
            response = await http.PostAsJsonAsync("auth/refresh", payload);
        }
        catch
        {
            return false;
        }

        if (!response.IsSuccessStatusCode)
            return false;

        var token = await response.Content.ReadFromJsonAsync<TokenResponse>();
        if (token is null)
            return false;

        var newBundle = new TokenBundle(
            token.Token,
            token.RefreshToken,
            token.ExpiresAt
        );

        await _tokenStore.SetBundleAsync(newBundle);
        await _authStateProvider.MarkUserAsAuthenticatedAsync(newBundle);

        return true;
    }

    // ---------------------------------------------------------
    // LOGOUT
    // ---------------------------------------------------------
    public async Task LogoutAsync()
    {
        try
        {
            var bundle = await _tokenStore.GetBundleAsync();
            var refresh = bundle?.RefreshToken;

            if (!string.IsNullOrWhiteSpace(refresh))
            {
                var http = _api.CreateUnauthenticated(); // IMPORTANT

                var payload = new RefreshRequest
                {
                    RefreshToken = refresh
                };

                await http.PostAsJsonAsync("auth/logout", payload);
            }
        }
        catch
        {
            // Best effort — logout should never block cleanup
        }
        finally
        {
            await _tokenStore.ClearAsync();

            // Remove legacy keys if they exist
            await _localStorage.RemoveItemAsync("accessToken");
            await _localStorage.RemoveItemAsync("refreshToken");
            await _localStorage.RemoveItemAsync("expiresAt");

            await _authStateProvider.MarkUserAsLoggedOutAsync();
        }
    }
}