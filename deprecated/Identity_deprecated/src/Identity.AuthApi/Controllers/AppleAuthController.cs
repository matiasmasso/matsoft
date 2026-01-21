using Identity.AuthApi.DTOs;
using Identity.Domain.Entities;
using Identity.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Identity.AuthApi.Controllers;

[ApiController]
[Route("auth/apple")]
public class AppleAuthController : ControllerBase
{
    private readonly AppleAuthService _apple;
    private readonly ITokenService _tokens;

    public AppleAuthController(AppleAuthService apple, ITokenService tokens)
    {
        _apple = apple;
        _tokens = tokens;
    }

    [HttpGet("start")]
    public IActionResult Start()
    {
        var redirectUri = $"{Request.Scheme}://{Request.Host}/auth/apple/callback";
        var url = _apple.BuildAppleLoginUrl(redirectUri);
        return Redirect(url);
    }

    [HttpGet("callback")]
    public async Task<IActionResult> Callback([FromQuery] string code)
    {
        var user = await _apple.HandleAppleCallbackAsync(code);
        var token = await _tokens.CreateJwtForUserAsync(user);

        return Ok(new AppleCallbackResponse(token));
    }
}