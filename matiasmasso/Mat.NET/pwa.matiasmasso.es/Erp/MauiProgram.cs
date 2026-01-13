using Erp.Data;
using Erp;
using Microsoft.AspNetCore.Components.Authorization;
using Erp.Services;
using Erp.Shared;
using System.Globalization;
using Erp.Models;
using DTO;
using Components;
using Microsoft.AspNetCore.Http;

namespace Erp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif

            //added on 22/08/22
            builder.Services.AddScoped(sp => new HttpClient { });
            builder.Services.AddScoped<ICookie, Cookie>();

            //Register needed elements for authentication
            builder.Services.AddAuthorizationCore(); // This is the core functionality
            builder.Services.AddScoped<CustomAuthenticationStateProvider>(); // This is our custom provider
            //When asking for the default Microsoft one, give ours!
            builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomAuthenticationStateProvider>());

            builder.Services.AddSingleton<AppState>();

            //added on 24/08/22 see https://stackoverflow.com/questions/73474199/cant-keep-cultureinfo-across-pages-on-net-maui-blazor
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            string? cc = Preferences.Get("blazorCulture", null);
            if (string.IsNullOrEmpty(cc)) {
                cc = "es-ES";
                Preferences.Default.Set("blazorCulture", cc);
            }
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(cc);

            //added on 18/9/22
            builder.Services.AddScoped<IClipboardService, ClipboardService>();

            //added on 29/09/22 to send email messages
            var mailSettings = new MailSettings();
            builder.Services.AddSingleton(sp => mailSettings); //(Configuration.GetSection("MailSettings").Get<MailSettings>());
            builder.Services.AddScoped<IMailService, DTO.MailService>();

            return builder.Build();
        }
    }
}