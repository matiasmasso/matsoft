using IdentityPlatform.Server.Auth.Dtos;
using IdentityPlatform.Server.Auth.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace IdentityPlatform.Server.Auth.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;

    public AuthController(IAuthService auth)
    {
        _auth = auth;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterRequest dto)
        => Ok(await _auth.RegisterAsync(dto));

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginRequest dto)
        => Ok(await _auth.LoginAsync(dto));

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(UserRefreshRequest dto)
        => Ok(await _auth.RefreshAsync(dto));

    [HttpPost("google")]
    public async Task<IActionResult> GoogleLogin(GoogleLoginRequest dto)
        => Ok(await _auth.GoogleLoginAsync(dto));

    [HttpPost("apple")]
    public async Task<IActionResult> AppleLogin(AppleLoginRequest dto)
        => Ok(await _auth.AppleLoginAsync(dto));
}