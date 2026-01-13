using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ZonaController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult Fetch(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = ZonaService.Find(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpPost]
        public IActionResult Update([FromBody] ZonaModel model)
        {
            IActionResult retval;
            try
            {
                var value = ZonaService.Update(model);
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
                var value = ZonaService.Delete(guid);
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
    public class ZonasController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetValues()
        {
            IActionResult retval;
            try
            {
                var value = ZonasService.GetValues();
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
