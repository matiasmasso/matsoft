using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PndController : ControllerBase
    {

        [HttpPost()]
        public IActionResult Update([FromBody] PndModel model)
        {
            IActionResult retval;
            try
            {
                var value = PndService.Update(model);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

    }

    [ApiController]
    [Route("[controller]")]
    public class PndsController : ControllerBase
    {
        [HttpGet("pending/{target}")]
        public IActionResult GetValues(Guid target)
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                if (user == null) throw new Exception("missing user permissions");
                var values = PndsService.PendingValues(target);
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
