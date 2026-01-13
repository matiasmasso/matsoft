using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressesController:ControllerBase
    {

        [HttpGet]
        public IActionResult GetValues()
        {
            try { 
                return Ok(AddressesService.GetValues()); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }
    }
}
