using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Shared;
using Api.Services;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LayoutController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                var lang = HttpHelper.Lang(Request);
                var value = LayoutService.Factory(user, lang);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }






        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO request)
        {
            IActionResult retval;
            try
            {
                var user = UserService.Login(request);
                var layout = LayoutService.Factory(user);
                retval = Ok(layout);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }
    }
}