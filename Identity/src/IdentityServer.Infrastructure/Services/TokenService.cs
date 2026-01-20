using IdentityServer.Application.Interfaces;
using IdentityServer.Infrastructure.Identity;
using IdentityServer.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace IdentityServer.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly ApplicationDbContext _db;

    public TokenService(IConfiguration config, ApplicationDbContext db)
    {
        _config = config;
        _db = db;
    }

    public string GenerateAccessToken(ApplicationUser user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:SigningKey"]));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new Claim("name", user.UserName ?? "")
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken(ApplicationUser user)
    {
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        var refresh = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = token,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(30)
        };

        _db.RefreshTokens.Add(refresh);
        _db.SaveChanges();

        return token;
    }
}