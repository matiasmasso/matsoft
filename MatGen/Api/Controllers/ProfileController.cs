using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly UserService _userService;

        public ProfileController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        public ActionResult<UserModel?> GetProfile()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
            else
            {
                var profile = _userService.Find(new Guid(userId));
                return profile;
            }

        }

        [HttpPut("me")]
        public IActionResult UpdateProfile(UserModel profile)
        {
            if(_userService.Update(profile))
            return Ok();
            else
                return BadRequest("Could not update profile");
        }

    }
}
