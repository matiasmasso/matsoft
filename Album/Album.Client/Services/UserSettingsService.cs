using Album.Client.Utils;

namespace Album.Client.Services;

public class UserSettingsService
{
    private readonly TypedLocalStorage _storage;
    private const string Key = "user_settings";

    private Dictionary<string, string> _settings = new();

    public UserSettingsService(TypedLocalStorage storage)
    {
        _storage = storage;
    }

    public async Task InitializeAsync()
    {
        var stored = await _storage.GetAsync<Dictionary<string, string>>(Key);
        if (stored is not null)
            _settings = stored;
    }

    public string? Get(string key)
        => _settings.TryGetValue(key, out var value) ? value : null;

    public async Task Set(string key, string value)
    {
        _settings[key] = value;
        await _storage.SetAsync(Key, _settings);
    }

    public async Task Remove(string key)
    {
        if (_settings.Remove(key))
            await _storage.SetAsync(Key, _settings);
    }

    public async Task Clear()
    {
        _settings.Clear();
        await _storage.RemoveAsync(Key);
    }
}