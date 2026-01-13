using Api.Extensions;
using Api.Services;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StringLocalizerController : ControllerBase
    {
        IOutputCacheStore cache;

        public StringLocalizerController(IOutputCacheStore cache)
        {
            this.cache = cache;
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] StringLocalizerModel value)
        {
            IActionResult retval;
            try
            {
                var values = new List<StringLocalizerModel>() { value };
                StringsLocalizerService.Update(values);
                await cache.EvictByTagAsync("StringsLocalizer", default);
                retval = Ok(true);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] string stringKey)
        {
            IActionResult retval;
            try
            {
                StringLocalizerService.Delete(stringKey);
                await cache.EvictByTagAsync("StringsLocalizer", default);
                retval = Ok();
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

    }



    [ApiController]
    [Route("[controller]")]

    public class StringsLocalizerController : ControllerBase
    {
        IOutputCacheStore cache;

        public StringsLocalizerController(IOutputCacheStore cache)
        {
            this.cache = cache;
        }

        [HttpGet]
        [OutputCache(PolicyName = "StringsLocalizer")]
        public IActionResult GetValues()
        {
            IActionResult retval;
            try
            {
                var values = StringsLocalizerService.GetValues();
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpPost]
        public IActionResult Update([FromBody] List<StringLocalizerModel> values)
        {
            IActionResult retval;
            try
            {
                StringsLocalizerService.Update(values);
                retval = Ok();
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

    }
}
