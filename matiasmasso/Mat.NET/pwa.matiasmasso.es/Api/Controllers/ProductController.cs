using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Thumbnail/{guid}.jpg")]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 2592000)] //1 mes = 60*60*24*30
        public IActionResult Thumbnail(Guid guid)
        {
            IActionResult retval;
            try
            {
                string mimeType = "image/jpeg";
                byte[]? value = Shared.Cache.GetImg(guid);
                if (value == null)
                {
                    value = ProductService.Thumbnail(guid);
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


    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {

        [HttpGet("CanonicalUrls")]
        public IActionResult CanonicalUrls()
        {
            IActionResult retval;
            try
            {
                var values = ProductsService.CanonicalUrls();
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


    }

}
