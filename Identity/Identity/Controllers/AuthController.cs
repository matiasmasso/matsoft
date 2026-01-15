using Identity.Attributes;
using Identity.Data;
using Identity.Domain.Entities;
using Identity.Models.DTOs;
using Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


//Usage:
//[AuthorizeAppRole("InventoryApp", "Manager")]
//[HttpGet("stock")] public IActionResult GetStock()
//{ return Ok("Stock data"); }

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly ApplicationDbContext _db;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        ITokenService tokenService,
        ApplicationDbContext db)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _db = db;
    }

    // ------------------------------------------------------------
    // POST /auth/login
    // ------------------------------------------------------------
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.UserName == request.UserName);

        if (user == null || !user.IsActive)
            return Unauthorized("Invalid credentials");

        var valid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!valid)
            return Unauthorized("Invalid credentials");

        var tokens = await _tokenService.GenerateTokensAsync(user, request.ApplicationId);
        return Ok(tokens);
    }

    // ------------------------------------------------------------
    // POST /auth/refresh
    // ------------------------------------------------------------
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
    {
        try
        {
            var tokens = await _tokenService.RefreshAsync(request);
            return Ok(tokens);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }

    // ------------------------------------------------------------
    // POST /auth/revoke
    // ------------------------------------------------------------
    [HttpPost("revoke")]
    public async Task<IActionResult> Revoke([FromBody] RevokeRequest request)
    {
        await _tokenService.RevokeRefreshTokenAsync(request.RefreshToken);
        return Ok(new { Message = "Refresh token revoked" });
    }
}
