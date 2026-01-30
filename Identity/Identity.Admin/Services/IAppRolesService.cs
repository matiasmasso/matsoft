using Identity.Client.Http;
using Identity.Contracts.Apps;

public interface IAppRolesService
{
    Task<Result<List<AppRoleDto>>> GetAllAsync(Guid appId);
    Task<Result<AppRoleDto>> GetAsync(Guid appId, Guid roleId);
    Task<Result<AppRoleDto>> CreateAsync(Guid appId, CreateAppRoleRequest request);
    Task<Result<AppRoleDto>> UpdateAsync(Guid appId, UpdateAppRoleRequest request);
    Task<Result<bool>> DeleteAsync(Guid appId, Guid roleId);
}
