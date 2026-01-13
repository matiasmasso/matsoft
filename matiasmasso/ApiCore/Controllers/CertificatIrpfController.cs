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
    public class CertificatIrpfController : ControllerBase
    {

        [HttpGet("pdf/{cert}")]
        public IActionResult Pdf(Guid cert)
        {
            IActionResult retval;
            try
            {
                var docfile = CertificatIrpfService.Docfile(cert);
                return docfile?.Document?.Data == null ? new NotFoundResult() : new FileContentResult(docfile.Document.Data, docfile.Document.ContentType());
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
                var model = JsonConvert.DeserializeObject<CertificatIrpfModel>(data);
                if (model != null)
                {
                    if (form.Files.Count > 0)
                    {
                        await FileUploadHelper.LoadFileStream(model.Docfile, form.Files);
                    }

                    CertificatIrpfService.Update(model);
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
    public class CertificatsIrpfController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetValues()
        {
            IActionResult retval;
            try
            {
                    return Ok(CertificatsIrpfService.GetValues());
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }




    }
}
