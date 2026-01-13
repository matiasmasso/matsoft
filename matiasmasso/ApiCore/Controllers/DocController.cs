using Api.Extensions;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using DTO;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DocController : ControllerBase
    {
        [HttpGet("{asin}")]
        public IActionResult Media(string asin)
        {
            IActionResult retval;
            try
            {
                var docfile = DocService.Media(asin);
                if (docfile?.Document?.Data == null)
                    return new NotFoundResult();
                else
                    return new FileContentResult(docfile.Document.Data, docfile.Document.ContentType());
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

 
        [HttpGet("thumbnail/{asin}")]
        public IActionResult Thumbnail(string asin)
        {
            IActionResult retval;
            try
            {
                var byteArray = DocService.Thumbnail(asin);
                if (byteArray == null)
                    retval = new NotFoundResult();
                else
                {
                    string contentType = MimeHelper.ContentType(DTO.Media.MimeCods.Jpg);
                    retval = new FileContentResult(byteArray, contentType);
                }
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

    }
}
