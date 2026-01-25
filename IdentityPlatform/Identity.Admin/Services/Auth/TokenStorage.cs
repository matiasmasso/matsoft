using Identity.Admin.Utils;

namespace Identity.Admin.Services.Auth;

public class TokenStorage
{
    private readonly LocalStorage _storage;

    public TokenStorage(LocalStorage storage)
    {
        _storage = storage;
    }

    public async Task SaveTokensAsync(string access, string refresh)
    {
        await _storage.SetAsync("access_token", access);
        await _storage.SetAsync("refresh_token", refresh);
    }

    public async Task<string?> GetAccessTokenAsync()
    {
        return await _storage.GetAsync("access_token");
    }

    public async Task<string?> GetRefreshTokenAsync()
    {
        return await _storage.GetAsync("refresh_token");
    }

    public async Task<(string? access, string? refresh)> GetTokensAsync()
    {
        var access = await _storage.GetAsync("access_token");
        var refresh = await _storage.GetAsync("refresh_token");
        return (access, refresh);
    }

    public async Task ClearAsync()
    {
        await _storage.RemoveAsync("access_token");
        await _storage.RemoveAsync("refresh_token");
    }
}