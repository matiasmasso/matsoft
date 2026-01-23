using Identity.Api.Application.Apps;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/apps/{appId:guid}/roles")]
public class RolesController : ControllerBase
{
    private readonly RoleService _roles;

    public RolesController(RoleService roles)
    {
        _roles = roles;
    }

    public record CreateRoleRequest(string Name);

    [HttpPost]
    public async Task<IActionResult> Create(Guid appId, CreateRoleRequest request)
    {
        var role = await _roles.CreateRoleAsync(appId, request.Name);
        return Ok(role);
    }

    [HttpGet]
    public async Task<IActionResult> List(Guid appId)
    {
        var roles = await _roles.ListRolesAsync(appId);
        return Ok(roles);
    }
}