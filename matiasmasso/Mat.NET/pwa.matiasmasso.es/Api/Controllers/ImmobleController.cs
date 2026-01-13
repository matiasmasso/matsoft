using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class ImmobleController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult Fetch(Guid guid)
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                if (user == null) throw new Exception("User not found");
                var lang = HttpHelper.Lang(Request);
                if (lang == null) throw new Exception("Lang unknown");
                var value = ImmobleService.Fetch(guid, lang!);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpPost]
        public IActionResult Update([FromBody] ImmobleModel model)
        {
            IActionResult retval;
            try
            {
                var value = ImmobleService.Update(model);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("fromInventariItem/{inventariItem}")]
        public IActionResult FromInventariItem(Guid inventariItem)
        {
            IActionResult retval;
            try
            {
                var lang = HttpHelper.Lang(Request);
                if (lang == null) throw new Exception("Lang unknown");
                var value = ImmobleService.FromInventariItem(inventariItem, lang);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpGet("InventariItem/{guid}")]
        public IActionResult InventariItem(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = ImmobleService.InventariItem(guid);
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
    public class ImmoblesController : ControllerBase
    {

        [HttpGet]
        public IActionResult Fetch()
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                if (user == null) throw new Exception("User not found");
                var lang = HttpHelper.Lang(Request);
                var values = ImmoblesService.Fetch(user!,lang!);
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