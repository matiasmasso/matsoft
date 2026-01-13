using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PubController : ControllerBase
    {
        private readonly PubService _pubService;
        public PubController(PubService pubService)
        {
            _pubService = pubService;
        }


        [HttpGet("{guid}")]
        public IActionResult Find(Guid guid)
        {
            try { return Ok(_pubService.Find(guid)); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }

        [HttpGet("media/{guid}")]
        public IActionResult Media(Guid guid)
        {
            try
            {
                var value = _pubService.Media(guid);
                return (value == null) ? new NotFoundResult() : new FileContentResult(value.Data!, value.ContentType());
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }


        [HttpGet("thumbnail/{guid}")]
        public IActionResult Thumbnail(Guid guid)
        {
            try
            {
                var value = _pubService.Thumbnail(guid);
                return (value == null) ? new NotFoundResult() : new FileContentResult(value.Data!, value.ContentType());
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }


        [HttpPost]
        public async Task<IActionResult> UpdateAsync()
        {
            try
            {
                bool value = false;
                var form = await Request.ReadFormAsync();
                if (form.ContainsKey("Data"))
                {
                    var data = form["Data"];
                    var model = JsonConvert.DeserializeObject<PubModel>(data!);
                    if (model != null)
                    {
                        var file = form.Files["File"];
                        if (file != null) model.Docfile!.Document!.Data = await DocFileController.IFormFileBytes(file);

                        var thumbnail = form.Files["Thumbnail"];
                        if (thumbnail != null) model.Docfile!.Thumbnail!.Data = await DocFileController.IFormFileBytes(thumbnail);

                        value = _pubService.Update(model);
                    }
                    return Ok(value);
                }
                else
                {
                    return BadRequest(new ProblemDetails
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Title = String.Format("Error on uploading Pub"),
                        Detail = "No data received on Pub Controller form[\"Data\"]"
                    });
                }

            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }


        [HttpGet("delete/{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var value = _pubService.Delete(guid);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }

    }


    [ApiController]
    [Route("[controller]")]
    public class PubsController : ControllerBase
    {
        private readonly PubsService _pubsService;
        public PubsController(PubsService pubsService)
        {
            _pubsService = pubsService;
        }

        [HttpGet]
        public IActionResult All()
        {
            try { return Ok(_pubsService.All()); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }
    }


}
