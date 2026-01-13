using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
        [OutputCache(PolicyName = "ProductCategories")]
    public class ProductCategoryController : ControllerBase
    {
        private IOutputCacheStore cache;
        public ProductCategoryController(IOutputCacheStore cache)
        {
            this.cache = cache;
        }


        [HttpGet("{guid}")]
        public IActionResult Find(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = ProductCategoryService.Find(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpGet("Image/{guid}.jpg")]

        public IActionResult Image(Guid guid)
        {
            IActionResult retval;
            try
            {
                string mimeType = "image/jpeg";
                byte[]? value = ProductCategoryService.Image(guid);
                return new FileContentResult(value, mimeType);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }

    }


    [ApiController]
    [Route("[controller]")]
    [OutputCache(PolicyName = "ProductCategories")]
    public class ProductCategoriesController : ControllerBase
    {
        [HttpGet("{emp}")]
        public IActionResult GetValues(EmpModel.EmpIds emp)
        {
            try
            {return Ok(ProductCategoriesService.All(emp));}
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }


    }

}
