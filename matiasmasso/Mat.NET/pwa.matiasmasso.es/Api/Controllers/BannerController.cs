using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BannerController : ControllerBase
    {
        private readonly ILogger<BannerController> _logger;
        public BannerController(ILogger<BannerController> logger)
        {
            _logger = logger;
        }



        [HttpGet("image/{guid}")]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 2592000)] //1 mes = 60*60*24*30
        public IActionResult ImgBanner600(Guid guid)
        {
            IActionResult retval;
            try
            {
                string mimeType = "image/jpeg";
                byte[]? value = Shared.Cache.GetImg(guid);
                if (value == null)
                {
                    value = BannerService.Image(guid);
                    if (value != null)
                        Shared.Cache.SetImg(value, guid);
                    else
                        value = new byte[] { };
                }
                retval = new FileContentResult(value, mimeType);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


    }
}