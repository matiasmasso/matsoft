using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProBasketController : ControllerBase
    {
        [HttpGet]
        public IActionResult Basket()
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                var value = ProBasketService.Basket(user!);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("{userGuid}")]
        public IActionResult GetValue(Guid userGuid)
        {
            IActionResult retval;
            try
            {
                var user = UserService.Find(userGuid);
                var value = ProBasketService.Basket(user!);
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
