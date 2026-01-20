using IdentityServer.Api.Models.OAuth;
using IdentityServer.Application.Interfaces;
using IdentityServer.Application.Services;

using IdentityServer.Domain.Entities;
using IdentityServer.Infrastructure.Identity;
using IdentityServer.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;


namespace IdentityServer.Api.Controllers;

[ApiController]
public class AuthorizationController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;

    public AuthorizationController(
        ApplicationDbContext db,
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        ITokenService tokenService)
    {
        _db = db;
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    // ============================
    // 1. Authorization Endpoint
    // ============================
    [HttpGet("~/connect/authorize")]
    public async Task<IActionResult> Authorize()
    {
        var request = new AuthorizationRequest
        {
            ClientId = Request.Query["client_id"],
            RedirectUri = Request.Query["redirect_uri"],
            ResponseType = Request.Query["response_type"],
            Scope = Request.Query["scope"],
            CodeChallenge = Request.Query["code_challenge"],
            CodeChallengeMethod = Request.Query["code_challenge_method"]
        };

        // 1. Validate client
        var client = await _db.OdbcClients
            .FirstOrDefaultAsync(c => c.ClientId == request.ClientId);

        if (client == null)
            return BadRequest("Invalid client_id");

        if (!client.RedirectUris.Contains(request.RedirectUri))
            return BadRequest("Invalid redirect_uri");

        // 2. If user not authenticated → redirect to login
        if (!User.Identity!.IsAuthenticated)
        {
            return Challenge(
                authenticationSchemes: IdentityConstants.ApplicationScheme,
                properties: new AuthenticationProperties
                {
                    RedirectUri = Request.Path + QueryString.Create(Request.Query)
                });
        }

        // 3. Get authenticated user
        var user = await _userManager.GetUserAsync(User)
            ?? throw new InvalidOperationException("User not found.");

        // 4. Generate authorization code
        var code = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32))
            .Replace("+", "-")
            .Replace("/", "_")
            .TrimEnd('=');

        var authCode = new AuthorizationCode
        {
            Id = Guid.NewGuid(),
            Code = code,
            ClientId = request.ClientId,
            RedirectUri = request.RedirectUri,
            CodeChallenge = request.CodeChallenge,
            CodeChallengeMethod = request.CodeChallengeMethod,
            UserId = user.Id.ToString(),
            ExpiresAt = DateTime.UtcNow.AddMinutes(5)
        };

        _db.AuthorizationCodes.Add(authCode);
        await _db.SaveChangesAsync();

        // 5. Redirect back to client
        var redirect = $"{request.RedirectUri}?code={code}";
        return Redirect(redirect);
    }

    // ============================
    // 2. Token Endpoint
    // ============================
    [HttpPost("~/connect/token")]
    public async Task<IActionResult> Token([FromForm] TokenRequest request)
    {
        // 1. Validate client
        var client = await _db.OdbcClients
            .FirstOrDefaultAsync(c => c.ClientId == request.ClientId);

        if (client == null)
            return BadRequest(new { error = "invalid_client" });

        // 2. Validate authorization code
        var authCode = await _db.AuthorizationCodes
            .FirstOrDefaultAsync(c => c.Code == request.Code);

        if (authCode == null || authCode.ExpiresAt < DateTime.UtcNow)
            return BadRequest(new { error = "invalid_grant" });

        if (authCode.ClientId != request.ClientId)
            return BadRequest(new { error = "invalid_grant" });

        if (authCode.RedirectUri != request.RedirectUri)
            return BadRequest(new { error = "invalid_grant" });

        // 3. Validate PKCE
        var computedChallenge = ComputePkceChallenge(request.CodeVerifier);

        if (computedChallenge != authCode.CodeChallenge)
            return BadRequest(new { error = "invalid_grant" });

        // 4. Load user
        var user = await _userManager.FindByIdAsync(authCode.UserId);
        if (user == null)
            return BadRequest(new { error = "invalid_grant" });

        // 5. Generate tokens
        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken(user);

        // 6. Remove authorization code (one-time use)
        _db.AuthorizationCodes.Remove(authCode);
        await _db.SaveChangesAsync();

        // 7. Return OAuth2-compliant response
        return Ok(new
        {
            access_token = accessToken,
            refresh_token = refreshToken,
            token_type = "Bearer",
            expires_in = 3600
        });
    }

    private static string ComputePkceChallenge(string verifier)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(verifier));
        return Convert.ToBase64String(bytes)
            .Replace("+", "-")
            .Replace("/", "_")
            .TrimEnd('=');
    }

    // ============================
    // 3. Logout Endpoint
    // ============================
    [HttpPost("~/connect/logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new { message = "Logged out" });
    }
}