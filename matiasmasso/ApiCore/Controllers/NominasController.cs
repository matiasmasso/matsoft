using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Newtonsoft.Json;
using System.Runtime.Versioning;
using Api.Helpers;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class NominaController : ControllerBase
    {
        [HttpGet("delete/{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                NominaService.Delete(guid);
                return Ok(true); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class NominasController : ControllerBase
    {
        [HttpGet()]
        public IActionResult GetValues()
        {
            try
            { return Ok(NominasService.GetValues()); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }

        [HttpPost, SupportedOSPlatform("windows")] 
        public async Task<IActionResult> UpdateAsync()
        {
            IActionResult retval;
            try
            {
                var form = await Request.ReadFormAsync();
                var data = form["Data"];
                var model = JsonConvert.DeserializeObject<List<NominaModel>>(data!);
                if (model != null)
                {
                    var files = form.Files;
                    foreach (var nomina in model)
                    {
                        await FileUploadHelper.LoadFileStream(nomina.Cca?.Docfile, files);
                    }
                    NominasService.Update(model);
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
        public IActionResult Delete([FromBody] List<Guid> guids)
        {
            try
            {
                NominasService.Delete(guids);
                return Ok(true);
            }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }

    }
}
