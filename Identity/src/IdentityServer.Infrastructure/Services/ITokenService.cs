using IdentityServer.Infrastructure.Identity;

namespace IdentityServer.Application.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(ApplicationUser user);
    string GenerateRefreshToken(ApplicationUser user);
}