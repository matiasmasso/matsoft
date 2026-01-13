using DTO;
using Microsoft.AspNetCore.Components;


namespace Shop4moms.Pages
{
    public class _ComponentBase : ComponentBase
    {
        [Inject] public IHttpContextAccessor? HttpContextAccessor { get; set; }
        [Inject] public AppState? AppState { get; set; }
        protected LangDTO Lang()
        {
            var cookie = HttpContextAccessor?.HttpContext?.Request.Cookies[Microsoft.AspNetCore.Localization.CookieRequestCultureProvider.DefaultCookieName];
            var cc = cookie == null ? "es" : cookie?.Split("|").First().Split("=")[1] ?? "es";
            var retval = LangDTO.FromCultureInfo(cc);
            return retval;
        }

        protected DTO.Integracions.Redsys.Tpv Tpv() => AppState!.Tpv;
    }
}
