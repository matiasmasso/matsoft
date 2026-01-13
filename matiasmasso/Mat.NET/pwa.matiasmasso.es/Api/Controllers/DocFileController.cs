using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocfileController : ControllerBase
    {

        [HttpGet("{hash}")]
        public IActionResult Media(string hash)
        {
            IActionResult retval;
            try
            {
                var docfile = DocfileService.Media(hash);
                string contentType = MimeHelper.ContentType((MimeHelper.MimeCods)docfile!.StreamMime!);
                retval = new FileContentResult(docfile!.Document!, contentType);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("thumbnail/{base64Hash}")]
        public IActionResult Thumbnail(string base64Hash)
        {
            IActionResult retval;
            try
            {
                var byteArray = DocfileService.Thumbnail(base64Hash);
                if (byteArray == null)
                    retval = new NotFoundResult();
                else
                {
                    string contentType = MimeHelper.ContentType(MimeHelper.MimeCods.Jpg);
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

    [ApiController]
    [Route("[controller]")]
    public class DocfilesController : ControllerBase
    {

        [HttpGet("FromSrc/{srcGuid}")]
        public IActionResult FromSrc(Guid srcGuid)
        {
            IActionResult retval;
            try
            {
                var values = DocfilesService.FromSrc(srcGuid);
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("FromTarget/{target}")]
        public IActionResult FromTarget(Guid target)
        {
            IActionResult retval;
            try
            {
                var values = DocfilesService.FromTarget(target);
                retval = Ok(values);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("{target}")]
        public IActionResult Fetch(Guid target)
        {
            IActionResult retval;
            try
            {
                //var user = HttpHelper.User(Request);
                //if (user == null) throw new Exception("User not found");
                var values = DocfilesService.Fetch(target);
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



