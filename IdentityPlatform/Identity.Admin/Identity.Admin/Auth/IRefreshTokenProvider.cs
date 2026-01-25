namespace Identity.Admin.Auth;

public interface IRefreshTokenProvider
{
    Task<string?> GetRefreshTokenAsync();
}