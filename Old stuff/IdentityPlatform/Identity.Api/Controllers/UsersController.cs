using Identity.Api.Data;
using Identity.Api.Domain.Users;
using Identity.Api.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IdentityDbContext _db;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UsersController(
        IdentityDbContext db,
        IPasswordHasher<User> passwordHasher)
    {
        _db = db;
        _passwordHasher = passwordHasher;
    }

    // GET /users
    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAll()
    {
        var users = await _db.Users
            .Include(u => u.AppEnrollments)
                .ThenInclude(e => e.App)
            .ToListAsync();

        return users.Select(u => u.ToDto()).ToList();
    }

    // GET /users/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> GetById(Guid id)
    {
        var user = await _db.Users
            .Include(u => u.AppEnrollments)
                .ThenInclude(e => e.App)
            .FirstOrDefaultAsync(u => u.Id == id.ToString());

        if (user is null)
            return NotFound();

        return user.ToDto();
    }

    // POST /users
    [HttpPost]
    public async Task<ActionResult<UserDto>> Create(CreateUserRequest request)
    {
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Email = request.Email,
            IsActive = true
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user.ToDto());
    }

    // PUT /users/{id}
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UserDto>> Update(Guid id, UpdateUserRequest request)
    {
        var user = await _db.Users.FindAsync(id);

        if (user is null)
            return NotFound();

        user.Email = request.Email;
        user.IsActive = request.IsActive;

        await _db.SaveChangesAsync();

        return user.ToDto();
    }

    // POST /users/{id}/reset-password
    [HttpPost("{id:guid}/reset-password")]
    public async Task<ActionResult> ResetPassword(Guid id, ResetPasswordRequest request)
    {
        var user = await _db.Users.FindAsync(id);

        if (user is null)
            return NotFound();

        user.PasswordHash = _passwordHasher.HashPassword(user, request.NewPassword);

        await _db.SaveChangesAsync();

        return NoContent();
    }

    // DELETE /users/{id}
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var user = await _db.Users.FindAsync(id);

        if (user is null)
            return NotFound();

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();

        return NoContent();
    }


}
