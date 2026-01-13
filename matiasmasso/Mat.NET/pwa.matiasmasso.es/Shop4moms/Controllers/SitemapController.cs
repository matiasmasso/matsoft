

using Shop4moms.Services;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using DTO.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Shop4moms.Services;
using System.Linq;
using System.Xml.Linq;

namespace Shop4moms.Controllers
{
    public class SitemapController : Controller
    {
        private HttpClient http;

        public SitemapController(HttpClient http)
        {
            this.http = http;
        }

        [Microsoft.AspNetCore.Mvc.Route("[controller].xml")]
        public IActionResult Index()
        {
            var host = HttpContext.Request.Host.Host;
            var document = SitemapModel.Index(host,SitemapService.SitemapPaths());
            var value = document.Serialize();
            return Ok(value);
        }
    }



    [Microsoft.AspNetCore.Mvc.Route("[controller]/[action].xml")]
    public class SitemapsController : Controller
    {
        private HttpClient http;

        public SitemapsController(HttpClient http)
        {
            this.http = http;
        }


        public IActionResult Pages()
        {
            var host = HttpContext.Request.Host.Host;
            var document = SitemapModel.Pages(host, SitemapService.Pages());
            var value = document.Serialize();
            return Ok(value);
        }

        public async Task<IActionResult> CategoriesAsync()
        {
            var host = HttpContext.Request.Host.Host;
            var apiResponseCategories = await HttpService.GetAsync<List<ProductCategoryModel>>(http, "Shop4moms/categories");
            var apiResponseRoutes = await HttpService.GetAsync<List<RouteModel>>(http, "Shop4moms/routes");
            var categories = apiResponseCategories.Value?.Where(x => x.Codi <= (int)ProductCategoryModel.Codis.accessories).ToList() ?? new();
            var routes = new RouteModel.Collection(apiResponseRoutes.Value?.Where(x => x.Src == RouteModel.Srcs.Category).ToList() ?? new());
            var document = SitemapModel.Categories(host, categories, routes);
            var value = document.Serialize();
            return Ok(value);
        }

        public async Task<IActionResult> SkusAsync()
        {
            var host = HttpContext.Request.Host.Host;
            var apiResponseCategories = await HttpService.GetAsync<List<ProductCategoryModel>>(http, "Shop4moms/categories");
            var apiResponseSkus = await HttpService.GetAsync<List<ProductSkuModel>>(http, "Shop4moms/skus");
            var apiResponseRoutes = await HttpService.GetAsync<RouteModel.Collection>(http, "Shop4moms/routes");
            var categories = apiResponseCategories.Value?.Where(x => x.Codi <= (int)ProductCategoryModel.Codis.accessories).ToList() ?? new();
            var skus = apiResponseSkus.Value?.Where(x => categories.Any(y => y.Guid == x.Category)).ToList() ?? new();
            var routes = new RouteModel.Collection(apiResponseRoutes.Value?.Where(x => x.Src == RouteModel.Srcs.Sku).ToList() ?? new());
            var document = SitemapModel.Skus(host, skus, routes);
            var value = document.Serialize();
            return Ok(value);
        }
        public async Task<IActionResult> VideosAsync()
        {
            var host = HttpContext.Request.Host.Host;
            var apiResponse = await HttpService.GetAsync<List<YouTubeMovieModel>>(http, "Shop4moms/Videos");
            var items = apiResponse.Value;
            var document = SitemapModel.Videos(host, items);
            var value = document.Serialize();
            return Ok(value);
        }
        public async Task<IActionResult> ImagesAsync()
        {
            var host = HttpContext.Request.Host.Host;
            var apiResponse = await HttpService.GetAsync<List<MediaResourceModel>>(http, "Shop4moms/MediaResources");
            var items = apiResponse.Value;
            var document = SitemapModel.Images(host, items);
            var value = document.Serialize();
            return Ok(value);
        }
    }
}
