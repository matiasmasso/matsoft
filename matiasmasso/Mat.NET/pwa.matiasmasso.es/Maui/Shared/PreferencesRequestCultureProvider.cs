using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.Shared
{
    public class PreferencesRequestCultureProvider: IRequestCultureProvider
    {
        //private readonly CultureInfo[] _cultures;

        //public PreferencesRequestCultureProvider(CultureInfo[] cultures)
        //{
        //    _cultures = cultures;
        //}

        public Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var cc = Preferences.Get("blazorCulture", "es-ES");
            return Task.FromResult(new ProviderCultureResult(cc));
        }
    }
}
