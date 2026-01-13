using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CacheController : ControllerBase
    {

        private IOutputCacheStore _cache;
        public CacheController( IOutputCacheStore cache)
        {
            _cache = cache;
        }

        [HttpPost]
        public IActionResult Load([FromBody] CacheDTO payload)
        {
            IActionResult? retval;
            try
            {

                var value = CacheService.ClientCache(payload);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval!;
        }

    }
}
