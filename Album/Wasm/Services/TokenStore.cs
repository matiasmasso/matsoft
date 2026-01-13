using Blazored.LocalStorage;
using DTO;
using Wasm.Services;

public sealed class TokenStore : ITokenStore
{
    private readonly ILocalStorageService _localStorage;

    public event Action? BundleChanged;

    private const string AccessKey = "accessToken";
    private const string RefreshKey = "refreshToken";
    private const string ExpiresKey = "expiresAt";

    public TokenStore(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<TokenBundle?> GetBundleAsync()
    {
        var access = await _localStorage.GetItemAsStringAsync(AccessKey);
        var refresh = await _localStorage.GetItemAsStringAsync(RefreshKey);
        var expires = await _localStorage.GetItemAsync<DateTime?>(ExpiresKey);

        if (string.IsNullOrWhiteSpace(access) ||
            string.IsNullOrWhiteSpace(refresh) ||
            expires is null)
        {
            return null;
        }

        return new TokenBundle(access, refresh, expires.Value);
    }

    public async Task SetBundleAsync(TokenBundle bundle)
    {
        await _localStorage.SetItemAsStringAsync(AccessKey, bundle.AccessToken!);
        await _localStorage.SetItemAsStringAsync(RefreshKey, bundle.RefreshToken!);
        await _localStorage.SetItemAsync(ExpiresKey, bundle.ExpiresAtUtc);

        BundleChanged?.Invoke();
    }

    public async Task ClearAsync()
    {
        await _localStorage.RemoveItemAsync(AccessKey);
        await _localStorage.RemoveItemAsync(RefreshKey);
        await _localStorage.RemoveItemAsync(ExpiresKey);

        BundleChanged?.Invoke();
    }
}
