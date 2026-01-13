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

        [HttpPost("Find")]
        public IActionResult Find([FromBody] string hash)
        {
            IActionResult retval;
            try
            {
                var value = DocfileService.GetValue(hash);
                return Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("{hash}")]
        public IActionResult Media(string hash)
        {
            IActionResult retval;
            try
            {
                var docfile = DocfileService.Media(hash);
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

        [HttpPost]
        public IActionResult MediaPost([FromBody]string hash)
        {
            IActionResult retval;
            try
            {
                var docfile = DocfileService.Media(hash);
                return docfile?.Document?.Data == null ? Ok(null) : new FileContentResult(docfile.Document.Data, docfile.Document.ContentType());
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



