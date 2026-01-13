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
    public class AeatController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult GetValue(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = AeatService.GetValue(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("item/{guid}")]
        public IActionResult GetItem(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = AeatService.GetItem(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpPost()]
        public IActionResult Update([FromBody] AeatModel model)
        {
            IActionResult retval;
            try
            {
                var value = AeatService.Update(model);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpPost("UpdateItem"), SupportedOSPlatform("windows")]
        public async Task<IActionResult> UpdateAsync()
        {
            IActionResult retval;
            try
            {
                var form = await Request.ReadFormAsync();
                var data = form["Data"];
                var model = JsonConvert.DeserializeObject<AeatModel.Item>(data);
                if (model != null)
                {
                    if (form.Files.Count > 0)
                    {
                        await FileUploadHelper.LoadFileStream(model.Docfile, form.Files);
                    }

                    AeatService.Update(model);
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
                var value = AeatService.Delete(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("deleteItem/{guid}")]
        public IActionResult DeleteItem(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = AeatService.DeleteItem(guid);
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
    public class AeatsController : ControllerBase
    {
        [HttpGet()]
        public IActionResult GetValues()
        {
            try
            { return Ok(AeatsService.GetValues()); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }

        [HttpGet("withItems")]
        public IActionResult GetValuesWithItems()
        {
            try
            { return Ok(AeatsService.GetValuesWithItems()); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }
    }
}