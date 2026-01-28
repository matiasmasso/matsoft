using Identity.Api.Application.Auth;
using Microsoft.AspNetCore.Mvc;

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

    public record RegisterRequest(string Username, string Email, string Password);

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _auth.RegisterAsync(request.Username, request.Email, request.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new { status = "registered" });
    }
}