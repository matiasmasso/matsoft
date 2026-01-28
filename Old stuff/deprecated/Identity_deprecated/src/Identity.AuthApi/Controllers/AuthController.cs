using Identity.AuthApi.DTOs;
using Identity.Domain.Entities;
using Identity.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.AuthApi.Controllers;

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

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req)
    {
        var user = await _users.FindByEmailAsync(req.Email);
        if (user == null)
            return Unauthorized("Invalid credentials");

        var result = await _signIn.CheckPasswordSignInAsync(user, req.Password, false);
        if (!result.Succeeded)
            return Unauthorized("Invalid credentials");

        var token = await _tokens.CreateJwtForUserAsync(user);
        var refreshToken = await _tokens.CreateRefreshTokenAsync(user);

        return Ok(new
        {
            accessToken = token,
            refreshToken = refreshToken
        });
    }

    public record RefreshRequest(string RefreshToken);

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest req)
    {
        var result = await _tokens.RefreshAsync(req.RefreshToken);
        if (result == null) return Unauthorized();

        return Ok(new
        {
            accessToken = result.Value.accessToken,
            refreshToken = result.Value.refreshToken
        });
    }
}