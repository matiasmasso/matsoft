using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CcaSchedController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult GetValue(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = CcaSchedService.GetValue(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpPost()]
        public IActionResult Update([FromBody] CcaSchedModel model)
        {
            IActionResult retval;
            try
            {
                var value = CcaSchedService.Update(model);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpGet("delete/{guid}")]
        public IActionResult Delete(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = CcaSchedService.Delete(guid);
                retval = Ok(value);
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
    public class CcaSchedsController : ControllerBase
    {
        [HttpGet()]
        public IActionResult GetValues()
        {
            try
            { return Ok(CcaSchedsService.GetValues()); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }

        [HttpGet("executeDueScheds")]
        public IActionResult ExecuteDueScheds()
        {
            try
            {
                UserModel? user;
#if DEBUG
                user = UserModel.Wellknown(UserModel.Wellknowns.matias);
#else
                user = HttpHelper.User(Request);
#endif

                if (user == null) throw new Exception("missing user permissions");
                var value = CcaSchedsService.ExecuteDueScheds(user);
                return Ok(value);
            }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }
    }
}