using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductPluginController: ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        public ProductPluginController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

    }


    [ApiController]
    [Route("[controller]")]
    public class ProductPluginsController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetValues()
        {
            IActionResult retval;
            try
            {
                var values = ProductPluginsService.GetValues();
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
