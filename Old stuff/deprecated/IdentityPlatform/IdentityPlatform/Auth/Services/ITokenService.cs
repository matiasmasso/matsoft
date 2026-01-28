using IdentityPlatform.Auth.Models;

namespace IdentityPlatform.Auth.Services;

public interface ITokenService
{
    string CreateAccessToken(User user, ClientApp app, IEnumerable<string> roles);
    RefreshToken CreateRefreshToken(User user, ClientApp app, string? deviceInfo, string? ipAddress);
}