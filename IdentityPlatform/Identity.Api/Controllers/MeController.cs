using Identity.Api.Data;
using Identity.Api.Domain.Users;
using Identity.Api.Mappings;
using Identity.Api.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Controllers;

[ApiController]
[Route("me")]
[Authorize]
public class MeController : ControllerBase
{
    private readonly IdentityDbContext _db;

    public MeController(IdentityDbContext db)
    {
        _db = db;
    }

    // -----------------------------------------
    // GET /me
    // -----------------------------------------
    [HttpGet]
    public async Task<ActionResult<UserProfileDto>> Get()
    {
        var userId = User.FindFirst("sub")?.Value;

        if (userId is null)
            return Unauthorized();

        var user = await _db.Users
            .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

        if (user is null)
            return NotFound();

        return user.ToProfileDto();
    }

    // -----------------------------------------
    // PUT /me
    // -----------------------------------------
    [HttpPut]
    public async Task<ActionResult> Update(UserProfileUpdateDto dto)
    {
        var userId = User.FindFirst("sub")?.Value;

        if (userId is null)
            return Unauthorized();

        var user = await _db.Users
            .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

        if (user is null)
            return NotFound();

        user.Name = dto.Name;
        user.Preferences = dto.Preferences;

        await _db.SaveChangesAsync();

        return Ok();
    }

    // -----------------------------------------
    // POST /me/avatar
    // -----------------------------------------
    [HttpPost("avatar")]
    public async Task<ActionResult> UploadAvatar(IFormFile file)
    {
        var userId = User.FindFirst("sub")?.Value;

        if (userId is null)
            return Unauthorized();

        var user = await _db.Users
            .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

        if (user is null)
            return NotFound();

        if (file is null || file.Length == 0)
            return BadRequest("No file uploaded.");

        // Store avatar in wwwroot/avatars/{userId}.png
        var folder = Path.Combine("wwwroot", "avatars");
        Directory.CreateDirectory(folder);

        var filePath = Path.Combine(folder, $"{user.Id}.png");

        using (var stream = System.IO.File.Create(filePath))
        {
            await file.CopyToAsync(stream);
        }

        user.AvatarUrl = $"/avatars/{user.Id}.png";

        await _db.SaveChangesAsync();

        return Ok(new { AvatarUrl = user.AvatarUrl });
    }
}