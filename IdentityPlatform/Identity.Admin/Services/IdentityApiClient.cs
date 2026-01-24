using System.Net.Http.Json;
using Identity.Admin.Models;

namespace Identity.Admin.Services;

public class IdentityApiClient
{
    private readonly HttpClient _http;

    public IdentityApiClient(HttpClient http)
    {
        _http = http;
    }

    // ------------------------------------------------------------
    // Apps
    // ------------------------------------------------------------
    public async Task<List<AppDto>?> GetAppsAsync()
    {
        var response = await _http.GetAsync("api/apps");
        return await response.ReadOrThrowAsync<List<AppDto>>();
        //await _http.GetFromJsonAsync<List<AppDto>>("api/apps");
    }


    public async Task<AppDto?> CreateAppAsync(CreateAppRequest req)
    {
        var response = await _http.PostAsJsonAsync("api/apps", req);
        return await response.ReadOrThrowAsync<AppDto>();
    }

    // ------------------------------------------------------------
    // Roles
    // ------------------------------------------------------------
    public async Task<List<RoleDto>?> GetRolesAsync(Guid appId)
    {
        var response = await _http.GetAsync($"api/apps/{appId}/roles");
        return await response.ReadOrThrowAsync<List<RoleDto>>();
    // await _http.GetFromJsonAsync<List<RoleDto>>($"api/apps/{appId}/roles");

    }

    public async Task<RoleDto?> CreateRoleAsync(Guid appId, CreateRoleRequest req)
    {
        var response = await _http.PostAsJsonAsync($"api/apps/{appId}/roles", req);
        return await response.Content.ReadFromJsonAsync<RoleDto>();
    }

    public async Task AssignRoleAsync(Guid appId, Guid userId, AssignRoleRequest req)
        => await _http.PostAsJsonAsync($"api/apps/{appId}/users/{userId}/roles", req);

    public async Task RemoveRoleAsync(Guid appId, Guid userId, Guid roleId)
        => await _http.DeleteAsync($"api/apps/{appId}/users/{userId}/roles/{roleId}");

    // ------------------------------------------------------------
    // Users in app
    // ------------------------------------------------------------
    public async Task<List<UserEnrollmentDto>?> GetUsersInAppAsync(Guid appId)
        => await _http.GetFromJsonAsync<List<UserEnrollmentDto>>($"api/apps/{appId}/users");

    public async Task EnrollUserAsync(Guid appId, Guid userId)
        => await _http.PostAsync($"api/apps/{appId}/users/{userId}/enroll", null);

    // ------------------------------------------------------------
    // User search
    // ------------------------------------------------------------
    public async Task<UserDto?> FindUserByEmailAsync(string email)
        => await _http.GetFromJsonAsync<UserDto>($"api/users/find?email={email}");
}