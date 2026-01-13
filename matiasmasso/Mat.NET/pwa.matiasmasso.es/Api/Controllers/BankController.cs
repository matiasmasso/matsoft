using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankController : ControllerBase
    {

        [HttpGet("logo/{guid}")]
        public IActionResult Logo(Guid guid)
        {
            IActionResult retval;
            try
            {
                string mimeType = "image/jpeg";
                var value = BankService.Logo(guid);
                retval = new FileContentResult(value, mimeType);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }
    }

}
