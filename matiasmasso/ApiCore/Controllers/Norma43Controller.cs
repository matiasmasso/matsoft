using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Api.Services.Integracions.Banca;
using DTO.Integracions.Banca;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class Norma43Controller : ControllerBase
    {

        [HttpPost()]
        public IActionResult UploadContent([FromBody] string filecontents)
        {
            IActionResult retval;
            try
            {
                var uploadedValue = Norma43.Factory(filecontents);
                var user = HttpHelper.User(Request);
                if (user != null)
                {
                    Norma43Service.Update(user, uploadedValue);
                    retval = Ok(true);
                }
                else
                    throw new Exception("missing user");
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost("assign/{n43guid}")]
        public IActionResult Assign(Guid n43guid, [FromBody] Guid ccaGuid)
        {
            IActionResult retval;
            try
            {
                Norma43Service.Assign(n43guid, ccaGuid);
                retval = Ok(true);
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
    public class Norma43sController : ControllerBase
    {
        [HttpGet()]
        public IActionResult PendingApunts()
        {
            try
            { return Ok(Norma43Service.PendingApunts()); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }
    }
}
