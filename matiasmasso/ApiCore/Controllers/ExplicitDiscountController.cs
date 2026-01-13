using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ExplicitDiscountController : ControllerBase
    {

        [HttpGet("{customer}/{product}")]
        public IActionResult GetValue(Guid customer, Guid product)
        {
            IActionResult retval;
            try
            {
                var value = ExplicitDiscountService.GetValue(customer, product);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpPost()]
        public IActionResult Update([FromBody] ExplicitDiscountModel model)
        {
            IActionResult retval;
            try
            {
                var value = ExplicitDiscountService.Update(model);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpGet("delete/{customer}/{product}")]
        public IActionResult Delete(Guid customer, Guid product)
        {
            IActionResult retval;
            try
            {
                var value = ExplicitDiscountService.Delete(customer, product);
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
    public class ExplicitDiscountsController : ControllerBase
    {
        [HttpGet()]
        public IActionResult GetValues()
        {
            try
            { return Ok(ExplicitDiscountsService.GetValues()); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }
    }
}
