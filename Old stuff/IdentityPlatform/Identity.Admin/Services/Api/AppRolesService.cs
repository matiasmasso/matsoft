using Identity.Admin.Http;
using Identity.Admin.Models.Roles;
using Identity.Admin.Services.Api;
using static System.Net.WebRequestMethods;

public class AppRolesService : BaseApiService
{
    public AppRolesService(SafeHttpClient http) : base(http) { }

    public Task<List<AppRoleDto>?> GetAllAsync(Guid appId)
        => Http.GetAsync<List<AppRoleDto>>($"apps/{appId}/roles");

    public Task<AppRoleDto?> CreateAsync(Guid appId, CreateAppRoleRequest request)
        => Http.PostAsync<CreateAppRoleRequest, AppRoleDto>($"apps/{appId}/roles", request);

    public Task<AppRoleDto?> UpdateAsync(Guid appId, Guid roleId, UpdateAppRoleRequest request)
        => Http.PutAsync<UpdateAppRoleRequest, AppRoleDto>($"apps/{appId}/roles/{roleId}", request);

    public Task<bool> DeleteAsync(Guid appId, Guid roleId)
        => Http.DeleteAsync($"apps/{appId}/roles/{roleId}");
}