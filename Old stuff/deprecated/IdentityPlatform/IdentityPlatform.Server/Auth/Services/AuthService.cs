using IdentityPlatform.Server.Auth.Dtos;
using IdentityPlatform.Server.Auth.Models;
using IdentityPlatform.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityPlatform.Server.Auth.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly IPasswordHasher<User> _hasher;
    private readonly ITokenService _tokens;

    public AuthService(AppDbContext db, IPasswordHasher<User> hasher, ITokenService tokens)
    {
        _db = db;
        _hasher = hasher;
        _tokens = tokens;
    }

    public async Task<AuthResult> RegisterAsync(UserRegisterRequest request)
    {
        var app = await _db.Apps.FindAsync(request.AppId)
                  ?? throw new InvalidOperationException("App not found");

        var existing = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (existing is not null)
            throw new InvalidOperationException("User already exists");

        var user = new User
        {
            Email = request.Email
        };
        user.PasswordHash = _hasher.HashPassword(user, request.Password);

        // Default role: Viewer (or whatever you decide)
        var defaultRole = await _db.Roles.FirstOrDefaultAsync(r => r.Name == "Viewer")
                         ?? throw new InvalidOperationException("Default role not configured");

        var enrollment = new UserAppEnrollment
        {
            User = user,
            App = app,
            Role = defaultRole,
            IsActive = true
        };

        _db.Users.Add(user);
        _db.Enrollments.Add(enrollment);
        await _db.SaveChangesAsync();

        return await IssueTokensAsync(user, app, request.AppId, null, null);
    }

    public async Task<AuthResult> LoginAsync(UserLoginRequest request)
    {
        var app = await _db.Apps.FindAsync(request.AppId)
                  ?? throw new InvalidOperationException("App not found");

        var user = await _db.Users
            .Include(u => u.Enrollments)
            .ThenInclude(e => e.Role)
            .FirstOrDefaultAsync(u => u.Email == request.Email)
            ?? throw new InvalidOperationException("Invalid credentials");

        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (result == PasswordVerificationResult.Failed)
            throw new InvalidOperationException("Invalid credentials");

        var enrollment = user.Enrollments.FirstOrDefault(e => e.AppId == app.Id && e.IsActive);
        if (enrollment is null)
            throw new InvalidOperationException("User not enrolled in this app");

        return await IssueTokensAsync(user, app, request.AppId, null, null);
    }

    public async Task<AuthResult> RefreshAsync(UserRefreshRequest request)
    {
        var token = await _db.RefreshTokens
            .FirstOrDefaultAsync(t => t.Token == request.RefreshToken && t.AppId == request.AppId);

        if (token is null || token.RevokedAt is not null || token.ExpiresAt <= DateTime.UtcNow)
            throw new InvalidOperationException("Invalid refresh token");

        var user = await _db.Users
            .Include(u => u.Enrollments)
            .ThenInclude(e => e.Role)
            .FirstOrDefaultAsync(u => u.Id == token.UserId)
            ?? throw new InvalidOperationException("User not found");

        var app = await _db.Apps.FindAsync(token.AppId)
                  ?? throw new InvalidOperationException("App not found");

        // Rotate refresh token
        token.RevokedAt = DateTime.UtcNow;
        var newToken = _tokens.CreateRefreshToken(user, app, token.DeviceInfo, token.IpAddress);
        _db.RefreshTokens.Add(newToken);

        await _db.SaveChangesAsync();

        var roles = user.Enrollments
            .Where(e => e.AppId == app.Id && e.IsActive)
            .Select(e => e.Role.Name)
            .ToList();

        var accessToken = _tokens.CreateAccessToken(user, app, roles);

        return new AuthResult(accessToken, newToken.Token, DateTime.UtcNow.AddMinutes(15));
    }

    public async Task<AuthResult> GoogleLoginAsync(GoogleLoginRequest request)
    {
        // TODO: validate Google IdToken, extract email + subject
        // For now, skeleton:
        var email = "extracted-from-google@example.com";
        var googleId = "google-subject-id";

        return await ExternalLoginAsync(email, googleId, provider: "Google", request.AppId);
    }

    public async Task<AuthResult> AppleLoginAsync(AppleLoginRequest request)
    {
        // TODO: validate Apple IdToken, extract email + subject
        var email = "extracted-from-apple@example.com";
        var appleId = "apple-subject-id";

        return await ExternalLoginAsync(email, appleId, provider: "Apple", request.AppId);
    }

    private async Task<AuthResult> ExternalLoginAsync(string email, string externalId, string provider, Guid appId)
    {
        var app = await _db.Apps.FindAsync(appId)
                  ?? throw new InvalidOperationException("App not found");

        var user = await _db.Users
            .Include(u => u.Enrollments)
            .ThenInclude(e => e.Role)
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user is null)
        {
            user = new User { Email = email };
            if (provider == "Google") user.GoogleId = externalId;
            if (provider == "Apple") user.AppleId = externalId;

            var defaultRole = await _db.Roles.FirstOrDefaultAsync(r => r.Name == "Viewer")
                             ?? throw new InvalidOperationException("Default role not configured");

            var enrollment = new UserAppEnrollment
            {
                User = user,
                App = app,
                Role = defaultRole,
                IsActive = true
            };

            _db.Users.Add(user);
            _db.Enrollments.Add(enrollment);
            await _db.SaveChangesAsync();
        }
        else
        {
            if (provider == "Google" && user.GoogleId is null)
                user.GoogleId = externalId;
            if (provider == "Apple" && user.AppleId is null)
                user.AppleId = externalId;

            var enrollment = user.Enrollments.FirstOrDefault(e => e.AppId == app.Id);
            if (enrollment is null)
            {
                var defaultRole = await _db.Roles.FirstOrDefaultAsync(r => r.Name == "Viewer")
                                 ?? throw new InvalidOperationException("Default role not configured");

                enrollment = new UserAppEnrollment
                {
                    User = user,
                    App = app,
                    Role = defaultRole,
                    IsActive = true
                };
                _db.Enrollments.Add(enrollment);
            }

            await _db.SaveChangesAsync();
        }

        return await IssueTokensAsync(user, app, appId, null, null);
    }

    private async Task<AuthResult> IssueTokensAsync(User user, ClientApp app, Guid appId, string? deviceInfo, string? ipAddress)
    {
        var roles = user.Enrollments
            .Where(e => e.AppId == appId && e.IsActive)
            .Select(e => e.Role.Name)
            .ToList();

        var accessToken = _tokens.CreateAccessToken(user, app, roles);
        var refreshToken = _tokens.CreateRefreshToken(user, app, deviceInfo, ipAddress);

        _db.RefreshTokens.Add(refreshToken);
        await _db.SaveChangesAsync();

        return new AuthResult(accessToken, refreshToken.Token, DateTime.UtcNow.AddMinutes(15));
    }
}