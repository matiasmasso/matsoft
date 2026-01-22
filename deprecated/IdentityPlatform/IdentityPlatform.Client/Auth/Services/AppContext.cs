using Blazored.LocalStorage;

namespace IdentityPlatform.Client.Auth.Services;

public class AppContext : IAppContext
{
    private readonly ILocalStorageService _storage;

    public Guid? CurrentAppId { get; private set; }

    public AppContext(ILocalStorageService storage)
    {
        _storage = storage;
    }

    public async Task InitializeAsync()
    {
        CurrentAppId = await _storage.GetItemAsync<Guid?>("current_app_id");
    }

    public async Task SetCurrentAppAsync(Guid appId)
    {
        CurrentAppId = appId;
        await _storage.SetItemAsync("current_app_id", appId);
    }
}