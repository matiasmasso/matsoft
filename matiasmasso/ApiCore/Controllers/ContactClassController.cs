using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ContactClassesController : ControllerBase
    {

        [HttpGet()]
        public IActionResult All()
        {
            IActionResult retval;
            try
            {
                var values = ContactClassesService.GetValues();
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
