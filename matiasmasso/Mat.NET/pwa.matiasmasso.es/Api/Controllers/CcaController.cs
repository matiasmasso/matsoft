using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CcaController: ControllerBase
    {

        [HttpGet("{guid}")]
        public IActionResult Fetch(Guid guid)
        {
            IActionResult retval;
            try
            {
                var lang = HttpHelper.Lang(Request);
                if (lang == null) lang = LangDTO.Default();
                var value = CcaService.Find(guid, lang);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpGet("pdf/{cca}")]
        public IActionResult Pdf(Guid cca)
        {
            IActionResult retval;
            try
            {
                var docfile = CcaService.Docfile(cca);
                return docfile is null ? new NotFoundResult() : new FileContentResult(docfile!.Document!, docfile.ContentType());
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("thumbnail/{cca}")]
        public IActionResult Thumbnail(Guid cca)
        {
            IActionResult retval;
            try
            {
                var value = CcaService.Thumbnail(cca);
                return value is null ? new NotFoundResult() : new FileContentResult(value!.Image!, value.ContentType());
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
    public class CcasController : ControllerBase
    {
        [HttpGet("{emp}/{yea}")]
        public IActionResult GetValues(int emp, int yea)
        {
            IActionResult retval;
            try
            {
                var values = CcasService.GetValues(emp,yea);
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
