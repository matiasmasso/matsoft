using Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roles;

        public RolesController(IRoleService roles)
        {
            _roles = roles;
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignRole(Guid userId, Guid appId, Guid roleId)
        {
            await _roles.AssignRole(userId, appId, roleId);
            return Ok("Role assigned");
        }
    }

}
