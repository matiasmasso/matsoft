using Identity.Services;
using Identity.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _users;
    private readonly IRoleService _roles;
    private readonly IAppService _apps;

    public UsersController(
        IUserService users,
        IRoleService roles,
        IAppService apps)
    {
        _users = users;
        _roles = roles;
        _apps = apps;
    }

    // ------------------------------------------------------------
    // GET: api/users
    // ------------------------------------------------------------
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _users.GetAll();
        return Ok(list.Select(u => new
        {
            u.UserId,
            u.Email,
            u.CreatedAt
        }));
    }

    // ------------------------------------------------------------
    // GET: api/users/{id}
    // ------------------------------------------------------------
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var user = await _users.GetById(id);
        if (user == null)
            return NotFound();

        return Ok(new
        {
            user.UserId,
            user.Email,
            user.CreatedAt,
            user.UpdatedAt
        });
    }

    // ------------------------------------------------------------
    // PUT: api/users/{id}
    // ------------------------------------------------------------
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateUserRequest request)
    {
        var user = await _users.GetById(id);
        if (user == null)
            return NotFound();

        user.Email = request.Email;
        user.UpdatedAt = DateTime.UtcNow;

        await _users.Update(user);

        return Ok(new { message = "User updated" });
    }

    // ------------------------------------------------------------
    // DELETE: api/users/{id}
    // ------------------------------------------------------------
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var user = await _users.GetById(id);
        if (user == null)
            return NotFound();

        await _users.Delete(id);
        return Ok(new { message = "User deleted" });
    }

    // ------------------------------------------------------------
    // GET: api/users/{id}/roles
    // ------------------------------------------------------------
    [HttpGet("{id:guid}/roles")]
    public async Task<IActionResult> GetRoles(Guid id)
    {
        var roles = await _users.GetRoles(id);
        return Ok(roles);
    }

    // ------------------------------------------------------------
    // PUT: api/users/{id}/roles
    // ------------------------------------------------------------
    [HttpPut("{id:guid}/roles")]
    public async Task<IActionResult> UpdateRoles(Guid id, List<string> roles)
    {
        var user = await _users.GetById(id);
        if (user == null)
            return NotFound();

        await _roles.AssignRolesToUser(id, roles);
        return Ok(new { message = "Roles updated" });
    }

    // ------------------------------------------------------------
    // GET: api/users/{id}/apps
    // ------------------------------------------------------------
    [HttpGet("{id:guid}/apps")]
    public async Task<IActionResult> GetApps(Guid id)
    {
        var apps = await _users.GetApps(id);
        return Ok(apps);
    }

    // ------------------------------------------------------------
    // PUT: api/users/{id}/apps
    // ------------------------------------------------------------
    [HttpPut("{id:guid}/apps")]
    public async Task<IActionResult> UpdateApps(Guid id, List<string> apps)
    {
        var user = await _users.GetById(id);
        if (user == null)
            return NotFound();

        await _apps.AssignAppsToUser(id, apps);
        return Ok(new { message = "Apps updated" });
    }
}
