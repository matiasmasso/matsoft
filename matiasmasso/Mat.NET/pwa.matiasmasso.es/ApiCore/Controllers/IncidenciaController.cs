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
    public class IncidenciaController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = IncidenciaService.Get(guid);
                retval = Ok(value);
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
                var model = JsonConvert.DeserializeObject<IncidenciaModel>(data);
                if (model != null)
                {
                    var files = form.Files;
                    foreach (var ticket in model.PurchaseTickets)
                    {
                        await FileUploadHelper.LoadFileStream(ticket, files);
                    }
                    foreach (var imageDoc in model.DocFileImages)
                    {
                        await FileUploadHelper.LoadFileStream(imageDoc, files);
                    }
                    foreach (var video in model.Videos)
                    {
                        await FileUploadHelper.LoadFileStream(video, files);
                    }

                    await IncidenciaService.Update(model);
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
    public class IncidenciasController : ControllerBase
    {

        [HttpGet("open/{emp}")]
        public IActionResult Open(int emp)
        {
            IActionResult retval;
            try
            {
                var values = IncidenciasService.Open(emp);
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("{emp}")]
        public IActionResult All(int emp)
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                if (user == null) throw new Exception("User unknown");

                var lang = HttpHelper.Lang(Request);
                var values = IncidenciasService.All(user, lang, emp);
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
