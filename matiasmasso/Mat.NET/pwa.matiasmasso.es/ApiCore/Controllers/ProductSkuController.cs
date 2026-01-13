using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [OutputCache(PolicyName = "ProductSkus")]
    public class ProductSkuController : ControllerBase
    {

        IOutputCacheStore cache;

        public ProductSkuController(IOutputCacheStore cache)
        {
            this.cache = cache;
        }

        [HttpGet("{guid}")]
        public IActionResult Find(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = ProductSkuService.Find(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpPost]
        public async Task<IActionResult> Update([FromBody] ProductSkuModel model)
        {
            IActionResult retval;
            try
            {
                var value = ProductSkuService.Update(model);
                await cache.Clear(OutputCacheExtensions.Tags.ProductSkus);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("delete/{guid}")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = ProductSkuService.Delete(guid);
                await cache.Clear(OutputCacheExtensions.Tags.ProductSkus);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }




        [HttpGet("Image/{guid}.jpg")]
        public IActionResult Thumbnail(Guid guid)
        {
            IActionResult retval;
            try
            {
                string mimeType = "image/jpeg";
                byte[]? value = ProductSkuService.Image(guid);
                retval = new FileContentResult(value, mimeType);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpGet("Thumbnail/{guid}.jpg")]
        public IActionResult Image(Guid guid)
        {
            IActionResult retval;
            try
            {
                string mimeType = "image/jpeg";
                byte[]? value = ProductSkuService.Thumbnail(guid);
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
    public class ProductSkusController : ControllerBase
    {
        [HttpGet("{empId}")]
        public IActionResult GetValues(EmpModel.EmpIds empId)
        {
            try
            { return Ok(ProductSkusService.GetValues(empId)); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }

        [HttpGet("stocks")]
        public IActionResult Stocks()
        {
            try
            { return Ok(SkuStocksService.GetValues()); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }

        [HttpGet("pncs")]
        public IActionResult Pncs()
        {
            try
            { return Ok(SkuPncsService.GetValues()); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }

    }


}
