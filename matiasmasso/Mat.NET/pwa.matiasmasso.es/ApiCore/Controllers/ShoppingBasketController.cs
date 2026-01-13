using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ShoppingBasketController : ControllerBase
    {
        IOutputCacheStore cache;

        public ShoppingBasketController(IOutputCacheStore cache)
        {
            this.cache = cache;
        }


        [HttpGet("{guid}")]
        public IActionResult Find(Guid guid)
        {
            try
            {
                var value = ShoppingBasketService.Find(guid);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }


        [HttpGet("fromTpvOrder/{tpvOrder}")]
        public async Task<IActionResult> FromTpvOrder(string tpvOrder)
        {
            try
            {
                var value = ShoppingBasketService.FromTpvOrder(tpvOrder);
                await cache.Clear(OutputCacheExtensions.Tags.Baskets);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }


        [HttpPost()]
        public async Task<IActionResult> Update([FromBody] ShoppingBasketModel model)
        {
            try
            {
                var value = ShoppingBasketService.Update(model);
                await cache.Clear(OutputCacheExtensions.Tags.Baskets);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }


        [HttpGet("delete/{guid}")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            try
            {
                var value = ShoppingBasketService.Delete(guid);
                await cache.Clear(OutputCacheExtensions.Tags.Baskets);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }

    }



    [ApiController]
    [Route("[controller]")]
        [OutputCache(PolicyName = "Baskets")]
    public class ShoppingBasketsController : ControllerBase
    {

        [HttpGet("{mkp}")]
        public IActionResult GetValues(MarketPlaceModel.Wellknowns mkp)
        {
            try
            {
                var marketplace = MarketPlaceModel.Wellknown(mkp);
                if (marketplace == null) throw new Exception("Marketplace unknown");
                var values = ShoppingBasketsService.GetValues(marketplace);
                return Ok(values);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails());}
        }
    }
}