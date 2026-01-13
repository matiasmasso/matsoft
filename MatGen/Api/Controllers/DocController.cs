using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;
using Api.Entities;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocController : ControllerBase
    {
        private readonly DocService _docService;
        private readonly DocFileService _docFileService;

        public DocController(DocService docService, DocFileService docFileService)
        {
            _docService = docService;
            _docFileService = docFileService;
        }


        [HttpGet("{guidOrAsin}")]
        public IActionResult Find(string guidOrAsin)
        {
            if (Guid.TryParse(guidOrAsin, out var guid))
            {
                //payload is Guid => return DocModel
                try { return Ok(_docService.Find(guid)); }
                catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
            }
            else if (guidOrAsin.Length == 10)
            {
                //payload is a 10 characters length string => return Docfile bytes[]
                var asin = guidOrAsin;
                var value = _docFileService.Media(asin);
                return (value == null) ? new NotFoundResult() : new FileContentResult(value.Data!, value.ContentType());
            }
            else
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = String.Format("Error on finding Doc"),
                    Detail = "Invalid guid or asin"
                });
        }


        [HttpGet("thumbnail/{asin}")]
        public IActionResult Thumbnail(string asin)
        {
            try
            {
                var value = _docFileService.Thumbnail(asin);
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
                    var model = JsonConvert.DeserializeObject<DocModel>(data!);
                    if (model != null)
                    {
                        if(model.DocFile != null)
                        {
                        var filename = model.DocFile!.HashFilename();
                        var file = form.Files[filename];
                        if(file != null) model.DocFile!.Document!.Data = await DocFileController.IFormFileBytes(file);

                            var thumbnailname = model.DocFile!.HashThumbnailname();
                            var thumbnail = form.Files[thumbnailname];
                            if (thumbnail != null) model.DocFile!.Thumbnail!.Data = await DocFileController.IFormFileBytes(thumbnail);
                        }


                        value = _docService.Update(model);
                    }
                    return Ok(value);
                }
                else
                {
                    return BadRequest(new ProblemDetails
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Title = String.Format("Error on uploading Doc"),
                        Detail = "No data received on Doc Controller form[\"Data\"]"
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
                var value = _docService.Delete(guid);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }

    }


    [ApiController]
    [Route("[controller]")]
    public class DocsController : ControllerBase
    {
        private readonly DocsService _docsService;
        public DocsController(DocsService docsService)
        {
            _docsService = docsService;
        }

        [HttpGet]
        public IActionResult All()
        {
            try { return Ok(_docsService.All()); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }
    }


}
