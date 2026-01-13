using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Api.Extensions;
using DTO;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookFraController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult GetValues(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = BookfraService.GetValue(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost()]
        public IActionResult Update([FromBody] BookfraModel model)
        {
            IActionResult retval;
            try
            {
                var value = BookfraService.Update(model);
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
                var value = BookfraService.Delete(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }
    }


    [Route("[controller]")]
    [ApiController]
    public class BookFrasController : ControllerBase
    {

        [HttpGet("{emp}/{year}")]
        public IActionResult GetValues(int emp, int year)
        {
            IActionResult retval;
            try
            {
                var value = BookfrasService.GetValues(emp, year);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("missingValues/{emp}/{year}")]
        public IActionResult MissingValues(int emp, int year)
        {
            IActionResult retval;
            try
            {
                var value = BookfrasService.MissingValues(emp, year);
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
