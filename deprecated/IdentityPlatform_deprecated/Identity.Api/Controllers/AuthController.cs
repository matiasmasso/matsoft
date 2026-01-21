using Identity.Api.Models;
using Identity.Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _users;
    private readonly SignInManager<ApplicationUser> _signIn;
    private readonly ITokenService _tokens;

    public AuthController(
        UserManager<ApplicationUser> users,
        SignInManager<ApplicationUser> signIn,
        ITokenService tokens)
    {
        _users = users;
        _signIn = signIn;
        _tokens = tokens;
    }

    public record LoginRequest(string Email, string Password, string ClientId);
    public record LoginResponse(string AccessToken, string RefreshToken);
    public record RefreshRequest(string RefreshToken, string ClientId);

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req)
    {
        var user = await _users.FindByEmailAsync(req.Email);
        if (user == null) return Unauthorized();

        var result = await _signIn.CheckPasswordSignInAsync(user, req.Password, lockoutOnFailure: true);
        if (!result.Succeeded) return Unauthorized();

        var tokens = await _tokens.IssueTokensAsync(user, req.ClientId);
        return Ok(new LoginResponse(tokens.accessToken, tokens.refreshToken));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest req)
    {
        var tokens = await _tokens.RefreshAsync(req.RefreshToken, req.ClientId);
        if (tokens == null) return Unauthorized();

        return Ok(new LoginResponse(tokens.Value.accessToken, tokens.Value.refreshToken));
    }
}