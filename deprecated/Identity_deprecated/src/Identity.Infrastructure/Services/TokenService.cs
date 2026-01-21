using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Infrastructure.Services;

public interface ITokenService
{
    Task<string> CreateJwtForUserAsync(ApplicationUser user);
    Task<string> CreateRefreshTokenAsync(ApplicationUser user);
    Task<(string accessToken, string refreshToken)?> RefreshAsync(string refreshToken);
    Task<string> CreateImpersonationTokenAsync(ApplicationUser target, string adminId);
}

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly UserManager<ApplicationUser> _users;

    public TokenService(IConfiguration config, UserManager<ApplicationUser> users)
    {
        _config = config;
        _users = users;
    }

    public async Task<string> CreateJwtForUserAsync(ApplicationUser user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
        new Claim("name", user.FullName ?? user.Email ?? ""),
        new Claim("email_verified", user.EmailConfirmed.ToString().ToLower())
    };

        if (!string.IsNullOrEmpty(user.FullName))
        {
            var parts = user.FullName.Split(' ', 2);
            claims.Add(new Claim("given_name", parts[0]));
            if (parts.Length > 1)
                claims.Add(new Claim("family_name", parts[1]));
        }

        var roles = await _users.GetRolesAsync(user);
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var token = new JwtSecurityToken(
            issuer: "identity-api",
            audience: "identity-clients",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> CreateRefreshTokenAsync(ApplicationUser user)
    {
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        var entity = new RefreshToken
        {
            UserId = user.Id,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            Revoked = false
        };

        _db.RefreshTokens.Add(entity);
        await _db.SaveChangesAsync();

        return token;
    }

    public async Task<(string accessToken, string refreshToken)?> RefreshAsync(string refreshToken)
    {
        var existing = await _db.RefreshTokens
            .FirstOrDefaultAsync(r => r.Token == refreshToken && !r.Revoked);

        if (existing == null || existing.ExpiresAt < DateTime.UtcNow)
            return null;

        var user = await _users.FindByIdAsync(existing.UserId);
        if (user == null) return null;

        // Optional: if you ever store impersonation info in refresh tokens, reject here

        existing.Revoked = true;

        var newAccess = await CreateJwtForUserAsync(user);
        var newRefresh = await CreateRefreshTokenAsync(user);

        await _db.SaveChangesAsync();

        return (newAccess, newRefresh);
    }

    public async Task<string> CreateImpersonationTokenAsync(ApplicationUser target, string adminId)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, target.Id),
        new Claim(JwtRegisteredClaimNames.Email, target.Email ?? ""),
        new Claim("impersonator", adminId)
    };

        var roles = await _users.GetRolesAsync(target);
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var token = new JwtSecurityToken(
            issuer: "identity-api",
            audience: "identity-clients",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}