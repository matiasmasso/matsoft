using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EdiversaInvRpts : ControllerBase
    {

        [HttpPost("{holding}")]
        public IActionResult LastStocksFromHolding(Guid holding, [FromBody] DateTime? fch )
        {
            IActionResult retval;
            try
            {
                //var user = HttpHelper.User(Request);
                //if (user == null) throw new Exception("User unknown");
                var values = EdiversaInvRptsService.GetLastValues(holding, fch);
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
