using IdentityPlatform.Auth.Dtos;

namespace IdentityPlatform.Auth.Services;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(UserRegisterRequest request);
    Task<AuthResult> LoginAsync(UserLoginRequest request);
    Task<AuthResult> RefreshAsync(UserRefreshRequest request);
    Task<AuthResult> GoogleLoginAsync(GoogleLoginRequest request);
    Task<AuthResult> AppleLoginAsync(AppleLoginRequest request);
}