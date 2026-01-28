using Identity.Api.Models;

namespace Identity.Api.Services;

public interface ITokenService
{
    Task<(string accessToken, string refreshToken)> IssueTokensAsync(ApplicationUser user, string clientId);
    Task<(string accessToken, string refreshToken)?> RefreshAsync(string refreshToken, string clientId);
}