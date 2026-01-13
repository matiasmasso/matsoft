using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using DTO.Helpers;
using Shop4moms.Services;

using DTO;
using Shop4moms.Services;
using Microsoft.AspNetCore.OutputCaching;

namespace Shop4moms.Controllers
{
    //[Route("[controller]/[action]")]

    public class MediaResourceController : Controller
    {
        private readonly HttpClient _http;
        private IOutputCacheStore _cache;


        public MediaResourceController(HttpClient http, IOutputCacheStore cache)
        {
            _http = http;
            _cache = cache;
        }


        [Route("[controller]/[action]/{id}")]
        [OutputCache(PolicyName = "MediaResources")]
        //used to display a high res image on its landing page, not for downloading since this uses JS blob
        public async Task<IActionResult> DownloadAsync(string id)
        {
            IActionResult retval;
            try
            {
                var apiResponse = await HttpService.GetAsync<MediaResourceModel>(_http, "MediaResource", id);
                if (apiResponse.Success())
                {
                    var mediaResource = apiResponse.Value!;
                    var serverPath = mediaResource.ServerPath();
                    var value = new ImageMime
                    {
                        Image = System.IO.File.ReadAllBytes(serverPath),
                        Mime = mediaResource.Mime
                    };
                    retval = new FileContentResult(value.Image, value.ContentType());
                }
                else
                    throw new Exception();
            }
            catch (Exception)
            {
                var value = ImageMime.Default(Media.MimeCods.Jpg);
                retval = new FileContentResult(value.Image, value.ContentType());
            }
            return retval;
        }




    }
}
