using Api.Extensions;
using Api.Helpers;
using Api.Services;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Runtime.Versioning;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FraController : ControllerBase
    {


        [HttpPost("Update"), SupportedOSPlatform("windows")]
        public async Task<IActionResult> UpdateAsync()
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                if (user == null) throw new Exception("missing user permissions");

                var form = await Request.ReadFormAsync();
                var data = form["Data"];
                var model = JsonConvert.DeserializeObject<CustomerInvoiceModel>(data!);
                if (model != null)
                {
                    if (form.Files.Count > 0)
                    {
                        await FileUploadHelper.LoadFileStream(model.Docfile, form.Files);
                    }

                    CustomerInvoiceService.Update(model);
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
    public class FrasController : ControllerBase
    {
        [HttpGet("{emp}")]
        public IActionResult GetValues(EmpModel.EmpIds emp)
        {
            IActionResult retval;
            try
            {
                var values = FrasService.GetValues(emp);
                retval = Ok(values);
            }
            catch (System.Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("LastFraNums")]
        public IActionResult LastFraNums()
        {
            try
            { return Ok(FrasService.LastFraNums()); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }



    }
}