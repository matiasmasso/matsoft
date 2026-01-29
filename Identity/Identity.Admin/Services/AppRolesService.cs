using Identity.Contracts.Apps;

public sealed class AppRolesService(HttpClient http) : IAppRolesService
{
    public Task<List<AppRoleDto>> GetAllAsync(Guid appId)
        => http.SafeGetAsync<List<AppRoleDto>>($"apps/{appId}/roles");

    public Task<AppRoleDto> GetAsync(Guid appId, Guid roleId)
        => http.SafeGetAsync<AppRoleDto>($"apps/{appId}/roles/{roleId}");

    public Task CreateAsync(CreateAppRoleRequest request)
        => http.SafePostAsync($"apps/{request.AppId}/roles", request);

    public Task UpdateAsync(UpdateAppRoleRequest request)
        => http.SafePutAsync($"apps/{request.AppId}/roles/{request.Id}", request);

    public Task DeleteAsync(Guid appId, Guid roleId)
        => http.SafeDeleteAsync($"apps/{appId}/roles/{roleId}");
}