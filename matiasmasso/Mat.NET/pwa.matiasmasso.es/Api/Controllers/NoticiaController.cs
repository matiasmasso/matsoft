using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoticiaController : ControllerBase
    {
         private readonly ILogger<WeatherForecastController> _logger;

        public NoticiaController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }


        [HttpPost("LandingPage")]
        public IActionResult LandingPage([FromBody]string urlSegment)
        {
            IActionResult retval;
            try
            {
                var value = NoticiaService.LandingPage(urlSegment);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("thumbnail/{guid}")]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 2592000)] //1 mes = 60*60*24*30
        public IActionResult Thumbnail(Guid guid)
        {
            IActionResult retval;
            try
            {
                string mimeType = "image/jpeg";
                byte[]? value = Shared.Cache.GetImg(guid);
                if (value == null)
                {
                    value = NoticiaService.Thumbnail(guid);
                    if (value != null)
                        Shared.Cache.SetImg(value, guid);
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


    [ApiController]
    [Route("[controller]")]
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