using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Shared;
using System;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [OutputCache(PolicyName = "Raffles")]
    public class RaffleController : ControllerBase
    {
        IOutputCacheStore cache;

        public RaffleController(IOutputCacheStore cache)
        {
            this.cache = cache;
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
        public IActionResult Thumbnail(Guid guid)
        {
            IActionResult retval;
            try
            {
                string mimeType = "image/jpeg";
                byte[]? value = RaffleService.Thumbnail(guid);
                 retval = new FileContentResult(value, mimeType);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }



        [HttpGet("ImgBanner600/{guid}.jpg")]
        public IActionResult ImgBanner600(Guid guid)
        {
            IActionResult retval;
            try
            {
                string mimeType = "image/jpeg";
                byte[]? value = RaffleService.ImgBanner600(guid);
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
                byte[]? value = RaffleService.RaffleWinnerImg(guid);
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
    [OutputCache(PolicyName = "Raffles")]
    public class RafflesController : ControllerBase
    {

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