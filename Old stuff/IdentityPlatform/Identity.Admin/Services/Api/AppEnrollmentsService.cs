using Identity.Admin.Http;
using Identity.Admin.Models.Users;

namespace Identity.Admin.Services.Api
{
    public class AppEnrollmentsService : BaseApiService
    {
        public AppEnrollmentsService(SafeHttpClient http) : base(http) { }

        public Task<List<UserDto>?> GetUsersAsync(Guid appId)
            => Http.GetAsync<List<UserDto>>($"apps/{appId}/users");

        public Task<bool> AddUserAsync(Guid appId, Guid userId)
            => Http.PostAsync($"apps/{appId}/users/{userId}");

        public Task<bool> RemoveUserAsync(Guid appId, Guid userId)
            => Http.DeleteAsync($"apps/{appId}/users/{userId}");
    }
}
