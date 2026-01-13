using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SelloutController : ControllerBase
    {


        [HttpPost()]
        public IActionResult Post([FromBody] SelloutDTO.Request request)
        {
            var user = HttpHelper.User(Request);
            if (user == null) throw new Exception("User unknown");
            var value = SelloutService.Load(request, user);
            IActionResult retval = Ok(value);
            return retval;
        }

        [HttpPost("orders")]
        public IActionResult Orders([FromBody] SelloutDTO.Request request)
        {
            var value = new SelloutDTO();
            var user = HttpHelper.User(Request);
            if (user == null) throw new Exception("User unknown");
            value.Orders = SelloutService.Orders(request,user);
            IActionResult retval = Ok(value);
            return retval;
        }


    }


}