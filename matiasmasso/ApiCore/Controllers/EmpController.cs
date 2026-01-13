using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EmpController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult GetValue(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = EmpService.GetValue(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("fromId/{id}")]
        public IActionResult FromId(EmpModel.EmpIds id)
        {
            IActionResult retval;
            try
            {
                var value = EmpService.FromId(id);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("cert/data/{emp}")]
        public IActionResult CertData(int emp)
        {
            IActionResult retval;
            try
            {
                Media? value = EmpService.CertData(emp);
                return value?.Data == null ? new NotFoundResult() : new FileContentResult(value.Data, value.ContentType());
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("cert/image/{emp}")]
        public IActionResult CertImage(int emp)
        {
            IActionResult retval;
            try
            {
                Media? value = EmpService.CertImage(emp);
                return value?.Data == null ? new NotFoundResult() : new FileContentResult(value.Data, value.ContentType());
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpPost()]
        public IActionResult Update([FromBody] EmpModel model)
        {
            IActionResult retval;
            try
            {
                var value = EmpService.Update(model);
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
                var value = EmpService.Delete(guid);
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
    public class EmpsController : ControllerBase
    {
        [HttpGet()]
        public IActionResult GetValues()
        {
            try
            { return Ok(EmpsService.GetValues()); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }
    }
}