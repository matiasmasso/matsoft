using Api.Extensions;
using Api.Services;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LloguerController : ControllerBase
    {
        [HttpGet("{guid}")]
        public IActionResult GetValue(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = LloguerService.GetValue(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpPost]
        public IActionResult Update([FromBody] LloguerModel model)
        {
            IActionResult retval;
            try
            {
                var value = LloguerService.Update(model);
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
                LloguerService.Delete(guid);
                retval = Ok(true);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("Quotes/{guid}")]
        public IActionResult Quotes(Guid guid)
        {
            IActionResult retval;
            try
            {
                var values = LloguerService.GetQuotes(guid);
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost("Invoice/{guid}")]
        public IActionResult Invoice(Guid guid, [FromBody] InvoiceSentModel invoice)
        {
            IActionResult retval;
            try
            {
                var value = LloguerService.Invoice(guid, invoice);
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
    public class LloguersController : ControllerBase
    {
        [HttpGet("{emp}")]
        public IActionResult GetValues(EmpModel.EmpIds emp)
        {
            IActionResult retval;
            try
            {
                var value = LloguersService.GetValues(emp);
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
