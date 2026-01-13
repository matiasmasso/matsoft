using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            IActionResult retval;
            try
            {
                UserModel? user = HttpHelper.User(Request);
                LangDTO? lang = HttpHelper.Lang(Request);
                var value = HomeService.Factory(user,lang);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
                //https://www.c-sharpcorner.com/article/standardize-your-web-apis-error-response-with-problemdetails-class/
            }
            return retval;
        }



    }
}