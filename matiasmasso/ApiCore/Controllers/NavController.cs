using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NavController : ControllerBase
    {

        [HttpPost]
        public IActionResult Update([FromBody] NavDTO.MenuItem value)
        {
            try
            {
                return Ok(NavService.Update(value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ProblemDetails());
            }
        }

        [HttpGet("delete/{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                return Ok(NavService.Delete(guid));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ProblemDetails());
            }
        }

    }




    [ApiController]
    [Route("[controller]")]
    public class NavsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetValues()
        {
            IActionResult retval;
            try
            {
                var value = NavsService.GetValues();
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpPost("UpdateSortOrder")]
        public async Task<IActionResult> UpdateSortOrder([FromBody] List<NavDTO.MenuItem> nav)
        {
            IActionResult retval;
            try
            {
                var value = await NavsService.UpdateSortOrder(nav);
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
