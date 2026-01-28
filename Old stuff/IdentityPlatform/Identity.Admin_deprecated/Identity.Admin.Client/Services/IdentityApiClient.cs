using ApiCore.Http;
using Identity.Shared.Models;

namespace Identity.Admin.Client.Services;

public class IdentityApiClient
{
    private readonly SafeHttpClient _http;

    public IdentityApiClient(SafeHttpClient http)
    {
        _http = http;
    }

    // ------------------------------------------------------------
    // Apps
    // ------------------------------------------------------------
    public Task<List<AppDto>?> GetAppsAsync()
        => _http.GetAsync<List<AppDto>>("apps");

    public Task<AppDto?> CreateAppAsync(CreateAppRequest req)
        => _http.PostAsync<CreateAppRequest, AppDto>("apps", req);

    public Task<bool> DeleteAppAsync(Guid appId)
        => _http.DeleteAsync($"apps/{appId}");

    // ------------------------------------------------------------
    // Roles
    // ------------------------------------------------------------
    public Task<List<RoleDto>?> GetRolesAsync(Guid appId)
        => _http.GetAsync<List<RoleDto>>($"apps/{appId}/roles");

    public Task<RoleDto?> CreateRoleAsync(Guid appId, CreateRoleRequest req)
        => _http.PostAsync<CreateRoleRequest, RoleDto>($"apps/{appId}/roles", req);

    public Task<bool> AssignRoleAsync(Guid appId, Guid userId, AssignRoleRequest req)
        => _http.PostAsync<AssignRoleRequest, object?>(
            $"apps/{appId}/users/{userId}/roles", req
        ).ContinueWith(_ => true);

    public Task<bool> RemoveRoleAsync(Guid appId, Guid userId, Guid roleId)
        => _http.DeleteAsync($"apps/{appId}/users/{userId}/roles/{roleId}");

    // ------------------------------------------------------------
    // Users in app
    // ------------------------------------------------------------
    public Task<List<UserEnrollmentDto>?> GetUsersInAppAsync(Guid appId)
        => _http.GetAsync<List<UserEnrollmentDto>>($"apps/{appId}/users");

    public Task<bool> EnrollUserAsync(Guid appId, Guid userId)
        => _http.PostAsync<object?, object?>(
            $"apps/{appId}/users/{userId}/enroll", null
        ).ContinueWith(_ => true);

    // ------------------------------------------------------------
    // User search
    // ------------------------------------------------------------
    public Task<UserDto?> FindUserByEmailAsync(string email)
        => _http.GetAsync<UserDto>($"users/find?email={email}");
}