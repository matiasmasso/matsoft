using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContractController : ControllerBase
    {
        [HttpGet("pdf/{guid}")]
        public async Task<IActionResult> Pdf(Guid guid)
        {
            IActionResult retval;
            try
            {
                byte[] byteArr = ContractService.Stream(guid);
                //if(byteArr == null) throw new ArgumentNullException("No s'ha trobat el pdf");
                string mimeType = "application/pdf";
                retval = new FileContentResult(byteArr, mimeType);
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
    public class ContractsController : ControllerBase
    {

        [HttpGet]
        public IActionResult Fetch()
        {
            IActionResult retval;
            try
            {
                var values = ContractsService.Fetch();
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("fromCodi/{guid}")]
        public IActionResult FromCodi(Guid guid)
        {
            IActionResult retval;
            try
            {
                var values = ContractsService.FromCodi(guid);
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