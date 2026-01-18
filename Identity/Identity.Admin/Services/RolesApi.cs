using Identity.Api.Infrastructure.Errors;
using Identity.DTO;
using System.Data;
using System.Net.Http.Json;

namespace Identity.Admin.Services;

public class RolesApi : _BaseApiClient
{
    public RolesApi(HttpClient http) : base(http) { }


    // ------------------------------------------------------------
    // GET /roles/app/{appId}
    // ------------------------------------------------------------
    public Task<(List<RoleDto>? Roles, List<string>? Errors)> GetRolesForAppAsync(Guid appId)
    {
        return ReadAsync<List<RoleDto>>(() =>
            _http.GetAsync($"roles/app/{appId}")
        );
    }


    // ------------------------------------------------------------
    // POST /roles
    // ------------------------------------------------------------
    public Task<List<string>?> CreateRoleAsync(CreateRoleRequest request)
    {
        return CallAsync(() =>
            _http.PostAsJsonAsync("roles", request)
        );
    }

    // ------------------------------------------------------------
    // PUT /roles/{roleId}
    // ------------------------------------------------------------
    public Task<List<string>?> UpdateRoleAsync(Guid roleId, UpdateRoleRequest request)
    {
        return CallAsync(() =>
            _http.PutAsJsonAsync($"roles/{roleId}", request)
        );
    }

    // ------------------------------------------------------------
    // DELETE /roles/{roleId}
    // ------------------------------------------------------------
    public Task<List<string>?> DeleteRoleAsync(Guid roleId)
    {
        return CallAsync(() =>
            _http.DeleteAsync($"roles/{roleId}")
        );
    }
}