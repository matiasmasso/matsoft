using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Newtonsoft.Json;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DownloadTargetController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = DownloadTargetService.FromTarget(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("thumbnail/{guid}")]
        public IActionResult Thumbnail(Guid guid)
        {
            IActionResult retval = new EmptyResult();
            try
            {
                Media? thumbnail = DownloadTargetService.Thumbnail(guid);
                if (thumbnail != null)
                {
                    string contentType = MimeHelper.ContentType((MimeHelper.MimeCods)thumbnail.Mime);
                    retval = new FileContentResult(thumbnail.Data, contentType);
                }
                return retval;
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync()
        {
            IActionResult retval;
            try
            {
                var form = await Request.ReadFormAsync();
                var file = form.Files["File"];
                var data = form["Data"];
                var model = JsonConvert.DeserializeObject<VehicleModel>(data);
                if (model == null)
                    throw (new System.Exception("null VehicleModel received on Api"));
                else
                {
                    var value = await VehicleService.UpdateAsync(model, file);
                    retval = Ok(value);
                }
                retval = Ok(true);
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
                var value = VehicleService.Delete(guid);
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
