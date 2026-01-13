using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [OutputCache(PolicyName = "Products")]
    public class ProductController: ControllerBase
    {
        IOutputCacheStore cache;

        public ProductController(IOutputCacheStore cache)
        {
            this.cache = cache;
        }

        [HttpGet("Thumbnail/{guid}.jpg")]
        public IActionResult Thumbnail(Guid guid)
        {
            IActionResult retval;
            try
            {
                string mimeType = "image/jpeg";
                byte[]? value = ProductService.Thumbnail(guid);
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
    [OutputCache(PolicyName = "Products")]
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
