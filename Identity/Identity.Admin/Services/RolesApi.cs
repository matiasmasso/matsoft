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
    // ------------------------------------------------------------
    public async Task<List<RoleDto>> GetRolesForAppAsync(Guid appId)
    {
        return await _http.GetFromJsonAsync<List<RoleDto>>(
            $"roles/app/{appId}"
        ) ?? new List<RoleDto>();
    }

    // ------------------------------------------------------------
    // POST /roles
    // ------------------------------------------------------------
    public async Task<RoleDto> CreateRoleAsync(CreateRoleRequest request)
    {
        var response = await _http.PostAsJsonAsync("roles", request);
        response.EnsureSuccessStatusCode();

        // Controller returns CreatedAtAction with no body → fetch roles again
        // or return a minimal DTO
        return new RoleDto
        {
            Id = Guid.NewGuid(), // caller should refresh list anyway
            Name = request.Name,
            ApplicationId = request.ApplicationId
        };
    }

    // ------------------------------------------------------------
    // PUT /roles/{roleId}
    // ------------------------------------------------------------
    public async Task UpdateRoleAsync(Guid roleId, UpdateRoleRequest request)
    {
        var response = await _http.PutAsJsonAsync($"roles/{roleId}", request);
        response.EnsureSuccessStatusCode();
    }

    // ------------------------------------------------------------
    // DELETE /roles/{roleId}
    // ------------------------------------------------------------
    public async Task DeleteRoleAsync(Guid roleId)
    {
        var response = await _http.DeleteAsync($"roles/{roleId}");
        response.EnsureSuccessStatusCode();
    }
}