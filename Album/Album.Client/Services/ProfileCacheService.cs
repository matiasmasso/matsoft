using Album.Client.Utils;

namespace Album.Client.Services;

public class ProfileCacheService
{
    private readonly UserProfileService _profileService;
    private readonly TypedLocalStorage _storage;

    private UserProfileDto? _cached;
    private const string CacheKey = "profile_cache";

    public ProfileCacheService(UserProfileService profileService, TypedLocalStorage storage)
    {
        _profileService = profileService;
        _storage = storage;
    }

    public async Task<UserProfileDto?> GetAsync(bool forceRefresh = false)
    {
        if (!forceRefresh && _cached is not null)
            return _cached;

        // Try local storage
        if (!forceRefresh)
        {
            var stored = await _storage.GetAsync<UserProfileDto>(CacheKey);
            if (stored is not null)
            {
                _cached = stored;
                return stored;
            }
        }

        // Fetch from API
        var profile = await _profileService.GetAsync();
        if (profile is not null)
        {
            _cached = profile;
            await _storage.SetAsync(CacheKey, profile);
        }

        return profile;
    }

    public async Task RefreshAsync()
    {
        await GetAsync(forceRefresh: true);
    }

    public async Task ClearAsync()
    {
        _cached = null;
        await _storage.RemoveAsync(CacheKey);
    }
}