using Microsoft.AspNetCore.Mvc;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Fetch()
        {
            IActionResult retval = Ok("Hello, World!");
            return retval;
        }
    }
}