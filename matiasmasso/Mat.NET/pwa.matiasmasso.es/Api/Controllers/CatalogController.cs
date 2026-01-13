using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController: ControllerBase
    {
        [HttpPost]
        public IActionResult Model([FromBody] CatalogModel request)
        {
            IActionResult retval;
            try
            {
                var value = CatalogService.Model(request);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Error on reading the catalogue",
                    Detail = ex.Message
                });
            }
            return retval;
        }
    }
}
