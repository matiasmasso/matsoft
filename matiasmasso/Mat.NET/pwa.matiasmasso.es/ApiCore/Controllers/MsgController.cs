using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MsgController:ControllerBase
    {
        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = MsgService.Find(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost]
        public IActionResult Update([FromBody] MsgModel model)
        {
            IActionResult retval;
            try
            {
                var value = MsgService.Update(model);
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
                var value = MsgService.Delete(guid);
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
    public class MsgsController : ControllerBase
    {
        [HttpGet]
        public IActionResult All()
        {
            IActionResult retval;
            try
            {
                var value = MsgsService.All();
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("{src}")]
        public IActionResult FromSrc(int src)
        {
            IActionResult retval;
            try
            {
                var value = MsgsService.All(src);
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


