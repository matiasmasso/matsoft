using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MgzInventoryController : ControllerBase
    {

        [HttpPost("{mgzGuid}")]
        public IActionResult GetValues(Guid mgzGuid, [FromBody] DateTime fch)
        {
            IActionResult retval;
            try
            {
                var mgz = new MgzModel(mgzGuid);
                var values = MgzInventoryService.GetValues(mgz,fch);
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost("extracte/{mgzGuid}/{skuGuid}")]
        public IActionResult Extracte(Guid mgzGuid, Guid skuGuid, [FromBody] DateTime fch)
        {
            IActionResult retval;
            try
            {
                var mgz = new MgzModel(mgzGuid);
                var sku = new ProductSkuModel(skuGuid);
                var value = MgzInventoryService.Extracte(mgz,sku,fch);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

    }


}
