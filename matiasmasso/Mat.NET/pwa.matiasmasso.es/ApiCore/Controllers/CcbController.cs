using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CcbController : ControllerBase
    {


        [HttpPost()]
        public IActionResult Update([FromBody] CcaModel.Item model)
        {
            IActionResult retval;
            try
            {
                var value = CcbService.Update(model);
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