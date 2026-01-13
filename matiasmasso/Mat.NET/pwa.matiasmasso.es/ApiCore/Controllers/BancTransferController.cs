using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BancTransfersController : ControllerBase
    {
        [HttpGet()]
        public IActionResult GetValues()
        {
            try
            { return Ok(BancTransfersService.GetValues()); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }

        [HttpPost()]
        public IActionResult Update([FromBody] List<BancModel.Transfer> values)
        {
            try
            {
                BancTransfersService.Update(values);
                return Ok(true); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }
    }
}