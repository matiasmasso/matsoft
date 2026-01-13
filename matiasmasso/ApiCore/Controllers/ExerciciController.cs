using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ExerciciController : ControllerBase
    {
        [HttpGet("apertura/{emp}/{year}")]
        public IActionResult Apertura(EmpModel.EmpIds emp, int year)
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                #if DEBUG
                if( user == null) user = UserModel.Wellknown(UserModel.Wellknowns.matias);
                #endif
                if (user == null) throw new Exception("missing user permissions");

                var exercici = new ExerciciModel(emp, year);
                ExerciciService.Apertura(user, exercici);
                retval = Ok(true);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("renumera/{emp}/{year}")]
        public IActionResult Renumera(EmpModel.EmpIds emp, int year)
        {
            IActionResult retval;
            try
            {
                ExerciciService.Renumera(emp, year);
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
    public class ExercicisController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetValues()
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                if (user == null) throw new Exception("missing user permissions");
                var values = ExercicisService.GetValues(user);
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
