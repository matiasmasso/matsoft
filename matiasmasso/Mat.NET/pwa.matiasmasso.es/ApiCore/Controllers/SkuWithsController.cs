using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [OutputCache(PolicyName = "SkuWiths")]
    public class SkuWithsController : ControllerBase
    {
        IOutputCacheStore cache;

        public SkuWithsController(IOutputCacheStore cache)
        {
            this.cache = cache;
        }



        [HttpGet]
        public IActionResult GetValues()
        {
            try {return Ok(SkuWithsService.GetValues());}
            catch (Exception ex) {return BadRequest(ex.ProblemDetails()); }
        }


    }
}
