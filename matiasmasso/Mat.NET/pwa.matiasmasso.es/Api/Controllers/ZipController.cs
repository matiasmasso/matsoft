using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ZipController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult Fetch(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = ZipService.Find(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost("Lookup/{countryIso}")]
        public IActionResult Lookup(string countryIso, [FromBody] string zipcod)
        {
            IActionResult retval;
            try
            {
                var lang = HttpHelper.Lang(Request);
                var value = ZipService.Lookup(countryIso, zipcod, lang);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpPost]
        public IActionResult Update([FromBody] ZipModel model)
        {
            IActionResult retval;
            try
            {
                var value = ZipService.Update(model);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost("FromGeocode")]
        public IActionResult UpdateFromGeocode([FromBody] GoogleApi.GeoCodeModel geoCode)
        {
            IActionResult retval;
            try
            {
                var value = ZipService.UpdateFromGeocode(geoCode);
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
                var value = ZipService.Delete(guid);
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
