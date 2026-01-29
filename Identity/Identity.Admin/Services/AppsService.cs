using Identity.Contracts.Apps;

public sealed class AppsService(HttpClient http) : IAppsService
{
    public Task<List<AppDto>> GetAllAsync()
        => http.SafeGetAsync<List<AppDto>>("apps");

    public Task<AppDto> GetAsync(Guid id)
        => http.SafeGetAsync<AppDto>($"apps/{id}");

    public Task CreateAsync(AppDto dto)
        => http.SafePostAsync("apps", dto);

    public Task UpdateAsync(AppDto dto)
        => http.SafePutAsync($"apps/{dto.Id}", dto);

    public Task DeleteAsync(Guid id)
        => http.SafeDeleteAsync($"apps/{id}");
}