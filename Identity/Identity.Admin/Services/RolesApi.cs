using System.Net.Http.Json;
using Identity.DTO;

namespace Identity.Admin.Services;

public class RolesApi
{
    private readonly HttpClient _http;

    public RolesApi(HttpClient http)
    {
        _http = http;
    }

    // ------------------------------------------------------------
    // GET /roles/app/{appId}
    // Returns all roles for a given application
    // ------------------------------------------------------------
    public async Task<List<RoleDto>> GetRolesForAppAsync(Guid appId)
    {
        return await _http.GetFromJsonAsync<List<RoleDto>>($"roles/app/{appId}")
               ?? new List<RoleDto>();
    }

    // ------------------------------------------------------------
    // POST /roles
    // Creates a new role
    // ------------------------------------------------------------
    public async Task CreateRoleAsync(CreateRoleRequest request)
    {
        var response = await _http.PostAsJsonAsync("roles", request);
        response.EnsureSuccessStatusCode();
    }

    // ------------------------------------------------------------
    // PUT /roles/{roleId}
    // Updates an existing role
    // ------------------------------------------------------------
    public async Task UpdateRoleAsync(Guid roleId, UpdateRoleRequest request)
    {
        var response = await _http.PutAsJsonAsync($"roles/{roleId}", request);
        response.EnsureSuccessStatusCode();
    }

    // ------------------------------------------------------------
    // DELETE /roles/{roleId}
    // Deletes a role
    // ------------------------------------------------------------
    public async Task DeleteRoleAsync(Guid roleId)
    {
        var response = await _http.DeleteAsync($"roles/{roleId}");
        response.EnsureSuccessStatusCode();
    }
}
