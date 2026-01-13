using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatController : ControllerBase
    {


        [HttpPost]
        public IActionResult Post([FromBody] StatDTO.StatRequest request)
        {
            IActionResult retval;
            try
            {
                var value = StatService.Load(request);
                return Ok(value); 

            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;

        }

    }


}
