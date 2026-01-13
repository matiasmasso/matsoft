using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ContactClassesController : ControllerBase
    {

        [HttpGet()]
        public IActionResult All()
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                if (user == null) throw new Exception("User unknown");
                var lang = HttpHelper.Lang(Request);
                if (lang == null) lang = LangDTO.Default();
                var values = ContactClassesService.All(lang);
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
