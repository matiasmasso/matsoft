using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Newtonsoft.Json;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;
using Api.Helpers;
using System.Runtime.Versioning;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [OutputCache(PolicyName = "DownloadTargets")]

    public class DownloadTargetController : ControllerBase
    {

        [HttpGet("thumbnail/{guid}")]
        public IActionResult Thumbnail(Guid guid)
        {
            IActionResult retval = new EmptyResult();
            try
            {
                Media? thumbnail = DownloadTargetService.Thumbnail(guid);
                if (thumbnail != null)
                {
                    string contentType = MimeHelper.ContentType((Media.MimeCods)thumbnail.Mime);
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



        [HttpPost, SupportedOSPlatform("windows")]
        public async Task<IActionResult> UpdateAsync()
        {
            IActionResult retval;
            try
            {
                var form = await Request.ReadFormAsync();
                var data = form["Data"];
                var model = JsonConvert.DeserializeObject<DownloadTargetModel>(data);
                if (model != null)
                {
                    if (form.Files.Count > 0)
                    {
                        await FileUploadHelper.LoadFileStream(model.DocFile, form.Files);
                    }

                    DownloadTargetService.Update(model);
                    retval = Ok(true);
                }
                else
                    retval = BadRequest(new Exception("null model"));

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
    [OutputCache(PolicyName = "DownloadTargets")]

    public class DownloadTargetsController : ControllerBase
    {

        [HttpGet("{target}")]
        public IActionResult FromTarget(Guid target)
        {
            try { return Ok(DownloadTargetsService.FromTarget(target)); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }


    }

}
