
using Microsoft.AspNetCore.Mvc;
using Web.Services;

namespace Web.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("[controller]/[action]")]
    public class KeepAliveController : Controller
    {
        private NoticiasService noticiasService;
        private ProductsService productsService;
        public KeepAliveController(NoticiasService noticiasService
            , ProductsService productsService)
        {
            this.noticiasService = noticiasService;
            this.productsService = productsService; 
        }

        public async Task<IActionResult> Get()
        {
            await noticiasService.FetchAsync();
            return Ok( "Ok");
        }
    }
}

