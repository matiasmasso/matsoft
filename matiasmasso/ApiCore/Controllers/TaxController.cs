using Microsoft.AspNetCore.Mvc;
using Api.Services;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;
using DTO;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [OutputCache(PolicyName = "Taxes")]
    public class TaxController : ControllerBase
    {
        IOutputCacheStore cache;

        public TaxController(IOutputCacheStore cache)
        {
            this.cache = cache;
        }


        [HttpGet("{guid}")]
        public IActionResult GetValue(Guid guid)
        {
            try
            { return Ok(TaxService.GetValue(guid)); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] TaxModel value)
        {
            try
            {
                TaxService.Update(value);
                await cache.Clear(OutputCacheExtensions.Tags.Taxes);
                return Ok(true);
            }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }


        [HttpGet("delete/{guid}")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            try
            {
                TaxService.Delete(guid);
                await cache.Clear(OutputCacheExtensions.Tags.Taxes);
                return Ok(true);
            }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }

    }




    [ApiController]
    [Route("[controller]")]
    [OutputCache(PolicyName = "Taxes")]
    public class TaxesController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetValues(Guid guid)
        {
            try
            { return Ok( TaxesService.GetValues());}
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }

    }
}

