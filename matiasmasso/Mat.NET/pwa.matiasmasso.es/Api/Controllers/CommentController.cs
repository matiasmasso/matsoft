using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Newtonsoft.Json;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {

        [HttpGet("fromUser/{user}")]
        public IActionResult FromUser(Guid user)
        {
            IActionResult retval;
            try
            {
                var values = CommentsServices.FromUser(user);
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

    }
}
