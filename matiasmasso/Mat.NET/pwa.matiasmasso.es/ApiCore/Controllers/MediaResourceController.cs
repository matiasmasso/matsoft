using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Api.Entities;
using Exception = System.Exception;
using Microsoft.AspNetCore.OutputCaching;
using System;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [OutputCache(PolicyName = "MediaResources")]
    public class MediaResourceController : ControllerBase
    {

        [HttpGet("{segment}")]
        public IActionResult Fetch(string segment)
        {
            try { 
                if(segment.IsGuid())
                    return Ok(MediaResourceService.GetValue(new Guid(segment))); 
                else
                    return Ok(MediaResourceService.FromFilename(segment));
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }



        [HttpGet("thumbnail/{filenameOrGuid}")]
        public IActionResult Thumbnail(string filenameOrGuid)
        {
            try
            {
                var value = MediaResourceService.Thumbnail(filenameOrGuid) ?? ImageMime.Default(Media.MimeCods.Jpg);
                return new FileContentResult(value.Image, value.ContentType());
            }
            catch (System.Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }

    }

    [ApiController]
    [Route("[controller]")]
    [OutputCache(PolicyName = "MediaResources")]
    public class MediaResourcesController : ControllerBase
    {
        [HttpGet()]
        public IActionResult GetValues()
        {
            try { return Ok(MediaResourcesService.GetValues()); }
            catch (System.Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }

    }
}
