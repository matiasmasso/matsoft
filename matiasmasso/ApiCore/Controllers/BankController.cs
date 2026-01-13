using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankController : ControllerBase
    {

        [HttpGet("logo/{guid}")]
        public IActionResult Logo(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = BankService.Logo(guid);
                retval = new FileContentResult(value?.Data, value?.ContentType());
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost()]
        public IActionResult Update([FromBody] BankModel model)
        {
            IActionResult retval;
            try
            {
                var value =BankService.Update(model);
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
                var value = BankService.Delete(guid);
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
    public class BanksController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetValues()
        {
            IActionResult retval;
            try
            {
                var values = BanksService.GetValues();
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
