using Microsoft.AspNetCore.Mvc;
using Api.Services;
using DTO;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductLandingPageController: ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult Fetch(Guid guid)
        {
            IActionResult retval;
            try
            {
                var lang = HttpHelper.Lang(Request);
                var value = ProductLandingPageService.Fetch(guid, lang);
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
