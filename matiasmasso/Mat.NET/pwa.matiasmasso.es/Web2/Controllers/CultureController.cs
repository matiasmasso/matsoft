using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Web.Services;
using DTO;

namespace Web.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("[controller]/[action]")]
    public class CultureController : Controller
    {
        private CultureService _cultureService;
        private ProductsService productsService;
        public CultureController(CultureService cultureService
            , ProductsService productsService)
        {
            _cultureService = cultureService;
            this.productsService = productsService;
        }

        //receives a periodic Get call from a Windows service in order to prevent Azure from timing out app state.
        //in future this should call lastupdatedtables to sync singleton services data
        [HttpPost()]
        public async Task<IActionResult> KeepAlivePost([FromBody] List<DirtyTableModel> values)
        {
            foreach(var item in values)
            {
                    productsService.SetDirtyIfNeeded(values);

            }
            return Ok("Ok");
        }
        public async Task<IActionResult> KeepAlive() //deprecated
        {
            await _cultureService.ReloadLocalizerStrings();
            return Ok("Ok");
        }

        public IActionResult Set(string culture, string redirectUri)
        {
            if (culture != null)
            {
                var cookieName = CookieRequestCultureProvider.DefaultCookieName;
                var oRequestCulture = new RequestCulture(culture, culture);
                var cultureProvider = CookieRequestCultureProvider.MakeCookieValue(oRequestCulture);
                var options = new CookieOptions() { 
                    SameSite = SameSiteMode.None, 
                    Secure = true,
                    Expires = new DateTimeOffset(2038, 1, 1, 0, 0, 0, TimeSpan.FromHours(0))
                };
                //options.Expires = new DateTimeOffset(2038, 1, 1, 0, 0, 0, TimeSpan.FromHours(0));
                HttpContext.Response.Cookies.Append(
                    cookieName
                    , cultureProvider
                    , options
                    );

            }
            return Redirect(redirectUri);
        }


    }
}
