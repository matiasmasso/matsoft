using Identity.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Identity.AuthApi.Controllers;

[ApiController]
[Route("auth")]
public class ProfileController : ControllerBase
{
    [HttpGet("me")]
    [Authorize]
    public IActionResult Me()
    {
        var user = HttpContext.User;

        var id = user.FindFirstValue(ClaimTypes.NameIdentifier)
                 ?? user.FindFirstValue("sub");

        var email = user.FindFirstValue(ClaimTypes.Email)
                    ?? user.FindFirstValue("email");

        var name = user.FindFirstValue("name")
                   ?? email;

        var roles = user.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

        return Ok(new
        {
            Id = id,
            Email = email,
            Name = name,
            Roles = roles
        });
    }

    public record ChangePasswordRequest(string CurrentPassword, string NewPassword);

    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword(
        [FromServices] UserManager<ApplicationUser> users,
        [FromBody] ChangePasswordRequest req)
    {
        var user = await users.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var result = await users.ChangePasswordAsync(user, req.CurrentPassword, req.NewPassword);
        if (!result.Succeeded) return BadRequest(result.Errors);

        return NoContent();
    }

    public record ChangeEmailRequest(string NewEmail);

    [HttpPost("change-email")]
    [Authorize]
    public async Task<IActionResult> ChangeEmail(
        [FromServices] UserManager<ApplicationUser> users,
        [FromBody] ChangeEmailRequest req)
    {
        var user = await users.GetUserAsync(User);
        if (user == null) return Unauthorized();

        user.Email = req.NewEmail;
        user.UserName = req.NewEmail;

        var result = await users.UpdateAsync(user);
        if (!result.Succeeded) return BadRequest(result.Errors);

        return NoContent();
    }
}