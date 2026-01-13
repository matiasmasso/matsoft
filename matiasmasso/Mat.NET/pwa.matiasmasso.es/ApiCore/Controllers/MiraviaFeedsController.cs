using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MiraviaFeedsController : ControllerBase
    {
        [HttpGet("ProductListingUpdate_basic_4000087472422_ES.csv")]
        public async Task<IActionResult> ProductListing()
        {
            IActionResult retval;
            try
            {
                var value = Services.Integracions.Miravia.ProductListing.GetValue();
                var csv = await value.ToCsv();
                retval = Ok(csv);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("UpdatePriceAndStock_4000087472422_ES.csv")]
        public async Task<IActionResult> ProductPriceStockStatus()
        {
            IActionResult retval;
            try
            {
                var value = Services.Integracions.Miravia.ProductPriceStockStatus.GetValue();
                var csv = await value.ToCsv();
                retval = Ok(csv);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }
    }
}
