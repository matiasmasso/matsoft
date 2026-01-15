using Identity.DTO;
using System.Net.Http.Json;

namespace Identity.Admin.Services;

public class RolesApi
{
    private readonly HttpClient _http;

    public RolesApi(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<RoleDto>> GetRolesForAppAsync(Guid appId)
    {
        return await _http.GetFromJsonAsync<List<RoleDto>>(
            $"roles/app/{appId}");
    }

    public async Task CreateRoleAsync(CreateRoleRequest request)
    {
        await _http.PostAsJsonAsync("roles", request);
    }

    public async Task UpdateRoleAsync(Guid roleId, UpdateRoleRequest request)
    {
        await _http.PutAsJsonAsync($"roles/{roleId}", request);
    }

    public async Task DeleteRoleAsync(Guid roleId)
    {
        await _http.DeleteAsync($"roles/{roleId}");
    }
}
