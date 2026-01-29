using Identity.Api.Data;
using Identity.Contracts.Dashboard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("dashboard")]
public sealed class DashboardController : ControllerBase
{
    private readonly AppDbContext _db;

    public DashboardController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<DashboardDto> Get()
    {
        return new DashboardDto
        {
            TotalApps = await _db.Apps.CountAsync(),
            TotalUsers = await _db.Users.CountAsync(),
            TotalRoles = await _db.AppRoles.CountAsync(),
            TotalSecrets = await _db.AppSecrets.CountAsync()
        };
    }
}