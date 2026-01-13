using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PremiumLineController : ControllerBase
    {
    }


    [ApiController]
    [Route("[controller]")]
    public class PremiumLinesController : ControllerBase
    {

        [HttpGet("{product}")]
        public IActionResult FromProduct(Guid product)
        {
            IActionResult retval;
            try
            {
                var oProduct = new ProductModel(product);
                var values = PremiumLinesService.All(oProduct);
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

