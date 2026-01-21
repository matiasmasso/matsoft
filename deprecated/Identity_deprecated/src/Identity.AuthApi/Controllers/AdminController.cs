using Identity.Domain.Entities;
using Identity.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Identity.AuthApi.Controllers;

[ApiController]
[Route("admin")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _users;

    public AdminController(UserManager<ApplicationUser> users)
    {
        _users = users;
    }

    [HttpGet("users")]
    public IActionResult GetUsers()
    {
        var users = _users.Users
            .Select(u => new
            {
                u.Id,
                u.Email,
                u.UserName,
                u.EmailConfirmed
            })
            .ToList();

        return Ok(users);
    }

    [HttpGet("users/{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await _users.FindByIdAsync(id);
        if (user == null) return NotFound();

        return Ok(new
        {
            user.Id,
            user.Email,
            user.UserName,
            user.EmailConfirmed
        });
    }

    public record CreateUserRequest(string Email, string Password, string? FullName);

    [HttpPost("users")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest req)
    {
        var user = new ApplicationUser
        {
            UserName = req.Email,
            Email = req.Email,
            FullName = req.FullName
        };

        var result = await _users.CreateAsync(user, req.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, new
        {
            user.Id,
            user.Email
        });
    }

    public record UpdateUserRequest(string? Email, string? FullName);

    [HttpPut("users/{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserRequest req)
    {
        var user = await _users.FindByIdAsync(id);
        if (user == null) return NotFound();

        if (!string.IsNullOrWhiteSpace(req.Email))
        {
            user.Email = req.Email;
            user.UserName = req.Email;
        }

        if (!string.IsNullOrWhiteSpace(req.FullName))
        {
            user.FullName = req.FullName;
        }

        var result = await _users.UpdateAsync(user);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return NoContent();
    }

    [HttpDelete("users/{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _users.FindByIdAsync(id);
        if (user == null) return NotFound();

        var result = await _users.DeleteAsync(user);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return NoContent();
    }

    [HttpPost("impersonate/{userId}")]
    public async Task<IActionResult> Impersonate(
    string userId,
    [FromServices] UserManager<ApplicationUser> users,
    [FromServices] ITokenService tokens)
    {
        var target = await users.FindByIdAsync(userId);
        if (target == null) return NotFound();

        var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "unknown";

        var token = await tokens.CreateImpersonationTokenAsync(target, adminId);

        return Ok(new { accessToken = token });
    }
}