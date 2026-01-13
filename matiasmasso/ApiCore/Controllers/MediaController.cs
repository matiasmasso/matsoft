using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using System.Drawing.Imaging;
using System.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Runtime.Versioning;
using Api.Extensions;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [SupportedOSPlatform("windows")]
    public class MediaController : ControllerBase
    {

        [HttpPost("resize/{mime}/{width}/{height}")]
        public IActionResult Image(Media.MimeCods mime, int width, int height, [FromForm] IFormFile file)
        {
            IActionResult retval = new EmptyResult();
            try
            {
                var image = System.Drawing.Image.FromStream(file.OpenReadStream());
                //var resized = new Bitmap(image, new System.Drawing.Size(width, height));
                var resized = DTO.Helpers.ImageHelper.Resize(image,width,height);
                using var imageStream = new MemoryStream();
                resized.Save(imageStream, DTO.Helpers.ImageHelper.ImageFormat(mime));
                var media = new Media
                {
                    Data = imageStream.ToArray(),
                    Mime = mime
                };
                    retval = new FileContentResult(media.Data, media.ContentType());

                return retval;
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

    }

}
