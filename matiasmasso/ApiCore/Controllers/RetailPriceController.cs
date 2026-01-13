using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class RetailPricesController : ControllerBase
    {
        [HttpGet("Current/{emp}")]
        public IActionResult Current(EmpModel.EmpIds emp)
        {
            try
            { return Ok(PriceListCustomerService.Current(emp)); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }

    }
}
