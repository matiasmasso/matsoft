using Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppsController : ControllerBase
{
    private readonly IAppService _apps;

    public AppsController(IAppService apps)
    {
        _apps = apps;
    }

    // GET: api/apps
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var apps = await _apps.GetAll();
        return Ok(apps);
    }
}
