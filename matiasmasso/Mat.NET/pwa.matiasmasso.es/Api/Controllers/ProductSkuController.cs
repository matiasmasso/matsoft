using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductSkuController : ControllerBase
    {
        [HttpGet("{guid}")]
        public IActionResult Find(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = ProductSkuService.Find(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpPost]
        public IActionResult Update([FromBody] ProductSkuModel model)
        {
            IActionResult retval;
            try
            {
                var value = ProductSkuService.Update(model);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("delete/{guid}")]
        public IActionResult Delete(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = ProductSkuService.Delete(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }




        [HttpGet("Image/{guid}.jpg")]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 2592000)] //1 mes = 60*60*24*30
        public IActionResult Thumbnail(Guid guid)
        {
            IActionResult retval;
            try
            {
                string mimeType = "image/jpeg";
                byte[]? value = Shared.Cache.GetImg(guid, Shared.Cache.Img.Cods.SkuImage);
                if (value == null)
                {
                    value = ProductSkuService.Image(guid);
                    if (value != null)
                        Shared.Cache.SetImg(value, guid, Shared.Cache.Img.Cods.SkuImage);
                    else
                        value = new byte[] { };
                }
                retval = new FileContentResult(value, mimeType);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpGet("Thumbnail/{guid}.jpg")]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 2592000)] //1 mes = 60*60*24*30
        public IActionResult Image(Guid guid)
        {
            IActionResult retval;
            try
            {
                string mimeType = "image/jpeg";
                byte[]? value = Shared.Cache.GetImg(guid, Shared.Cache.Img.Cods.SkuThumbnail);
                if (value == null)
                {
                    value = ProductSkuService.Thumbnail(guid);
                    if (value != null)
                        Shared.Cache.SetImg(value, guid, Shared.Cache.Img.Cods.SkuThumbnail);
                    else
                        value = new byte[] { };
                }
                retval = new FileContentResult(value, mimeType);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


    }
}
