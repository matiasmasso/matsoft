using Identity.Client.Http;
using Identity.Contracts.Apps;
using Identity.Contracts.Users;


public sealed class UserAppsService(SafeHttp http) : IUserAppsService
{
    public Task<Result<List<UserAppDto>>> GetAllAsync(Guid userId)
        => http.Get<List<UserAppDto>>($"userapps/{userId}");

    public Task<Result<bool>> EnrollAsync(Guid userId, Guid appId)
        => http.Post<bool>($"userapps/{userId}/{appId}", new { });


    public Task<Result<bool>> UnEnrollAsync(Guid userId, Guid appId)
        => http.Delete($"userapps/{userId}/{appId}");

}
