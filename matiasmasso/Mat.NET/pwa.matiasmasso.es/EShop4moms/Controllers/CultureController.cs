
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Shop4moms.Controllers
{
    [Route("[controller]/[action]")]
    public class CultureController : Controller
    {
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
            return LocalRedirect(redirectUri);
        }
    }
}
