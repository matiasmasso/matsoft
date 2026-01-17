using Identity.Api.Infrastructure.Errors;
using Identity.DTO;
using System.Data;
using System.Net.Http.Json;

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

    public async Task<(List<RoleDto>? Roles, List<string>? Errors)> GetRolesForAppAsync(Guid appId)
    {
        try
        {
            var response = await _http.GetAsync($"roles/app/{appId}");

            if (!response.IsSuccessStatusCode)
            {
                var payload = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                return (null, payload?.Errors ?? new List<string> { "Unknown error" });
            }

            var roles = await response.Content.ReadFromJsonAsync<List<RoleDto>>();
            return (roles, null);
        }
        catch (Exception ex)
        {
            return (null, new List<string> { ex.Message });
        }
    }

    // ------------------------------------------------------------
    // POST /roles
    // ------------------------------------------------------------
    public async Task<(RoleDto? Role, List<string>? Errors)> CreateRoleAsync(CreateRoleRequest request)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("roles", request);
            if (!response.IsSuccessStatusCode)
            {
                var payload = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                return (null, payload?.Errors ?? new List<string> { "Unknown error" });
            }
            // Controller returns CreatedAtAction with no body → fetch roles again
            // or return a minimal DTO
            var role = new RoleDto
            {
                Id = Guid.NewGuid(), // caller should refresh list anyway
                Name = request.Name,
                ApplicationId = request.ApplicationId
            };
            return (role, null);
        }
        catch (Exception ex)
        {
            return (null, new List<string> { ex.Message });
        }


    }

    // ------------------------------------------------------------
    // PUT /roles/{roleId}
    // ------------------------------------------------------------
    public async Task<List<string>?> UpdateRoleAsync(Guid roleId, UpdateRoleRequest request)
    {
        var response = await _http.PutAsJsonAsync($"roles/{roleId}", request);
        if (!response.IsSuccessStatusCode)
        {
            var payload = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return (payload?.Errors ?? new List<string> { "Unknown error" });
        }
        return null;
    }

    // ------------------------------------------------------------
    // DELETE /roles/{roleId}
    // ------------------------------------------------------------
    public async Task<List<string>?> DeleteRoleAsync(Guid roleId)
    {
        var response = await _http.DeleteAsync($"roles/{roleId}");
        if (!response.IsSuccessStatusCode)
        {
            var payload = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return (payload?.Errors ?? new List<string> { "Unknown error" });
        }
        return null;
    }
}