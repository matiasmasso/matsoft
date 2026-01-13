using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CustomerInvoiceTemplateController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult GetValue(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = CustomerInvoiceTemplateService.GetValue(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpPost()]
        public IActionResult Update([FromBody] CustomerInvoiceModel.Template model)
        {
            IActionResult retval;
            try
            {
                var value = CustomerInvoiceTemplateService.Update(model);
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
                var value = CustomerInvoiceTemplateService.Delete(guid);
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
    public class CustomerInvoiceTemplatesController : ControllerBase
    {
        [HttpGet()]
        public IActionResult GetValues()
        {
            try
            { return Ok(CustomerInvoiceTemplatesService.GetValues()); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }
    }
}