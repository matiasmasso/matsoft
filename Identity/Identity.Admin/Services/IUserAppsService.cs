using Identity.Client.Http;
using Identity.Contracts.Apps;
using Identity.Contracts.Users;
using static System.Net.WebRequestMethods;

public interface IUserAppsService
{
    Task<Result<List<UserAppDto>>> GetAllAsync(Guid userId);
    Task<Result<bool>> EnrollAsync(Guid userId, Guid appId);
    Task<Result<bool>> UnEnrollAsync(Guid userId, Guid appId);
}
