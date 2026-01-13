using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [OutputCache(PolicyName = "ProductDepts")]
    public class ProductDeptController : ControllerBase
    {
        IOutputCacheStore cache;

        public ProductDeptController(IOutputCacheStore cache)
        {
            this.cache = cache;
        }

        [HttpGet("{guid}")]
        public IActionResult Find(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = ProductDeptService.Find(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpGet("Thumbnail/{guid}.jpg")]
        public IActionResult Thumbnail(Guid guid)
        {
            IActionResult retval;
            try
            {
                string mimeType = "image/jpeg";
                byte[]? value = ProductDeptService.Thumbnail(guid);
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
    [OutputCache(PolicyName = "ProductDepts")]
    public class ProductDeptsController : ControllerBase
    {
        [HttpGet("{emp}")]
        public IActionResult GetValues(EmpModel.EmpIds emp)
        {
            try
            { return Ok(ProductDeptsService.GetValues(emp)); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }

    }
}
