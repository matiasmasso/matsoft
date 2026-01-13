using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LangTextController : ControllerBase
    {

        [HttpPost("langItem")]
        public IActionResult Update([FromBody] LangTextModel.LangItem model)
        {
            IActionResult retval;
            try
            {
                var value = LangTextService.UpdateItem(model);
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
    public class LangTextsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetValues()
        {
            IActionResult retval;
            try
            {
                var values = LangTextsService.GetValues();
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
