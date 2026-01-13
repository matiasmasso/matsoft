using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CacheController : ControllerBase
    {

        [HttpPost]
        public IActionResult Load([FromBody] CacheDTO payload)
        {
            IActionResult? retval;
            try
            {

                var value = CacheService.ClientCache(payload);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval!;
        }

    }
}
