using Identity.Api.Data;
using Identity.Api.Entities;
using Identity.Contracts.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("users")]
public sealed class UsersController : ControllerBase
{
    private readonly AppDbContext _db;

    public UsersController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IEnumerable<UserDto>> GetAll()
    {
        return await _db.Users
            .Select(u => new UserDto
            {
                Id = u.Id,
                Email = u.Email,
                Name = u.DisplayName,
                Enabled = u.Enabled
            })
            .ToListAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> Get(Guid id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user is null) return NotFound();

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.DisplayName,
            Enabled = user.Enabled
        };
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Create(CreateUserRequest dto)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = dto.Email,
            DisplayName = dto.Name,
            Enabled = dto.Enabled
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.DisplayName,
            Enabled = user.Enabled
        };
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateUserRequest dto)
    {
        if (id != dto.Id) return BadRequest();

        var user = await _db.Users.FindAsync(id);
        if (user is null) return NotFound();

        user.Email = dto.Email;
        user.DisplayName = dto.Name;
        user.Enabled = dto.Enabled;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user is null) return NotFound();

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("{id:guid}/toggle-enabled")]
    public async Task<IActionResult> ToggleEnabled(Guid id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user is null)
            return NotFound();

        user.Enabled = !user.Enabled;

        await _db.SaveChangesAsync();
        return NoContent();
    }


    [HttpGet("{userId:guid}/apps")]
    public async Task<ActionResult<List<UserAppDto>>> GetAppsForUser(Guid userId)
    {
        return await _db.UserApps
    .Where(x => x.UserId == userId)
    .Select(x => new UserAppDto
    {
        Id = x.AppId,
        Name = x.App.Name
        //,Roles = x.Roles.Select(r => r.RoleName).ToList()
    })
    .ToListAsync();

    }
}