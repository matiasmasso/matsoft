using Identity.Admin.Utils;

namespace Identity.Admin.Services.Auth;

public class LocalStorageTokenProvider : ITokenProvider
{
    private readonly LocalStorage _storage;

    public LocalStorageTokenProvider(LocalStorage storage)
    {
        _storage = storage;
    }

    public async Task<string?> GetTokenAsync()
    {
        return await _storage.GetAsync("access_token");
    }
}