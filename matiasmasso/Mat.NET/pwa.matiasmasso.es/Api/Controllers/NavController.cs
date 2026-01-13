using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NavController : ControllerBase
    {
        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = NavService.Get(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost]
        public IActionResult Update([FromBody] NavDTO.Model model)
        {
            IActionResult retval;
            try
            {
                var value = NavService.Update(model);
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
    public class NavsController : ControllerBase
    {
        [HttpGet]
        public IActionResult All()
        {
            IActionResult retval;
            try
            {
                var value = NavsService.Full();
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("custom/{emp}")]
        public IActionResult Custom(int emp)
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                var lang = HttpHelper.Lang(Request);
                var values = NavsService.Custom(user, lang, emp);
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost("UpdateSortOrder")]
        public async Task<IActionResult> UpdateSortOrder([FromBody] NavDTO nav)
        {
            IActionResult retval;
            try
            {
                var value = await NavsService.UpdateSortOrder(nav);
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
