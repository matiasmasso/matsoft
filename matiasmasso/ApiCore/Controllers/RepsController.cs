using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class RepController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult Find(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = RepService.Find(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpPost]
        public IActionResult Update([FromBody] RepModel model)
        {
            IActionResult retval;
            try
            {
                var value = RepService.Update(model);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("delete/{guid}")]
        public IActionResult Delete(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = RepService.Delete(guid);
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
    public class RepsController : ControllerBase
    {

        [HttpGet("{emp}")]
        public IActionResult Fetch(int emp)
        {
            IActionResult retval;
            try
            {
                var values = RepsService.All(emp);
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
