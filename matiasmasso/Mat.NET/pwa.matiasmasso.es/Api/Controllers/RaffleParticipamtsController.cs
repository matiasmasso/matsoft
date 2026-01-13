using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Newtonsoft.Json;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class RaffleParticipantController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult GetValue(Guid guid)
        {
            IActionResult retval;
            try
            {
                var values = RaffleParticipantService.GetValue(guid);
                retval = Ok(values);
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
    public class RaffleParticipantsController : ControllerBase
    {

        [HttpGet("fromUser/{guid}")]
        public IActionResult FromUser(Guid guid)
        {
            IActionResult retval;
            try
            {
                var values = RaffleParticipantsService.FromUser(guid);
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
