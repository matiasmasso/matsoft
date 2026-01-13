using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[OutputCache(PolicyName = "StoreLocator")]
    public class StoreLocatorController : ControllerBase
    {
        IOutputCacheStore cache;

        public StoreLocatorController(IOutputCacheStore cache)
        {
            this.cache = cache;
        }

        [HttpPost("{lang}")]
        public IActionResult GetValue(string lang, [FromBody] ProductModel product)
        {
            try
            {
                var values = StoreLocatorService.Factory(product, new LangDTO(lang));
                return Ok(values);
            }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }

        [HttpGet("raw")]
        public IActionResult GetRawValues()
        {
            try
            {
                var values = StoreLocatorRawService.GetValues();
                return Ok(values);
            }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }

    }
}
