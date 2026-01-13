using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class IbansController : ControllerBase
    {
        [HttpGet()]
        public IActionResult GetValues()
        {
            try
            { return Ok(IbansService.GetValues()); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }
    }
}