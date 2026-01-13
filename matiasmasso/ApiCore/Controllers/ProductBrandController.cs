using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [OutputCache(PolicyName = "ProductBrands")]
    public class ProductBrandController : ControllerBase
    {
        [HttpGet("{guid}")]
        public IActionResult Find(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = ProductBrandService.Find(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpPost]
        public IActionResult Update([FromBody] ProductBrandModel model)
        {
            IActionResult retval;
            try
            {
                var value = ProductBrandService.Update(model);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("delete/{guid}")]
        public IActionResult Delete(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = ProductBrandService.Delete(guid);
                retval = Ok(value);
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
    [OutputCache(PolicyName = "ProductBrands")]
    public class ProductBrandsController : ControllerBase
    {
        [HttpGet("{emp}")]
        public IActionResult GetValues(EmpModel.EmpIds emp)
        {
            try
            { return Ok(ProductBrandsService.All(emp)); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }

    }

}
