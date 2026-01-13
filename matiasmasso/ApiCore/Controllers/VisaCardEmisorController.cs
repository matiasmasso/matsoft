using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class VisaCardEmisorController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult GetValue(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = VisaCardEmisorService.GetValue(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpPost()]
        public IActionResult Update([FromBody] GuidNom model)
        {
            IActionResult retval;
            try
            {
                var value = VisaCardEmisorService.Update(model);
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
                var value = VisaCardEmisorService.Delete(guid);
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
    public class VisaCardEmisorsController : ControllerBase
    {
        [HttpGet()]
        public IActionResult GetValues()
        {
            try
            { return Ok(VisaCardEmisorsService.GetValues()); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }
    }
}
