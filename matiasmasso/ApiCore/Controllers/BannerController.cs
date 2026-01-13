using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [OutputCache(PolicyName = "Banners")]
    public class BannerController : ControllerBase
    {
        IOutputCacheStore cache;

        public BannerController(IOutputCacheStore cache)
        {
            this.cache = cache;
        }



        [HttpGet("image/{guid}")]
        public IActionResult ImgBanner600(Guid guid)
        {
            IActionResult retval;
            try
            {
                string mimeType = "image/jpeg";
                byte[]? value = BannerService.Image(guid);
                retval = new FileContentResult(value, mimeType);
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
    [OutputCache(PolicyName = "Banners")]
    public class BannersController : ControllerBase
    {
        IOutputCacheStore cache;

        public BannersController(IOutputCacheStore cache)
        {
            this.cache = cache;
        }



        [HttpGet]
        public IActionResult GetValues()
        {
            try { return Ok(BannersService.GetValues()); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }


    }
}