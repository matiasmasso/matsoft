using Identity.Contracts.Apps;

public interface IAppRolesService
{
    Task<List<AppRoleDto>> GetAllAsync(Guid appId);
    Task<AppRoleDto> GetAsync(Guid appId, Guid roleId);
    Task CreateAsync(CreateAppRoleRequest request);
    Task UpdateAsync(UpdateAppRoleRequest request);
    Task DeleteAsync(Guid appId, Guid roleId);
}