using System.Net.Http.Json;

namespace Identity.Admin.Services;

public class IdentityApiClient
{
    private readonly HttpClient _http;

    public IdentityApiClient(HttpClient http)
    {
        _http = http;
    }

    // -------------------------
    // Apps
    // -------------------------
    public Task<List<AppDto>?> GetAppsAsync()
        => _http.GetFromJsonAsync<List<AppDto>>("api/apps");

    public Task<AppDto?> CreateAppAsync(CreateAppRequest req)
        => _http.PostAsJsonAsync("api/apps", req)
                .Result.Content.ReadFromJsonAsync<AppDto>();

    // -------------------------
    // Roles
    // -------------------------
    public Task<List<RoleDto>?> GetRolesAsync(Guid appId)
        => _http.GetFromJsonAsync<List<RoleDto>>($"api/apps/{appId}/roles");

    public Task<RoleDto?> CreateRoleAsync(Guid appId, CreateRoleRequest req)
        => _http.PostAsJsonAsync($"api/apps/{appId}/roles", req)
                .Result.Content.ReadFromJsonAsync<RoleDto>();

    // -------------------------
    // Users in app
    // -------------------------
    public Task<List<UserEnrollmentDto>?> GetUsersInAppAsync(Guid appId)
        => _http.GetFromJsonAsync<List<UserEnrollmentDto>>($"api/apps/{appId}/users");

    public Task EnrollUserAsync(Guid appId, Guid userId)
        => _http.PostAsync($"api/apps/{appId}/users/{userId}/enroll", null);

    public Task AssignRoleAsync(Guid appId, Guid userId, Guid roleId)
        => _http.PostAsJsonAsync(
            $"api/apps/{appId}/users/{userId}/roles",
            new { RoleId = roleId }
        );

    public Task RemoveRoleAsync(Guid appId, Guid userId, Guid roleId)
        => _http.DeleteAsync($"api/apps/{appId}/users/{userId}/roles/{roleId}");
}