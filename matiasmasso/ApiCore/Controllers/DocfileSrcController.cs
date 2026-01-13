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
    public class DocfileSrcController : ControllerBase
    {


        [HttpPost, SupportedOSPlatform("windows")]
        public async Task<IActionResult> UpdateAsync()
        {
            IActionResult retval;
            try
            {
                var form = await Request.ReadFormAsync();
                var data = form["Data"];
                var model = JsonConvert.DeserializeObject<DocfileSrcModel>(data);
                if (model != null)
                {
                    if (form.Files.Count > 0)
                    {
                        await FileUploadHelper.LoadFileStream(model.Docfile, form.Files);
                    }

                    DocfileSrcService.Update(model);
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

        [HttpPost("delete")]
        public IActionResult Delete([FromBody] DocfileSrcModel model)
        {
            IActionResult retval;
            try
            {
                var value = DocfileSrcService.Delete(model);
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