Create a .Net7.0 Class Library and name it "Translations" (Do not name it "Resources"!)
Add a public empty class Translation in the root (no Translations 's')
Add a Folder called Resources in the Translations projec
Add resource files Translation.resx, Translation.es.resx, etc to Resources folder

In the client project:
Install Nuget Package Microsoft.Extensions.Localization

In the client project Program.cs:
Change the config to builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

after app.UseRouting and before app.MapControllers, add:
var supportedCultures = new[] { "es", "ca", "en", "pt" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

In _Imports.razor
Add @using Translations

In Index.razor:
@inject IStringLocalizer<Translation> Localizer
<h1>@Localizer["HelloWorld"]</h1>

In client project:
Add a Controllers folder
Add next controller:

using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace [project name].Controllers
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
