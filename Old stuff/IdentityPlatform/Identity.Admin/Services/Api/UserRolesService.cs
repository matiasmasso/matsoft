using Identity.Admin.Http;
using Identity.Admin.Services.Api;
using static System.Net.WebRequestMethods;

public class UserRolesService : BaseApiService
{
    public UserRolesService(SafeHttpClient http) : base(http) { }

    public Task<List<Guid>?> GetAssignedRolesAsync(Guid appId, Guid userId)
        => Http.GetAsync<List<Guid>>($"apps/{appId}/users/{userId}/roles");

    public Task<bool> AssignAsync(Guid appId, Guid userId, Guid roleId)
        => Http.PostAsync($"apps/{appId}/users/{userId}/roles/{roleId}");

    public Task<bool> UnassignAsync(Guid appId, Guid userId, Guid roleId)
        => Http.DeleteAsync($"apps/{appId}/users/{userId}/roles/{roleId}");
}