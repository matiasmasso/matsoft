using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Api.Helpers;
using Newtonsoft.Json;
using System.Runtime.Versioning;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactDocController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult Fetch(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = ContactDocService.GetValue(guid);
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
            IActionResult retval;
            try
            {
                var media = ContactDocService.Docfile(guid);
                return media?.Data == null ? new NotFoundResult() : new FileContentResult(media.Data, media.ContentType());
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
                if (form.ContainsKey("Data"))
                {
                    var data = form["Data"];
                    var model = JsonConvert.DeserializeObject<ContactDocModel>(data!);
                    if (model != null)
                    {
                        if (form.Files.Count > 0)
                        {
                            await FileUploadHelper.LoadFileStream(model.Docfile, form.Files);
                        }

                        ContactDocService.Update(model);
                        retval = Ok(true);
                    }
                    else
                        retval = BadRequest(new Exception("null model"));
                }
                else
                    retval = BadRequest(new Exception("null data"));

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
    public class ContactDocsController : ControllerBase
    {

        [HttpGet("{contact}")]
        public IActionResult FromContact(Guid contact)
        {
            IActionResult retval;
            try
            {
                var values = ContactDocsService.GetValues(contact);
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