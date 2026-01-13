using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Newtonsoft.Json;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TransmissionsController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetValues()
        {
            IActionResult retval;
            try
            {
                //var user = HttpHelper.User(Request);
                //if (user == null) throw new Exception("User not found");
                var values = TransmissionsService.GetValues((int)EmpModel.EmpIds.MatiasMasso,DateTime.Today.Year);
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
