using Identity.Domain.Entities;
using Identity.DTO;

namespace Identity.Services
{
    public interface ITokenService
    {
        Task<TokenResponse> GenerateTokensAsync(ApplicationUser user, Guid applicationId);
        Task<TokenResponse> RefreshAsync(RefreshRequest request);
        Task RevokeRefreshTokenAsync(string refreshToken);
    }

}
