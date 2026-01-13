using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BancController : ControllerBase
    {

        [HttpGet("logo/{guid}")]
        public IActionResult Logo(Guid guid)
        {
            IActionResult retval;
            try
            {
                string mimeType = "image/jpeg";
                var value = BancService.Logo(guid) ?? new byte[]{ };
                retval = new FileContentResult(value, mimeType);
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
    public class BancsController : ControllerBase
    {

        [HttpGet("saldos")]
        public IActionResult Saldos()
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                if (user == null) throw new Exception("User unknown");
                var values = BancsService.Saldos(user);
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