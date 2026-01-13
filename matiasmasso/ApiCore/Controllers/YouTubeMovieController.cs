using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Api.Entities;
using Exception = System.Exception;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class YouTubeMovieController : ControllerBase
    {

        IOutputCacheStore cache;

        public YouTubeMovieController(IOutputCacheStore cache)
        {
            this.cache = cache;
        }

        [HttpPost("fromSegment")]
        [OutputCache(PolicyName = "Videos")]
        public IActionResult Fetch([FromBody] string segment)
        {
            try { return Ok(YouTubeMovieService.FromSegment(segment)); }
            catch (System.Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }


        [HttpGet("thumbnail/{guid}")]
        [OutputCache(PolicyName = "Videos")]
        public IActionResult Thumbnail(Guid guid)
        {
            try
            {
                var value = YouTubeMovieService.Thumbnail(guid) ?? ImageMime.Default(Media.MimeCods.Jpg);
                return new FileContentResult(value.Image, value.ContentType());
            }
            catch (System.Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }
    }



    [ApiController]
    [Route("[controller]")]
    public class YouTubeMoviesController : ControllerBase
    {
        IOutputCacheStore cache;

        public YouTubeMoviesController(IOutputCacheStore cache)
        {
            this.cache = cache;
        }

        [HttpGet()]
        [OutputCache(PolicyName = "Videos")]
        public IActionResult Fetch()
        {
            IActionResult retval;
            try
            {
                var values = YouTubeMoviesService.GetValues();
                retval = Ok(values);
            }
            catch (System.Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }
    }
}
