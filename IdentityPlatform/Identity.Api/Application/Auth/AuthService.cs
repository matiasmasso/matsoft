using Identity.Api.Domain.Users;
using Identity.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Api.Application.Auth;

public class AuthService
{
    private readonly IdentityDbContext _db;
    private readonly JwtOptions _jwt;
    private readonly IPasswordHasher<User> _hasher;

    public AuthService(IdentityDbContext db, IOptions<JwtOptions> jwt, IPasswordHasher<User> hasher)
    {
        _db = db;
        _jwt = jwt.Value;
        _hasher = hasher;
    }

    public async Task<(string accessToken, string refreshToken)> LoginAsync(string email, string password)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);

        if (user == null)
            throw new AuthenticationException("Invalid credentials");

        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);

        if (result == PasswordVerificationResult.Failed)
            throw new AuthenticationException("Invalid credentials");

        var accessToken = GenerateAccessToken(user);
        var refreshToken = await GenerateRefreshToken(user.Id);

        return (accessToken, refreshToken);
    }

    public async Task RegisterAsync(string email, string password)
    {
        // Check if email already exists
        if (await _db.Users.AnyAsync(x => x.Email == email))
            throw new Exception("Email already registered");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            IsActive = true
        };

        // Hash password using built-in ASP.NET Core hasher
        user.PasswordHash = _hasher.HashPassword(user, password);

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
    }

    private string GenerateAccessToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SigningKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.AccessTokenMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<string> GenerateRefreshToken(Guid userId)
    {
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        var refresh = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddDays(_jwt.RefreshTokenDays),
            Revoked = false,
            CreatedAt = DateTime.UtcNow
        };

        _db.RefreshTokens.Add(refresh);
        await _db.SaveChangesAsync();

        return token;
    }

    public async Task<(string access, string refresh)> RefreshAsync(string refreshToken)
    {
        var stored = await _db.RefreshTokens
            .FirstOrDefaultAsync(x => x.Token == refreshToken);

        if (stored == null)
            throw new Exception("Invalid refresh token");

        if (stored.Revoked)
            throw new Exception("Refresh token has been revoked");

        if (stored.ExpiresAt < DateTime.UtcNow)
            throw new Exception("Refresh token has expired");

        var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == stored.UserId);

        if (user == null)
            throw new Exception("User not found");

        // Revoke old token
        stored.Revoked = true;

        // Issue new tokens
        var newAccess = GenerateAccessToken(user);
        var newRefresh = await GenerateRefreshToken(user.Id);

        await _db.SaveChangesAsync();

        return (newAccess, newRefresh);
    }

    public async Task<UserProfile> GetProfileAsync(Guid userId)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
            throw new Exception("User not found");

        return new UserProfile
        {
            Id = user.Id,
            Email = user.Email
        };
    }
}