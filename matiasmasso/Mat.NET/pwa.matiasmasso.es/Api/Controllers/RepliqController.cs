using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RepliqController: ControllerBase
    {
    }

    [ApiController]
    [Route("[controller]")]
    public class RepliqsController: ControllerBase
    {
        [HttpGet("{rep}")]
        public IActionResult Fetch(Guid rep)
        {
            IActionResult retval;
            try
            {
                var values = RepliqsService.All(rep);
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
