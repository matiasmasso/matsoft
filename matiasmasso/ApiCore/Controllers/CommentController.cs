using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [OutputCache(PolicyName = "Comments")]
    public class CommentController : ControllerBase
    {
        IOutputCacheStore cache;

        public CommentController(IOutputCacheStore cache)
        {
            this.cache = cache;
        }


        [HttpGet("{guid}")]
        public IActionResult GetValue(Guid guid)
        {
            try
            { return Ok(CommentService.GetValue(guid)); }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] ContentModel.Comment value)
        {
            try
            {
                CommentService.Update(value);
                await cache.Clear(OutputCacheExtensions.Tags.Comments);
                return Ok(true);
            }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }


        [HttpGet("delete/{guid}")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            try
            {
                CommentService.Delete(guid);
                await cache.Clear(OutputCacheExtensions.Tags.Comments);
                return Ok(true);
            }
            catch (Exception ex)
            { return BadRequest(ex.ProblemDetails()); }
        }

    }







    [ApiController]
    [Route("[controller]")]
    [OutputCache(PolicyName = "Comments")]
    public class CommentsController : ControllerBase
    {
        IOutputCacheStore cache;

        public CommentsController(IOutputCacheStore cache)
        {
            this.cache = cache;
        }


        /// <summary>
        /// All comments from a user, regardless if they have been answered or even deleted
        /// </summary>
        /// <param name="user">the user who posted the comment</param>
        /// <returns>List of Comment objects</returns>
        [HttpGet("fromUser/{user}")]
        public IActionResult FromUser(Guid user)
        {
            try { return Ok(CommentsService.FromUser(user)); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }

        /// <summary>
        /// Comments from a source which are not answered yet
        /// </summary>
        /// <param name="src">source where the comment was posted</param>
        /// <returns>List of Comment objects</returns>
        [HttpGet("openfromSrc/{src}")]
        public IActionResult OpenFromSrc(int src)
        {
            try { return Ok(CommentsService.OpenFromSrc(src)); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }

        /// <summary>
        /// All comments from a source, regardless if they have been answered or even deleted
        /// </summary>
        /// <param name="src">source where the comment was posted</param>
        /// <returns>List of Comment objects</returns>
        [HttpGet("fromSrc/{src}")]
        public IActionResult FromSrc(int src)
        {
            try { return Ok(CommentsService.FromSrc(src)); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }

        /// <summary>
        /// All comments from a conversation thread
        /// </summary>
        /// <param name="answering">guid of first comment on thread</param>
        /// <returns>List of Comment objects</returns>
        [HttpGet("thread/{answering}")]
        public IActionResult Thread(Guid answering)
        {
            try { return Ok(CommentsService.Thread(answering)); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }

    }

}