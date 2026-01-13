using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TpvLogController:ControllerBase
    {
        IOutputCacheStore cache;

        public TpvLogController(IOutputCacheStore cache)
        {
            this.cache = cache;
        }



        [HttpGet("{guid}")]
        public IActionResult GetValue(Guid guid)
        {
            try
            {
                var value = TpvLogService.GetValue(guid);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }



        [HttpPost()]
        public async Task<IActionResult> Update([FromBody] DTO.Integracions.Redsys.TpvLog model)
        {
            try
            {
                var value = TpvLogService.Update(model);
                await cache.Clear(OutputCacheExtensions.Tags.TpvLogs);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }



        [HttpGet("delete/{guid}")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            try
            {
                var value = TpvLogService.Delete(guid);
                await cache.Clear(OutputCacheExtensions.Tags.TpvLogs);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }

    }
}
