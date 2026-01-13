using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Newtonsoft.Json;
using Api.Extensions;
using Api.Helpers;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EscripturaController : ControllerBase
    {
        [HttpGet("{Guid}")]
        public IActionResult GetValue(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = EscripturaService.GetValue(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("pdf/{guid}")]
        public async Task<IActionResult> Document(Guid guid)
        {
            IActionResult retval;
            try
            {
                Media? value = EscripturaService.Document(guid);
                retval = new FileContentResult(value.Data, value.ContentType());
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
                var data = form["Data"];
                var model = JsonConvert.DeserializeObject<EscripturaModel>(data!);
                if (model != null)
                {
                    if (form.Files.Count > 0)
                    {
                        await FileUploadHelper.LoadFileStream(model.Docfile, form.Files);
                    }

                    EscripturaService.Update(model);
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



        [HttpGet("delete/{guid}")]
        public IActionResult Delete(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = EscripturaService.Delete(guid);
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
    public class EscripturasController : ControllerBase
    {

        [HttpGet]
        public IActionResult Fetch()
        {
            IActionResult retval;
            try
            {
                var values = EscripturasService.Fetch();
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