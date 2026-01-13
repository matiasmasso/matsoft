using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [OutputCache(PolicyName = "Noticias")]
    public class NoticiaController : ControllerBase
    {
        IOutputCacheStore cache;

        public NoticiaController(IOutputCacheStore cache)
        {
            this.cache = cache;
        }


        //[HttpPost("LandingPage")]
        //public async Task<IActionResult> LandingPage([FromBody]string urlSegment)
        //{
        //    IActionResult retval;
        //    try
        //    {
        //        var value = NoticiaService.LandingPage(urlSegment);
        //        await cache.Clear(OutputCacheExtensions.Tags.Noticias);
        //        retval = Ok(value);
        //    }
        //    catch (Exception ex)
        //    {
        //        retval = BadRequest(ex.ProblemDetails());
        //    }
        //    return retval;
        //}


        [HttpGet("thumbnail/{guid}")]
        public IActionResult Thumbnail(Guid guid)
        {
            IActionResult retval;
            try
            {
                string mimeType = "image/jpeg";
                byte[]? value = NoticiaService.Thumbnail(guid);
                retval = new FileContentResult(value, mimeType);
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
    [OutputCache(PolicyName = "Noticias")]
    public class NoticiasController : ControllerBase
    {

        [HttpGet("")]
        public IActionResult Noticias()
        {
            IActionResult retval;
            try
            {
                UserModel? user = HttpHelper.User(Request);
                LangDTO lang = HttpHelper.Lang(Request) ?? LangDTO.Esp();
                var values = NoticiasService.All(user, lang);
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