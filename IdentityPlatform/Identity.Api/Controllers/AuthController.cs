using Identity.Api.Application.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading;


namespace Identity.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _auth;

    public AuthController(AuthService auth)
    {
        _auth = auth;
    }

    public record RegisterRequest(string Email, string Password);

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        await _auth.RegisterAsync(request.Email, request.Password);
        return Ok(new { status = "registered" });
    }
    public record LoginRequest(string Email, string Password);

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        try
        {
            var (access, refresh) = await _auth.LoginAsync(request.Email, request.Password);
            return Ok(new { access, refresh });

        }
        catch (AuthenticationException authEx)
        {
            return Unauthorized(new { error = authEx.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    public record RefreshRequest(string RefreshToken);

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshRequest request)
    {
        var (access, refresh) = await _auth.RefreshAsync(request.RefreshToken);
        return Ok(new { access, refresh });
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (userIdClaim == null)
            return Unauthorized();

        var userId = Guid.Parse(userIdClaim);

        var profile = await _auth.GetFullProfileAsync(userId);

        return Ok(profile);
    }
}