using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Api.Services.Integracions.Banca;
using DTO.Integracions.Banca;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class Norma43TemplateController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult GetValue(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = Norma43TemplateService.GetValue(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpPost()]
        public IActionResult Update([FromBody] Norma43.Template model)
        {
            IActionResult retval;
            try
            {
                var value = Norma43TemplateService.Update(model);
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
                var value = Norma43TemplateService.Delete(guid);
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
    public class Norma43TemplatesController : ControllerBase
    {
        [HttpGet()]
        public IActionResult GetValues()
        {
            try
            { return Ok(Norma43TemplatesService.GetValues()); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }
    }
}
