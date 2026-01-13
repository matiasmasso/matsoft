using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class AtlasController:ControllerBase
    {

        [HttpPost]
        public IActionResult Model([FromBody] AtlasModel request)
        {
            IActionResult retval;
            try
            {
                var value = AtlasService.Model(request);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Error on reading Atlas",
                    Detail = ex.Message
                });
            }
            return retval;
        }


        [HttpGet("Contacts")]
        public IActionResult Contacts()
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                if (user == null) throw new Exception("User unknown");
                var value = AtlasService.Contacts(user.Emp);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Error on reading Atlas",
                    Detail = ex.Message
                });
            }
            return retval;
        }


    }
}
