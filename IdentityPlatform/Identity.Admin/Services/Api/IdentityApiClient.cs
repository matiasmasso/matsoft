using System.Net.Http.Json;
using Identity.Admin.Models.Apps;

namespace Identity.Admin.Services.Api;

public class IdentityApiClient
{
    private readonly HttpClient _http;

    public IdentityApiClient(HttpClient http) => _http = http;

    public Task<List<AppDto>?> GetAppsAsync() =>
        _http.GetFromJsonAsync<List<AppDto>>("apps");

    public Task CreateAppAsync(CreateAppRequest req) =>
        _http.PostAsJsonAsync("apps", req);

    public Task DeleteAppAsync(Guid id) =>
        _http.DeleteAsync($"apps/{id}");
}