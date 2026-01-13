using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ShoppingBasketController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult Find(Guid guid)
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                if (user == null) throw new Exception("User unknown");
                var value = ShoppingBasketService.Find(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpPost()]
        public IActionResult Update([FromBody] ShoppingBasketModel model)
        {
            IActionResult retval;
            try
            {
                var value = ShoppingBasketService.Update(model);
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
                var value = ShoppingBasketService.Delete(guid);
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
    public class ShoppingBasketsController : ControllerBase
    {
        [HttpGet("{mkp}")]
        public IActionResult GetValues(MarketPlaceModel.Wellknowns mkp)
        {
            IActionResult retval;
            try
            {
                var marketplace = MarketPlaceModel.Wellknown(mkp);
                if (marketplace == null) throw new Exception("Marketplace unknown");
                var values = ShoppingBasketsService.All(marketplace);
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