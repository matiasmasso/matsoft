using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MarketPlaceController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult Fetch(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = MarketPlaceService.Find(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpPost]
        public IActionResult Update([FromBody] MarketPlaceModel model)
        {
            IActionResult retval;
            try
            {
                var value = MarketPlaceService.Update(model);
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
                var value = MarketPlaceService.Delete(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("offers/{marketplace}")]
        public IActionResult Offers(Guid marketplace)
        {
            IActionResult retval;
            try
            {
                var values = MarketPlaceService.Offers(marketplace);
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost("offer")]
        public IActionResult UpdateOffer([FromBody] OfferModel offer)
        {
            IActionResult retval;
            try
            {
                var values = MarketPlaceService.UpdateOffer(offer);
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost("offer/delete")]
        public IActionResult DeleteOffer([FromBody] OfferModel offer)
        {
            IActionResult retval;
            try
            {
                var values = MarketPlaceService.DeleteOffer(offer);
                retval = Ok(values);
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
    public class MarketPlacesController : ControllerBase
    {

        [HttpGet("{emp}")]
        public IActionResult Fetch(int emp)
        {
            IActionResult retval;
            try
            {
                var values = MarketPlacesService.All(emp);
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