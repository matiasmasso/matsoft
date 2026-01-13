using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using DocumentFormat.OpenXml.Bibliography;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EdiversaInvRpts : ControllerBase
    {

        [HttpPost("{holding}")]
        public IActionResult LastStocksFromHolding(Guid holding, [FromBody] DateTime? fch)
        {
            IActionResult retval;
            try
            {
                //var user = HttpHelper.User(Request);
                //if (user == null) throw new Exception("User unknown");
                var values = EdiversaInvRptsService.GetLastValues(holding, fch);
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost("Excel/{holding}/{fch}")]
        public IActionResult Excel(Guid holding,string fch)
        {
            IActionResult retval;
            try
            {
                var dtFch = new DateTime(
                    Convert.ToInt32(fch.Substring(0, 4)),
                    Convert.ToInt32(fch.Substring(4, 2)),
                    Convert.ToInt32(fch.Substring(6, 2))
                    );
                var cache = Api.Shared.AppState.Cache(EmpModel.EmpIds.MatiasMasso);
                var value = EdiversaInvRptsService.Excel(holding, dtFch, cache);
                var mimeType = DTO.MimeHelper.ContentType(Media.MimeCods.Xlsx);
                retval = new FileContentResult(value, mimeType);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }
    }
}
