using Identity.Api.Application.Apps;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/apps/{appId:guid}/users")]
public class EnrollmentsController : ControllerBase
{
    private readonly EnrollmentService _enrollments;

    public EnrollmentsController(EnrollmentService enrollments)
    {
        _enrollments = enrollments;
    }

    public record AssignRoleRequest(Guid RoleId);

    // ------------------------------------------------------------
    // Enroll user into app
    // ------------------------------------------------------------
    [HttpPost("{userId:guid}/enroll")]
    public async Task<IActionResult> Enroll(Guid appId, Guid userId)
    {
        var enrollment = await _enrollments.EnrollUserAsync(userId, appId);
        return Ok(enrollment);
    }

    // ------------------------------------------------------------
    // Assign role to user
    // ------------------------------------------------------------
    [HttpPost("{userId:guid}/roles")]
    public async Task<IActionResult> AssignRole(Guid appId, Guid userId, AssignRoleRequest request)
    {
        var enrollment = await _enrollments.EnrollUserAsync(userId, appId);
        var role = await _enrollments.AssignRoleAsync(enrollment.Id, request.RoleId);
        return Ok(role);
    }

    // ------------------------------------------------------------
    // Remove role from user
    // ------------------------------------------------------------
    [HttpDelete("{userId:guid}/roles/{roleId:guid}")]
    public async Task<IActionResult> RemoveRole(Guid appId, Guid userId, Guid roleId)
    {
        var enrollment = await _enrollments.EnrollUserAsync(userId, appId);
        await _enrollments.RemoveRoleAsync(enrollment.Id, roleId);
        return Ok();
    }

    // ------------------------------------------------------------
    // List users in app
    // ------------------------------------------------------------
    [HttpGet]
    public async Task<IActionResult> ListUsers(Guid appId)
    {
        var users = await _enrollments.ListUsersInAppAsync(appId);
        return Ok(users);
    }
}