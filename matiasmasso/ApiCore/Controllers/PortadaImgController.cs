
using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using System.IO.Compression;
using Newtonsoft.Json;
using System;
using Api.Extensions;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [OutputCache(PolicyName = "PortadaImgs")]

    public class PortadaImgController : ControllerBase
    {

        IOutputCacheStore cache;

        public PortadaImgController(IOutputCacheStore cache)
        {
            this.cache = cache;
        }



        [HttpGet("img/{id}")]
        public IActionResult Img(string id)
        {
            IActionResult retval;
            try
            {
                ImageMime? value = PortadaImgService.ImageMime(id);
                retval = new FileContentResult(value.Image, value.ContentType());
                return retval;
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync()
        {
            IActionResult retval;
            try
            {
                var form = await Request.ReadFormAsync();
                var file = form.Files["File"];
                var data = form["Data"];
                var model = JsonConvert.DeserializeObject<PortadaImgModel>(data);
                if (model != null)
                {
                    if (await PortadaImgService.UpdateAsync(model, file))
                        await cache.Clear(OutputCacheExtensions.Tags.Baskets);
                }
                retval = Ok(true);
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
    [OutputCache(PolicyName = "PortadaImgs")]
    public class PortadaImgsController : ControllerBase
    {

        [HttpGet]
        public IActionResult All()
        {
            IActionResult retval;
            try
            {
                var lang = HttpHelper.Lang(Request);
                var values = PortadaImgsService.All(lang);
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

