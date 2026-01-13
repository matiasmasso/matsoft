using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Newtonsoft.Json;
using Api.Extensions;

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
                if (form.ContainsKey("Data"))
                {
                    var file = form.Files["File"];
                    var thumbnail = form.Files["Thumbnail"];
                    var data = form["Data"];

                    var model = JsonConvert.DeserializeObject<EscripturaModel>(data);
                    if (model == null)
                        throw (new Exception("null value escriptura data"));
                    else
                        await EscripturaService.UpdateAsync(model, file, thumbnail);
                    retval = Ok(true);
                }
                else
                {
                    retval = BadRequest(new ProblemDetails
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Title = String.Format("Error on uploading Doc"),
                        Detail = "No data received on Doc Controller form[\"Data\"]"
                    });
                }

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