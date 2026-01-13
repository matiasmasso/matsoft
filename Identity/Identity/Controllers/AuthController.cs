using Identity.Data;
using Identity.Domain.Entities;
using Identity.Infrastructure.Security;
using Identity.Models.Auth;
using Identity.Services;
using Identity.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _users;
        private readonly JwtTokenService _jwt;
        private readonly PasswordHasher _hasher;
        private readonly RefreshTokenService _refreshTokens;
        private readonly Services.IEmailSender _emailSender;

        public AuthController(
            IUserService users,
            JwtTokenService jwt,
            PasswordHasher hasher,
            RefreshTokenService refreshTokens,
            Services.IEmailSender emailSender)
        {
            _users = users;
            _jwt = jwt;
            _hasher = hasher;
            _refreshTokens = refreshTokens;
            _emailSender = emailSender;
        }



        [HttpGet("test-email")]
        public async Task<IActionResult> TestEmail([FromServices] Services.IEmailSender sender)
        {
            await sender.SendAsync("your-email@example.com", "Test", "Hello Matias!");
            return Ok("Sent");
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(Models.Auth.RegisterRequest request)
        {
            if (await _users.Exists(request.Email))
                return BadRequest("Email already exists");

            var user = await _users.Create(request.Email, request.Password);
            return Ok(new { user.UserId, user.Email });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Models.Auth.LoginRequest request)
        {
            var user = await _users.GetByEmail(request.Email);
            if (user == null || !_hasher.Verify(request.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials");

            var roles = await _users.GetRoles(user.UserId);
            var apps = await _users.GetApps(user.UserId);

            var accessToken = _jwt.GenerateToken(user, roles, apps);
            var refreshToken = await _refreshTokens.CreateRefreshToken(user.UserId);

            return Ok(new
            {
                accessToken,
                refreshToken = refreshToken.Token
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenRequest request)
        {
            var storedToken = await _refreshTokens.GetValidToken(request.RefreshToken);
            if (storedToken == null)
                return Unauthorized("Invalid or expired refresh token");

            var user = await _users.GetById(storedToken.UserId);
            if (user == null)
                return Unauthorized("User not found");

            // Rotate token (invalidate old one)
            await _refreshTokens.RevokeToken(storedToken.TokenId);

            // Issue new refresh token
            var newRefreshToken = await _refreshTokens.CreateRefreshToken(user.UserId);

            // Issue new access token
            var roles = await _users.GetRoles(user.UserId);
            var apps = await _users.GetApps(user.UserId);
            var newAccessToken = _jwt.GenerateToken(user, roles, apps);

            return Ok(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken.Token
            });
        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout(RefreshTokenRequest request)
        {
            var token = await _refreshTokens.GetValidToken(request.RefreshToken);
            if (token != null)
                await _refreshTokens.RevokeToken(token.TokenId);

            return Ok("Logged out");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(Models.DTOs.ForgotPasswordRequest request)
        {
            // 1. Always return OK to avoid user enumeration
            var user = await _users.GetByEmail(request.Email);
            if (user == null)
                return Ok();

            // 2. Create a refresh token (repurposed as a password-reset token)
            var token = await _refreshTokens.CreateRefreshToken(user.UserId);

            // 3. Override expiration for password reset (1 hour)
            await _refreshTokens.UpdateExpiration(token.TokenId, DateTime.UtcNow.AddHours(1));

            // 4. Encode token for URL
            var encodedToken = WebEncoders.Base64UrlEncode(
                Encoding.UTF8.GetBytes(token.Token)
            );

            // 5. Build callback URL for WASM
            var callbackUrl =
                $"{request.ClientUrl}/reset-password?email={user.Email}&token={encodedToken}";

            // 6. Send email
            await _emailSender.SendAsync(
                user.Email,
                "Password Reset",
                $"Click the link to reset your password: {callbackUrl}"
            );

            return Ok();
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(Models.DTOs.ResetPasswordRequest request)
        {
            // 1. Get user
            var user = await _users.GetByEmail(request.Email);
            if (user == null)
                return BadRequest("Invalid request");

            // 2. Decode token from URL-safe Base64
            string rawToken;
            try
            {
                rawToken = Encoding.UTF8.GetString(
                    WebEncoders.Base64UrlDecode(request.Token)
                );
            }
            catch
            {
                return BadRequest("Invalid token format");
            }

            // 3. Validate token using your RefreshTokenService
            var token = await _refreshTokens.GetValidToken(rawToken);
            if (token == null || token.UserId != user.UserId)
                return BadRequest("Invalid or expired token");

            // 4. Update password
            user.PasswordHash = _hasher.Hash(request.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;
            await _users.Update(user);

            // 5. Revoke token using the service
            await _refreshTokens.RevokeToken(token.TokenId);

            return Ok("Password has been reset successfully");
        }
    }
}
