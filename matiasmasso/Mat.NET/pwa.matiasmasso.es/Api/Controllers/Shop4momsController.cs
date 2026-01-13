using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Api.Services.Integracions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Shop4momsController : ControllerBase
    {

        [HttpGet]
        public IActionResult Fetch()
        {
            IActionResult retval;
            try
            {
                var value = Shop4momsService.Fetch();
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost]
        public IActionResult SaveBasket([FromBody] ShoppingBasketModel basket)
        {
            IActionResult retval;
            try
            {
                Shop4momsService.SaveBasket(basket);
                retval = Ok(true);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost("content")]
        public IActionResult ContentFromSegment([FromBody] string segment)
        {
            IActionResult retval;
            try
            {
                var value = Shop4momsService.Content(segment);
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
