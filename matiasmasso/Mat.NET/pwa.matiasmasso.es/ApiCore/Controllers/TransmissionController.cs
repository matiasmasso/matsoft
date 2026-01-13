using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Newtonsoft.Json;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransmissionController : ControllerBase
    {

        [HttpGet("deliveries/{guid}")]
        public IActionResult deliveries(Guid guid)
        {
            IActionResult retval;
            try
            {
                var values = TransmissionService.Deliveries(guid);
                return Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("deliveries/pdf/{guid}")]
        public IActionResult deliveriesPdf(Guid guid)
        {
            IActionResult retval;
            try
            {
                var media = TransmissionService.DeliveriesPdf(guid);
                return media?.Data == null ? new NotFoundResult() : new FileContentResult(media.Data, media.ContentType());
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpPost]
        public IActionResult Update(TransmissionModel value)
        {
            IActionResult retval;
            try
            {
                var id = TransmissionService.Update(value);
                return Ok(id);
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
    public class TransmissionsController : ControllerBase
    {

        [HttpGet("{year}")]
        public IActionResult GetValues(int year)
        {
            IActionResult retval;
            try
            {
                var values = TransmissionsService.GetValues((int)EmpModel.EmpIds.MatiasMasso,year);
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
