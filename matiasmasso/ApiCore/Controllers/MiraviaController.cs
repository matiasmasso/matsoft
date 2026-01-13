using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Newtonsoft.Json;
using System.Runtime.Versioning;
using Api.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MiraviaController : ControllerBase
    {

        [HttpPost("saveOrder"), SupportedOSPlatform("windows")]
        public async Task<IActionResult> SaveOrderAsync()
        {
            IActionResult retval;
            try
            {
                var form = await Request.ReadFormAsync();
                var data = form["Data"];
                var model = JsonConvert.DeserializeObject<DTO.Integracions.Miravia.Order> (data!);
                if (model != null)
                {
                    var files = form.Files;
                    
                    foreach (var docfile in model.ShipmentLabels)
                    {
                        await FileUploadHelper.LoadFileStream(docfile, files);
                    }
                    var ticket = Services.Integracions.Miravia.OrderFulfillment.Register(model);
                    retval = Ok(ticket);
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


        [HttpPost("tickets/fromOrderNumbers")]
        public IActionResult TicketsFromOrderNumbers([FromBody] List<string> orderNumbers)
        {
            IActionResult retval;
            try
            {
                var marketplace = MarketPlaceModel.Wellknown(MarketPlaceModel.Wellknowns.Miravia)!;
                var value = Services.ConsumerTicketsService.FromOrderNumbers(marketplace.Guid, orderNumbers);
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