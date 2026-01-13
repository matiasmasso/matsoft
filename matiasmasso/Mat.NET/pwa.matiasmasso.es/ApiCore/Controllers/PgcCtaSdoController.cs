using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PgcCtaSdosController : ControllerBase
    {

        [HttpGet("{emp}/{year}/{mes?}")]
        public IActionResult Fetch(int emp, int year, int? mes = null)
        {
            IActionResult retval;
            try
            {
                var value = PgcCtaSdosService.GetValues(emp, year, mes);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("{contact}")]
        public IActionResult Fetch(Guid contact)
        {
            IActionResult retval;
            try
            {
                var lang = HttpHelper.Lang(Request) ?? LangDTO.Default();
                var value = PgcCtaSdosService.Fetch(contact, lang);
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
