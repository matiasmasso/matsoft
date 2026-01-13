using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CorrespondenciaController : ControllerBase
    {
        [HttpGet("pdf/{guid}")]
        public IActionResult Pdf(Guid guid)
        {
            IActionResult retval;
            try
            {
                var docfile = CorrespondenciaService.Docfile(guid);
                string contentType = MimeHelper.ContentType((MimeHelper.MimeCods)docfile!.StreamMime!);
                retval = new FileContentResult(docfile!.Document!, contentType);
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
    public class ContactCorrespondenciaController : ControllerBase
    {

        [HttpGet("{contact}")]
        public IActionResult FetchList(Guid contact)
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                if (user == null) throw new Exception("User unknown");
                var lang = HttpHelper.Lang(Request);
                var values = ContactCorrespondenciaService.Fetch(contact);
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