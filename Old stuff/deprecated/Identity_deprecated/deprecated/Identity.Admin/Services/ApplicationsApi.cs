using Identity.DTO;
using System.Net.Http.Json;

namespace Identity.Admin.Services;

public class ApplicationsApi : _BaseApiClient
{
    public ApplicationsApi(HttpClient http) : base(http) { }


    // ------------------------------------------------------------
    // GET /applications
    // Returns all applications
    // ------------------------------------------------------------
    public Task<(List<ApplicationDto>? Applications, List<string>? Errors)> GetApplicationsAsync()
    {
        return ReadAsync<List<ApplicationDto>>(() =>
            _http.GetAsync("applications")
        );
    }

    // ------------------------------------------------------------
    // GET /applications/{id}
    // Returns a single application
    // ------------------------------------------------------------
    public Task<(ApplicationDto? Application, List<string>? Errors)> GetApplicationAsync(Guid appId)
    {
        return ReadAsync<ApplicationDto>(() =>
            _http.GetAsync($"applications/{appId}")
        );
    }

    // ------------------------------------------------------------
    // POST /applications
    // Creates a new application
    // ------------------------------------------------------------
    public Task<List<string>?> CreateApplicationAsync(ApplicationDto value)
    {
        return CallAsync(() =>
            _http.PostAsJsonAsync("applications", value)
        );
    }

    // ------------------------------------------------------------
    // PUT /applications/{id}
    // Updates an existing application
    // ------------------------------------------------------------
    public Task<List<string>?> UpdateApplicationAsync(ApplicationDto value)
    {
        return CallAsync(() =>
            _http.PostAsJsonAsync($"applications", value)
        );
    }

    // ------------------------------------------------------------
    // DELETE /applications/{id}
    // Deletes an application
    // ------------------------------------------------------------
    public Task<List<string>?> DeleteApplicationAsync(ApplicationDto value)
    {
        var appId = (Guid)value.ApplicationId!;
        return CallAsync(() =>
            _http.DeleteAsync($"applications/{appId}")
        );

    }
}
