using Identity.Api.Infrastructure.Errors;
using Identity.DTO;
using System.Net.Http.Json;

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


    public async Task<(List<ApplicationDto>? Applications, List<string>? Errors)> GetApplicationsAsync()
    {
        try
        {
            var response = await _http.GetAsync("applications");

            if (!response.IsSuccessStatusCode)
            {
                var payload = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                return (null, payload?.Errors ?? new List<string> { "Unknown error" });
            }

            var apps = await response.Content.ReadFromJsonAsync<List<ApplicationDto>>();
            return (apps ?? new List<ApplicationDto>(), null);
        }
        catch (Exception ex)
        {
            return (null, new List<string> { ex.Message });
        }
    }


    // ------------------------------------------------------------
    // GET /applications/{id}
    // Returns a single application
    // ------------------------------------------------------------
    public async Task<(ApplicationDto? Applications, List<string>? Errors)> GetApplicationAsync(Guid appId)
    {
        //return await _http.GetFromJsonAsync<ApplicationDto>($"applications/{appId}");
        var response = await _http.GetAsync($"applications/{appId}");

        if (!response.IsSuccessStatusCode)
        {
            var payload = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return (null, payload?.Errors ?? new List<string> { "Unknown error" });
        }
        var app = await response.Content.ReadFromJsonAsync<ApplicationDto>();
        return (app, null);
    }

    // ------------------------------------------------------------
    // POST /applications
    // Creates a new application
    // ------------------------------------------------------------
    public async Task<List<string>?> CreateApplicationAsync(CreateApplicationRequest request)
    {
        var response = await _http.PostAsJsonAsync("applications", request);
        if (!response.IsSuccessStatusCode)
        {
            var payload = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return (payload?.Errors ?? new List<string> { "Unknown error" });
        }
        return null;
    }

    // ------------------------------------------------------------
    // PUT /applications/{id}
    // Updates an existing application
    // ------------------------------------------------------------
    public async Task<List<string>?> UpdateApplicationAsync(Guid appId, UpdateApplicationRequest request)
    {
        var response = await _http.PutAsJsonAsync($"applications/{appId}", request);
        if (!response.IsSuccessStatusCode)
        {
            var payload = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return (payload?.Errors ?? new List<string> { "Unknown error" });
        }
        return null;
    }

    // ------------------------------------------------------------
    // DELETE /applications/{id}
    // Deletes an application
    // ------------------------------------------------------------
    public async Task<List<string>?> DeleteApplicationAsync(Guid appId)
    {
        var response = await _http.DeleteAsync($"applications/{appId}");
        if (!response.IsSuccessStatusCode)
        {
            var payload = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return (payload?.Errors ?? new List<string> { "Unknown error" });
        }
        return null;
    }
}
