using IdentityPlatform.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityPlatform.Apps.Controllers;

[ApiController]
[Route("admin/dashboard")]
[Authorize(Roles = "Admin")]
public class AdminDashboardController : ControllerBase
{
    private readonly AppDbContext _db;

    public AdminDashboardController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await _db.Users.CountAsync();
        var apps = await _db.Apps.CountAsync();
        var enrollments = await _db.Enrollments.CountAsync();
        var activeEnrollments = await _db.Enrollments.CountAsync(e => e.IsActive);

        return Ok(new
        {
            Users = users,
            Apps = apps,
            Enrollments = enrollments,
            ActiveEnrollments = activeEnrollments
        });
    }
}