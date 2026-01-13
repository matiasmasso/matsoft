using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Api.Services;
using Api.Extensions;


namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class RedsysController : ControllerBase
    {
        IOutputCacheStore cache;

        public RedsysController(IOutputCacheStore cache)
        {
            this.cache= cache;
        }


        [HttpPost("BookRequest")]
        public async Task<IActionResult> BookRequest([FromBody] DTO.Integracions.Redsys.TpvLog model)
        {
            try
            {
                var value = TpvLogService.BookRequest(model);
                await cache.Clear(OutputCacheExtensions.Tags.TpvLogs);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }


        [HttpGet("Log/{guid}")]
        public IActionResult Logs(Guid guid)
        {
            try
            {
                var value = TpvLogService.GetValue(guid);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }

        [HttpPost("log")]
        public async Task<IActionResult> UpdateLog([FromBody] DTO.Integracions.Redsys.TpvLog model)
        {
            try
            {
                var value = TpvLogService.Update(model);
                await cache.Clear(OutputCacheExtensions.Tags.TpvLogs);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }



        [HttpGet("log/delete/{guid}")]
        public async Task<IActionResult> DeleteLog(Guid guid)
        {
            try
            {
                var value = TpvLogService.Delete(guid);
                await cache.Clear(OutputCacheExtensions.Tags.TpvLogs);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }



        [HttpGet("Logs/{merchantCode?}")]
        [OutputCache(PolicyName = "TpvLogs")]

        public IActionResult Logs(string? merchantCode = null)
        {
            try
            {
                var values = TpvLogsService.GetValues(merchantCode);
                return Ok(values);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }
    }
}