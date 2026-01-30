using Identity.Client.Http;
using Identity.Contracts.Apps;
using Identity.Contracts.Users;

public interface IUserAppRolesService
{
    Task<Result<List<UserAppDto>>> GetUserAppsAsync(Guid userId);
    Task<Result<List<AppRoleAssignmentDto>>> GetRoleAssignmentsAsync(Guid userId, Guid appId);
    Task<Result<bool>> UpdateAssignmentsAsync(UpdateUserAppRolesRequest request);
}
