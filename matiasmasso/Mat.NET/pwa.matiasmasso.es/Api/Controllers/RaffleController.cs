using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Shared;
using System;
using Api.Extensions;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RaffleController : ControllerBase
    {
        private readonly ILogger<RaffleController> _logger;
        public RaffleController(ILogger<RaffleController> logger)
        {
            _logger = logger;
        }


        [HttpGet("{guid}")]
        public IActionResult Fetch(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = RaffleService.Find(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("player/{raffle}")]
        public IActionResult Player(Guid raffle)
        {
            IActionResult retval;
            try
            {
                var user = HttpHelper.User(Request);
                var value = RaffleService.Player(raffle, user);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpPost("participant")]
        public IActionResult UpdateParticipant([FromBody] RaffleModel.Participant participant)
        {
            IActionResult retval;
            try
            {
                var value = RaffleService.UpdateParticipant(participant);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpGet("thumbnail/{guid}.jpg")]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 2592000)] //1 mes = 60*60*24*30
        public IActionResult Thumbnail(Guid guid)
        {
            IActionResult retval;
            try
            {
                string mimeType = "image/jpeg";
                byte[]? value = Shared.Cache.GetImg(guid, Shared.Cache.Img.Cods.RaffleThumbnail);
                if (value == null)
                {
                    value = RaffleService.Thumbnail(guid);
                    if (value != null)
                        Shared.Cache.SetImg(value, guid, Shared.Cache.Img.Cods.RaffleThumbnail);
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



        [HttpGet("ImgBanner600/{guid}.jpg")]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 2592000)] //1 mes = 60*60*24*30
        public IActionResult ImgBanner600(Guid guid)
        {
            IActionResult retval;
            try
            {
                string mimeType = "image/jpeg";
                byte[]? value = Shared.Cache.GetImg(guid, Shared.Cache.Img.Cods.RaffleImgBanner600);
                if (value == null)
                {
                    value = RaffleService.ImgBanner600(guid);
                    if (value != null)
                        Shared.Cache.SetImg(value, guid, Shared.Cache.Img.Cods.RaffleImgBanner600);
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


        [HttpGet("winner/image/{guid}.jpg")]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 2592000)] //1 mes = 60*60*24*30
        public IActionResult WinnerImg(Guid guid)
        {
            IActionResult retval;
            try
            {
                string mimeType = "image/jpeg";
                byte[]? value = Shared.Cache.GetImg(guid, Shared.Cache.Img.Cods.RaffleWinnerImg);
                if (value == null)
                {
                    value = RaffleService.RaffleWinnerImg(guid);
                    if (value != null)
                        Shared.Cache.SetImg(value, guid, Shared.Cache.Img.Cods.RaffleWinnerImg);
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
    public class RafflesController : ControllerBase
    {
        private readonly ILogger<RafflesController> _logger;
        public RafflesController(ILogger<RafflesController> logger)
        {
            _logger = logger;
        }



        [HttpGet]
        public IActionResult All()
        {
            IActionResult retval;
            try
            {
                var lang = HttpHelper.Lang(Request);
                var values = RafflesService.All(lang);
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