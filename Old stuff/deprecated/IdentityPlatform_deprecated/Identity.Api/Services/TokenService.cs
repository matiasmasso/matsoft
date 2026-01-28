using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Identity.Api.Data;
using Identity.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Api.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly UserManager<ApplicationUser> _users;
    private readonly IdentityDbContext _db;

    public TokenService(IConfiguration config, UserManager<ApplicationUser> users, IdentityDbContext db)
    {
        _config = config;
        _users = users;
        _db = db;
    }

    public async Task<(string accessToken, string refreshToken)> IssueTokensAsync(ApplicationUser user, string clientId)
    {
        var access = await CreateJwtAsync(user, clientId);
        var refresh = await CreateRefreshTokenAsync(user, clientId);
        return (access, refresh);
    }

    public async Task<(string accessToken, string refreshToken)?> RefreshAsync(string refreshToken, string clientId)
    {
        var existing = await _db.RefreshTokens
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Token == refreshToken && !r.Revoked && r.ClientId == clientId);

        if (existing == null || existing.ExpiresAt < DateTime.UtcNow)
            return null;

        existing.Revoked = true;

        var access = await CreateJwtAsync(existing.User, clientId);
        var newRefresh = await CreateRefreshTokenAsync(existing.User, clientId);

        await _db.SaveChangesAsync();

        return (access, newRefresh);
    }

    private async Task<string> CreateJwtAsync(ApplicationUser user, string clientId)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new("name", user.FullName ?? user.Email ?? ""),
            new("client_id", clientId)
        };

        var roles = await _users.GetRolesAsync(user);
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var appRoles = await _db.AppUserRoles
            .Include(aur => aur.App)
            .Where(aur => aur.UserId == user.Id && aur.App.ClientId == clientId)
            .ToListAsync();

        foreach (var ar in appRoles)
        {
            claims.Add(new Claim("app_role", $"{ar.App.ClientId}:{ar.RoleName}"));
        }

        var token = new JwtSecurityToken(
            issuer: "identity-api",
            audience: clientId,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<string> CreateRefreshTokenAsync(ApplicationUser user, string clientId)
    {
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        var entity = new RefreshToken
        {
            UserId = user.Id,
            ClientId = clientId,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            Revoked = false
        };

        _db.RefreshTokens.Add(entity);
        await _db.SaveChangesAsync();

        return token;
    }
}