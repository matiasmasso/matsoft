using System.Net.Http.Json;
using Identity.DTO;

namespace Identity.Admin.Services;

public class ApplicationsApi
{
    private readonly HttpClient _http;

    public ApplicationsApi(HttpClient http)
    {
        _http = http;
    }

    // ------------------------------------------------------------
    // GET /applications
    // Returns all applications
    // ------------------------------------------------------------
    public async Task<List<ApplicationDto>> GetApplicationsAsync()
    {
        return await _http.GetFromJsonAsync<List<ApplicationDto>>("applications")
               ?? new List<ApplicationDto>();
    }

    // ------------------------------------------------------------
    // GET /applications/{id}
    // Returns a single application
    // ------------------------------------------------------------
    public async Task<ApplicationDto?> GetApplicationAsync(Guid appId)
    {
        return await _http.GetFromJsonAsync<ApplicationDto>($"applications/{appId}");
    }

    // ------------------------------------------------------------
    // POST /applications
    // Creates a new application
    // ------------------------------------------------------------
    public async Task CreateApplicationAsync(CreateApplicationRequest request)
    {
        var response = await _http.PostAsJsonAsync("applications", request);
        response.EnsureSuccessStatusCode();
    }

    // ------------------------------------------------------------
    // PUT /applications/{id}
    // Updates an existing application
    // ------------------------------------------------------------
    public async Task UpdateApplicationAsync(Guid appId, UpdateApplicationRequest request)
    {
        var response = await _http.PutAsJsonAsync($"applications/{appId}", request);
        response.EnsureSuccessStatusCode();
    }

    // ------------------------------------------------------------
    // DELETE /applications/{id}
    // Deletes an application
    // ------------------------------------------------------------
    public async Task DeleteApplicationAsync(Guid appId)
    {
        var response = await _http.DeleteAsync($"applications/{appId}");
        response.EnsureSuccessStatusCode();
    }
}
