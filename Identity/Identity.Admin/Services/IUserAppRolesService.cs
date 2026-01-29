using Identity.Contracts.Apps;
using Identity.Contracts.Users;

public interface IUserAppRolesService
{
    Task<List<UserAppDto>> GetUserAppsAsync(Guid userId);
    Task<List<AppRoleAssignmentDto>> GetRoleAssignmentsAsync(Guid userId, Guid appId);
    Task UpdateAssignmentsAsync(UpdateUserAppRolesRequest request);
}