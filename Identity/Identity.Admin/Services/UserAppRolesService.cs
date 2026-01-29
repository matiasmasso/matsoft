using Identity.Contracts.Apps;
using Identity.Contracts.Users;

public sealed class UserAppRolesService(HttpClient http) : IUserAppRolesService
{
    public Task<List<UserAppDto>> GetUserAppsAsync(Guid userId)
        => http.SafeGetAsync<List<UserAppDto>>($"users/{userId}/apps");

    public Task<List<AppRoleAssignmentDto>> GetRoleAssignmentsAsync(Guid userId, Guid appId)
        => http.SafeGetAsync<List<AppRoleAssignmentDto>>($"users/{userId}/apps/{appId}/roles");

    public Task UpdateAssignmentsAsync(UpdateUserAppRolesRequest request)
        => http.SafePutAsync(
            $"users/{request.UserId}/apps/{request.AppId}/roles",
            request
        );
}