using Components.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Shop4moms.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("[controller]/[action]")]
    public class CultureController : Controller
    {
        private CultureService _cultureService;
        public CultureController(CultureService cultureService)
        {
            _cultureService = cultureService;
        }

        public IActionResult Set(string culture, string redirectUri)
        {
            if (culture != null)
            {
                var cookieName = CookieRequestCultureProvider.DefaultCookieName;
                var oRequestCulture = new RequestCulture(culture, culture);
                var cultureProvider = CookieRequestCultureProvider.MakeCookieValue(oRequestCulture);
                var options = new CookieOptions();
                options.Expires = new DateTimeOffset(2038, 1, 1, 0, 0, 0, TimeSpan.FromHours(0));
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
