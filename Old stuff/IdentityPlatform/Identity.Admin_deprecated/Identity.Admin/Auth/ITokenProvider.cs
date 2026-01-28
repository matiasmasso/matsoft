namespace Identity.Admin.Auth;

public interface ITokenProvider
{
    Task<string?> GetAccessTokenAsync();
}
