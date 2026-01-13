using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeliveryController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult Fetch(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = DeliveryService.Fetch(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("pdf/{guid}")]
        public IActionResult Pdf(Guid guid)
        {
            try
            {
                Media value = DeliveryService.Pdf(guid);
                return value?.Data == null ? new NotFoundResult() : new FileContentResult(value.Data, value.ContentType());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ProblemDetails());
            }
        }

        [HttpGet("WithTracking/{guid}")]
        public IActionResult WithTracking(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = DeliveryService.WithTracking(guid);
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
    public class DeliveriesController : ControllerBase
    {
        [HttpGet("{emp}")]
        public IActionResult GetValues(int emp)
        {
            IActionResult retval;
            try
            {
                var values = DeliveriesService.GetValues(emp);
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("{emp}/{year}")]
        public IActionResult GetValues(int emp, int year)
        {
            IActionResult retval;
            try
            {
                var values = DeliveriesService.GetValues(emp, year);
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("pdfs")]
        public IActionResult Pdf([FromBody] List<Guid> guids)
        {
            //TO DO: cridar desd de api.matiasmasso.es/api/Transmisio/PdfDeliveries/{guid}"
            try
            {
                Media value = DeliveriesService.Pdfs(guids);
                return value?.Data == null ? new NotFoundResult() : new FileContentResult(value.Data, value.ContentType());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ProblemDetails());
            }
        }

        [HttpPost]
        public IActionResult Update([FromBody] List<DeliveryModel> values)
        {
            IActionResult retval;
            try
            {
                DeliveriesService.Update(values);
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
