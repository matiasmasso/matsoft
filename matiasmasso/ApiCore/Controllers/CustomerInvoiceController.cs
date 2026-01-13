using Api.Entities;
using Api.Extensions;
using Api.Helpers;
using Api.Services;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Runtime.Versioning;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerInvoiceController : ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult GetValue(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = CustomerInvoiceService.GetValue(guid);
                retval = Ok(value);
            }
            catch (System.Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost("Pdf")]
        public IActionResult Pdf([FromBody] CustomerInvoiceModel args)
        {
            try
            {
                Media value = CustomerInvoiceService.Media(args);
                return value?.Data == null ? new NotFoundResult() : new FileContentResult(value.Data, value.ContentType());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ProblemDetails());
            }
        }

        [HttpGet("SetCsv/{guid}/{csv}")]
        public IActionResult SetCsv(Guid guid, string csv)
        {
            IActionResult retval;
            try
            {
                var value = CustomerInvoiceService.SetCsv(guid, csv);
                retval = Ok(value);
            }
            catch (System.Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost("Update"), SupportedOSPlatform("windows")]
        public async Task<IActionResult> UpdateAsync()
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                if (user == null) throw new System.Exception("missing user permissions");

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
                    retval = BadRequest(new System.Exception("null model"));

            }
            catch (System.Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost("RebuildPdf"), SupportedOSPlatform("windows")]
        public async Task<IActionResult> RebuildPdf()
        {
            IActionResult retval;
            try
            {
                var form = await Request.ReadFormAsync();
                var data = form["Data"];
                var model = JsonConvert.DeserializeObject<CustomerInvoiceModel>(data);
                if (model != null)
                {
                    if (form.Files.Count > 0)
                        await FileUploadHelper.LoadFileStream(model.Docfile, form.Files);

                    CustomerInvoiceService.RebuildPdf(model);
                    retval = Ok(true);
                }
                else
                    retval = BadRequest(new System.Exception("null model"));

            }
            catch (System.Exception ex)
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
                CustomerInvoiceService.Delete(guid);
                retval = Ok(true);
            }
            catch (System.Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }
    }



    [ApiController]
    [Route("[controller]")]
    public class CustomerInvoicesController : ControllerBase
    {

        [HttpGet("{emp}")]
        public IActionResult GetValues(EmpModel.EmpIds emp)
        {
            IActionResult retval;
            try
            {
                var value = CustomerInvoicesService.GetValues(emp);
                retval = Ok(value);
            }
            catch (System.Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

    }
}
