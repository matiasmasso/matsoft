using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PgcCtaExtracteController : ControllerBase
    {
        [HttpPost]
        public  IActionResult Fetch([FromBody] PgcCtaExtracteDTO src)
        {
            IActionResult retval;
            try
            {
                var value = PgcCtaExtracteService.Extracte(src);
                retval= Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

    }
}
