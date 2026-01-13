using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MgzInventoryController : ControllerBase
    {

        [HttpGet("{mgz}")]
        public IActionResult Get(Guid mgz)
        {
            IActionResult retval;
            try
            {
                var value = MgzInventoryService.Factory(mgz);
                var months = value.Months(2022);
                var endJune = value.Days(2022, 6);

                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

    }


}
