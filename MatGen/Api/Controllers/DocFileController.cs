using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using DTO.Helpers;
using Api.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DocFileController : ControllerBase
    {
        private readonly DocFileService _docFileService;

        public DocFileController(DocFileService docFileService)
        {
            _docFileService = docFileService;
        }

        [HttpGet("{asin}")]
        public IActionResult Find(string asin)
        {
            try
            {
                var value = _docFileService.Find(asin);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }

        [HttpPost("findBySha256")]
        public IActionResult FindBySha256([FromBody] string sha256)
        {
            try
            {
                var value = _docFileService.FindBySha256(sha256);
                return Ok(value);
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
                    var model = JsonConvert.DeserializeObject<DocfileModel>(data!);
                    if (model != null)
                    {
                        var file = form.Files["File"];
                        if (file != null) model.Document!.Data = await IFormFileBytes(file);

                        var thumbnail = form.Files["Thumbnail"];
                        if (thumbnail != null) model.Thumbnail!.Data = await IFormFileBytes(thumbnail);

                        value = _docFileService.Update(model!);
                    }
                    return Ok(value);
                }
                else
                {
                    return BadRequest(new Microsoft.AspNetCore.Mvc.ProblemDetails
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Title = String.Format("Error on uploading DocFile"),
                        Detail = "No data received on DocFile Controller form[\"Data\"]"
                    });

                }
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }


        [HttpGet("delete/{asin}")]
        public  IActionResult Delete(string asin)
        {
            try
            {
                var value = _docFileService.Delete(asin);
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }

        /// <summary>
        /// needs Ghostcript installed on Azure to work
        /// </summary>
        /// <returns></returns>
        [HttpPost("rasterize")]
        public async Task<IActionResult> Rasterize()
        {
            try
            {
                DocfileModel? value = null;
                var form = await Request.ReadFormAsync();
                if (form.Files.Count > 0)
                {
                    var file = form.Files[0];
                    //var fileBytes = await file.BytesAsync();
                    //value = PdfService.Rasterize(fileBytes);
                    //if (value != null)
                    //{
                    //    value.Document = null;
                    //    value.Thumbnail = null;
                    //}
                }
                return Ok(value);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }


        public static async Task<byte[]> IFormFileBytes(IFormFile file)
        {
            Stream stream = file.OpenReadStream();
            MemoryStream ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            stream.Close();

            ms.Seek(0, SeekOrigin.Begin);
            return ms.ToArray();
        }

    }

    [ApiController]
    [Route("[controller]")]
    public class DocFilesController : ControllerBase
    {
        private readonly DocFilesService _docFilesService;

        public DocFilesController(DocFilesService docFilesService)
        {
            _docFilesService = docFilesService;
        }

        [HttpGet]
        public IActionResult All()
        {
            try { return Ok(_docFilesService.All()); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }

        //[HttpGet("SetSha256")]
        //public IActionResult SetSha256()
        //{
        //    IActionResult retval;
        //    try
        //    {
        //        int count = DocsService.SetSha256();
        //        retval = Ok($"{count} Sha256 generats Ok");
        //    }
        //    catch (Exception ex)
        //    {
        //        retval = BadRequest(ex.ProblemDetails());
        //    }
        //    return retval;
        //}


        //[HttpGet("SetAsin")]
        //public async Task<IActionResult> SetAsinAsync()
        //{
        //    IActionResult retval;
        //    try
        //    {
        //        var count = await DocsService.SetAsinAsync();
        //        retval = Ok($"{count} asins generats Ok");
        //    }
        //    catch (Exception ex)
        //    {
        //        retval = BadRequest(ex.ProblemDetails());
        //    }
        //    return retval;
        //}
    }


}
