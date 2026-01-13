using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogPostController : ControllerBase
    {
         private readonly ILogger<WeatherForecastController> _logger;

        public BlogPostController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }


        [HttpGet("thumbnail/{guid}")]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 2592000)] //1 mes = 60*60*24*30
        public IActionResult Thumbnail(Guid guid)
        {
            IActionResult retval;
            try
            {
                string mimeType = "image/jpeg";
                byte[]? value = Shared.Cache.GetImg(guid);
                if(value == null)
                {
                    value = BlogPostService.Thumbnail(guid);
                    if(value != null)
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