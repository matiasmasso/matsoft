using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PriceListSupplierController : ControllerBase
    {
        [HttpGet("current")]
        public IActionResult Current()
        {
            try
            { 
                var values = PriceListSupplierService.Current();
                return Ok(values);
            }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }
    }
}
