using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            IActionResult retval;
            try
            {
                var value = "Hello, world!";
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
                //https://www.c-sharpcorner.com/article/standardize-your-web-apis-error-response-with-problemdetails-class/
            }
            return retval;
        }




    }
}