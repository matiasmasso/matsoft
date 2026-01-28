using IdentityPlatform.Server.Auth.Models;

namespace IdentityPlatform.Server.Auth.Services;

public interface ITokenService
{
    string CreateAccessToken(User user, ClientApp app, IEnumerable<string> roles);
    RefreshToken CreateRefreshToken(User user, ClientApp app, string? deviceInfo, string? ipAddress);
}