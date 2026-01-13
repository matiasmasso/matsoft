using Microsoft.AspNetCore.Mvc;
using Api.Services;
using DTO;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContentController: ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public ContentController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost("FromUrlSegment")]
        public IActionResult LandingPage([FromBody] string urlSegment)
        {
            IActionResult retval;
            try
            {
                var value = ContentService.FromUrlSegment(urlSegment);
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
