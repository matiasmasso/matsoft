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
    public class DocSrcController : ControllerBase
    {
        private readonly DocSrcService _docSrcService;

       public DocSrcController(DocSrcService docSrcService)
        {
            _docSrcService = docSrcService;
        }

        [HttpGet("{guid}")]
        public IActionResult Find(Guid guid)
        {
            try { return Ok(_docSrcService.Find(guid)); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }


        [HttpGet("media/{guid}")]
        public IActionResult Media(Guid guid)
        {
            try
            {
                var value = _docSrcService.Media(guid);
                return (value == null) ? new NotFoundResult() : new FileContentResult(value.Data!, value.ContentType());
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }


        [HttpGet("thumbnail/{guid}")]
        public IActionResult Thumbnail(Guid guid)
        {
            try
            {
                var value = _docSrcService.Thumbnail(guid);
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
                    var model = JsonConvert.DeserializeObject<DocSrcModel>(data!);
                    if (model != null)
                    {
                        var file = form.Files["File"];
                        if (file != null) model.DocFile!.Document!.Data = await DocFileController.IFormFileBytes(file);

                        var thumbnail = form.Files["Thumbnail"];
                        if (thumbnail != null) model.DocFile!.Thumbnail!.Data = await DocFileController.IFormFileBytes(thumbnail);

                        value = _docSrcService.Update(model);
                    }
                    return Ok(value);
                }
                else
                {
                    return BadRequest(new Microsoft.AspNetCore.Mvc.ProblemDetails
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Title = String.Format("Error on uploading DocSrc"),
                        Detail = "No data received on DocSrc Controller form[\"Data\"]"
                    });
                }

            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }



        [HttpGet("delete/{guid}")]
        public IActionResult Delete(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = _docSrcService.Delete(guid);
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
    public class DocSrcsController : ControllerBase
    {
        private readonly DocSrcsService _docSrcsService;

        public DocSrcsController(DocSrcsService docSrcsService)
        {
            _docSrcsService = docSrcsService;
        }

        [HttpGet]
        public IActionResult All()
        {
            try { return Ok(_docSrcsService.All()); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }
    }


}
