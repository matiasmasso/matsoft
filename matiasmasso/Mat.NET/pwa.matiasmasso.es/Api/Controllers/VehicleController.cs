using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Newtonsoft.Json;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = VehicleService.Find(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("Documents/{guid}")]
        public IActionResult Documents(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = DownloadTargetsService.All(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("Multas/{guid}")]
        public IActionResult Multas(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = MultasService.All(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("image/{guid}")]
        public IActionResult Image(Guid guid)
        {
            IActionResult retval = new EmptyResult();
            try
            {
                DocFileModel? docfile = VehicleService.Docfile(guid);
                if(docfile != null)
                {
                    string contentType = MimeHelper.ContentType((MimeHelper.MimeCods)docfile!.StreamMime!);
                    retval = new FileContentResult(docfile!.Document!, contentType);
                }
                return retval;
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
                var file = form.Files["File"];
                var data = form["Data"];
                var model = JsonConvert.DeserializeObject<VehicleModel>(data);
                if (model == null)
                    throw (new System.Exception("null VehicleModel received on Api"));
                else
                {
                    var value = await VehicleService.UpdateAsync(model, file);
                    retval = Ok(value);
                }
                retval = Ok(true);
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
                var value = VehicleService.Delete(guid);
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
    public class VehiclesController : ControllerBase
    {

        [HttpGet]
        public IActionResult Fetch()
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                if (user == null) throw new Exception("User not found");
                var values = VehiclesService.All(user!);
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Error on reading Vehicles list",
                    Detail = ex.Message
                });
            }
            return retval;
        }

        [HttpGet("CarModels")]
        public IActionResult CarModels()
        {
            IActionResult retval;
            try
            {
                var values = VehiclesService.CarModels();
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