using Identity.Client.Http;
using Identity.Contracts.Apps;
using Identity.Contracts.Users;

public sealed class UserAppRolesService(SafeHttp http) : IUserAppRolesService
{
    public Task<Result<List<UserAppDto>>> GetUserAppsAsync(Guid userId)
        => http.Get<List<UserAppDto>>($"users/{userId}/apps");

    public Task<Result<List<AppRoleAssignmentDto>>> GetRoleAssignmentsAsync(Guid userId, Guid appId)
        => http.Get<List<AppRoleAssignmentDto>>($"users/{userId}/apps/{appId}/roles");

    public Task<Result<bool>> UpdateAssignmentsAsync(UpdateUserAppRolesRequest request)
        => http.Put<bool>(
            $"users/{request.UserId}/apps/{request.AppId}/roles",
            request,
            "Role assignments updated successfully"
        );
}
