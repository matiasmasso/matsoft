using Identity.Contracts.Apps;

public sealed class AppSecretsService(HttpClient http) : IAppSecretsService
{
    public Task<List<AppSecretDto>> GetAllAsync(Guid appId)
        => http.SafeGetAsync<List<AppSecretDto>>($"apps/{appId}/secrets");

    public Task<AppSecretDto> GetAsync(Guid appId, Guid secretId)
        => http.SafeGetAsync<AppSecretDto>($"apps/{appId}/secrets/{secretId}");

    public Task CreateAsync(CreateAppSecretRequest request)
        => http.SafePostAsync($"apps/{request.AppId}/secrets", request);

    public Task UpdateAsync(UpdateAppSecretRequest request)
        => http.SafePutAsync($"apps/{request.AppId}/secrets/{request.Id}", request);

    public Task DeleteAsync(Guid appId, Guid secretId)
        => http.SafeDeleteAsync($"apps/{appId}/secrets/{secretId}");
}