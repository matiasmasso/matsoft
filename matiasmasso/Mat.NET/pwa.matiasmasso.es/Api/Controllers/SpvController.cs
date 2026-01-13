using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SpvController : ControllerBase
    {

        [HttpGet("{guid}")]
        public async Task<IActionResult> Fetch(Guid guid)
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                if (user == null) throw new Exception("User unknown");
                var value = SpvService.Find(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpPost()]
        public async Task<IActionResult> Update([FromBody] SpvModel model)
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                if (user == null) throw new Exception("User unknown");
                var value =  SpvService.Update(model);
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
    public class SpvsController : ControllerBase
    {

        [HttpGet()]
        public IActionResult Fetch()
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                if (user == null) throw new Exception("User unknown");
                var values = SpvsService.All(user);
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