using Blazored.LocalStorage;

namespace IdentityPlatform.Client.Auth.Services;

public class LocalTokenStorage : ITokenStorage
{
    private readonly ILocalStorageService _storage;

    public LocalTokenStorage(ILocalStorageService storage)
    {
        _storage = storage;
    }

    public async Task SaveTokensAsync(string accessToken, string refreshToken)
    {
        await _storage.SetItemAsync("access_token", accessToken);
        await _storage.SetItemAsync("refresh_token", refreshToken);
    }

    public async Task<string?> GetAccessTokenAsync()
    {
        return await _storage.GetItemAsync<string>("access_token");
    }

    public async Task<string?> GetRefreshTokenAsync()
    {
        return await _storage.GetItemAsync<string>("refresh_token");
    }

    public async Task ClearAsync()
    {
        await _storage.RemoveItemAsync("access_token");
        await _storage.RemoveItemAsync("refresh_token");
    }
}