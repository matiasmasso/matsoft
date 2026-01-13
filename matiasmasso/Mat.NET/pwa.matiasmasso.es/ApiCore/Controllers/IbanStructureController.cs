using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class IbanStructureController : ControllerBase
    {

        [HttpGet("{countryISO}")]
        public IActionResult GetValue(string countryISO)
        {
            IActionResult retval;
            try
            {
                var value = IbanStructureService.GetValue(countryISO);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpPost()]
        public IActionResult Update([FromBody] IbanStructureModel model)
        {
            IActionResult retval;
            try
            {
                var value = IbanStructureService.Update(model);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpGet("delete/{countryISO}")]
        public IActionResult Delete(string countryISO)
        {
            IActionResult retval;
            try
            {
                var value = IbanStructureService.Delete(countryISO);
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
    public class IbanStructuresController : ControllerBase
    {
        [HttpGet()]
        public IActionResult GetValues()
        {
            try
            { return Ok(IbanStructuresService.GetValues()); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }
    }
}