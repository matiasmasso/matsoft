using Identity.Api.Infrastructure.Errors;
using Identity.Data;
using Identity.Domain.Entities;
using Identity.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public UsersController(
        ApplicationDbContext db,
        UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    // ------------------------------------------------------------
    // GET /users
    // List all users
    // ------------------------------------------------------------
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            var users = await _db.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    IsActive = u.IsActive,
                    CreatedAt = u.CreatedAt,

                    Applications = _db.UserApplications
                        .Where(ua => ua.UserId == u.Id)
                        .Select(ua => new ApplicationDto
                        {
                            ApplicationId = ua.ApplicationId,
                            Name = ua.Application.Name,
                            IsActive = ua.IsActive
                        })
                        .ToList(),

                    Roles = _db.UserRoles
                        .Where(ur => ur.UserId == u.Id)
                        .Select(ur => new UserRoleDto
                        {
                            RoleId = ur.RoleId,
                            Name = ur.Role.Name,
                            ApplicationId = ur.ApplicationId
                        })
                        .ToList()
                })
                .ToListAsync();

            return Ok(users);
        }
        catch (Exception ex)
        {
            return BadRequest(ErrorResult.FromException(ex));
        }
    }



    // ------------------------------------------------------------
    // GET /users/{userId}
    // Get user details + apps + roles
    // ------------------------------------------------------------
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        try
        {
            var user = await _db.Users
                .Where(u => u.Id == userId)
                .Select(u => new
                {
                    u.Id,
                    u.UserName,
                    u.Email,
                    u.IsActive,
                    Applications = _db.UserApplications
                        .Where(ua => ua.UserId == userId)
                        .Select(ua => new
                        {
                            ua.ApplicationId,
                            ua.Application.Name,
                            ua.IsActive
                        })
                        .ToList(),

                    Roles = _db.UserRoles
                        .Where(ur => ur.UserId == userId)
                        .Include(ur => ur.Role)
                        .Select(ur => new
                        {
                            ur.RoleId,
                            ur.Role.Name,
                            ur.ApplicationId
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ErrorResult.FromException(ex));
        }
    }

    // ------------------------------------------------------------
    // POST /users/create
    // Create a new user
    // ------------------------------------------------------------
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        try
        {
            var existing = await _userManager.FindByNameAsync(request.UserName);
            if (existing != null)
                return BadRequest("Username already exists");

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = request.UserName,
                Email = request.Email,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return ErrorResult.FromIdentityErrors(result.Errors);

            return Ok(new { Message = "User created", user.Id });

        }
        catch (Exception ex)
        {
            return BadRequest(ErrorResult.FromException(ex));
        }
    }

    // ------------------------------------------------------------
    // POST /users/activate
    // Activate or deactivate a user
    // ------------------------------------------------------------
    [HttpPost("activate")]
    public async Task<IActionResult> ActivateUser([FromBody] ActivateUserRequest request)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return NotFound("User not found");

            user.IsActive = request.IsActive;
            await _userManager.UpdateAsync(user);

            return Ok("User updated");

        }
        catch (Exception ex)
        {
            return BadRequest(ErrorResult.FromException(ex));
        }
    }

    // ------------------------------------------------------------
    // POST /users/reset-password
    // Admin resets a user's password
    // ------------------------------------------------------------
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return NotFound("User not found");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Password reset");

        }
        catch (Exception ex)
        {
            return BadRequest(ErrorResult.FromException(ex));
        }
    }

    // ------------------------------------------------------------
    // POST /users/enroll
    // Enroll user into an application
    // ------------------------------------------------------------
    [HttpPost("enroll")]
    public async Task<IActionResult> EnrollUser([FromBody] EnrollUserRequest request)
    {
        try
        {
            var exists = await _db.UserApplications.AnyAsync(ua =>
                ua.UserId == request.UserId &&
                ua.ApplicationId == request.ApplicationId);

            if (exists)
                return BadRequest("User already enrolled");

            var enrollment = new UserApplication
            {
                UserId = request.UserId,
                ApplicationId = request.ApplicationId,
                IsActive = true
            };

            _db.UserApplications.Add(enrollment);
            await _db.SaveChangesAsync();

            return Ok("User enrolled");

        }
        catch (Exception ex)
        {
            return BadRequest(ErrorResult.FromException(ex));
        }
    }

    // ------------------------------------------------------------
    // POST /users/unenroll
    // Remove user from an application
    // ------------------------------------------------------------
    [HttpPost("unenroll")]
    public async Task<IActionResult> UnenrollUser([FromBody] EnrollUserRequest request)
    {
        try
        {
            var enrollment = await _db.UserApplications.FirstOrDefaultAsync(ua =>
                ua.UserId == request.UserId &&
                ua.ApplicationId == request.ApplicationId);

            if (enrollment == null)
                return NotFound("User is not enrolled");

            _db.UserApplications.Remove(enrollment);
            await _db.SaveChangesAsync();

            return Ok("User unenrolled");

        }
        catch (Exception ex)
        {
            return BadRequest(ErrorResult.FromException(ex));
        }
    }

    // ------------------------------------------------------------
    // PUT /users/{id}
    // Update user basic info
    // ------------------------------------------------------------
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest request)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound("User not found");

            user.UserName = request.UserName;
            user.Email = request.Email;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { Message = "User updated", id });

        }
        catch (Exception ex)
        {
            return BadRequest(ErrorResult.FromException(ex));
        }
    }

    // ------------------------------------------------------------
    // DELETE /users/{id}
    // Delete a user
    // ------------------------------------------------------------
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound("User not found");

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { Message = "User deleted", id });

        }
        catch (Exception ex)
        {
            return BadRequest(ErrorResult.FromException(ex));
        }
    }

    [HttpGet("{userId}/applications")]
    public async Task<IActionResult> GetUserApplications(Guid userId)
    {
        try
        {
            var apps = await _db.UserApplications
                .Where(x => x.UserId == userId)
                .Select(x => x.ApplicationId)
                .ToListAsync();

            return Ok(apps);
        }
        catch (Exception ex)
        {
            return BadRequest(ErrorResult.FromException(ex));
        }

    }


}
