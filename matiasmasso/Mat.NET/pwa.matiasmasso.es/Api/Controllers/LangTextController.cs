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
        /// <summary>
        /// Returns all string resources except those related to outdated products
        /// </summary>
        [HttpGet("active")]
        public IActionResult Active()
        {
            IActionResult retval;
            try
            {
                var values = LangTextsService.Active(); //.Take(10); //------------fake take
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
