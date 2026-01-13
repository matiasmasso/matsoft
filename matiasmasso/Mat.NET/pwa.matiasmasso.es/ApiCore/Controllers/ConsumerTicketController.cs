using Api.Extensions;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ConsumerTicketController : ControllerBase
    {
        [HttpGet("{guid}")]
        public IActionResult GetValue(Guid guid)
        {
            try
            { return Ok(ConsumerTicketService.GetValue(guid)); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }

        [HttpGet("FromOrderNum/{orderNum}")]
        public IActionResult FromOrderNum(string orderNum)
        {
            try
            { return Ok(ConsumerTicketService.FromOrderNum(orderNum)); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }

        [HttpGet("FromOrderNum/{marketplace}/{orderNum}")]
        public IActionResult FromMarketPlaceAndOrderNum(Guid marketplace, string orderNum)
        {
            try
            {
                var value = ConsumerTicketService.FromOrderNum(orderNum, marketplace);
                return Ok(value); 
            }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }

    }

    [ApiController]
    [Route("[controller]")]
    public class ConsumerTicketsController : ControllerBase
    {
        [HttpGet("FromUser/{marketPlace}/{user}")]
        public IActionResult FromUser(Guid marketPlace, Guid user)
        {
            IActionResult retval;
            try
            {
                var values = ConsumerTicketsService.FromUser(marketPlace, user);
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
