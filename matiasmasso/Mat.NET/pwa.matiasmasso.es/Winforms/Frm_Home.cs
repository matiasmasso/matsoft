using Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Winforms.Services;

namespace Winforms
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var services = new ServiceCollection();
            services.AddWindowsFormsBlazorWebView();
            services.AddHttpClient();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddAuthorizationCore();
            services.AddScoped<ICookie, Cookie>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<CultureService>();

            blazorWebView1.HostPage = "wwwroot\\index.html";
            blazorWebView1.Services = services.BuildServiceProvider();
            blazorWebView1.RootComponents.Add<CheckAuth>("#app",
               new Dictionary<string, object?> {
                    //{"Authenticated", new EventCallback<string>(null, (string token)=>{Authenticated(token); }) }
               });
        }

       // var accessTokenProvider = new AzureServiceTokenProvider("<your-connection-string-for-access-token-provider>");
       //builder.Services.AddHttpClient<IOrdersClient, OrdersClient>().ConfigureHttpClient(async conf =>
       //     {
       //     conf.BaseAddress = new Uri("<your-api-base-url>");
       // }).AddHttpMessageHandler(() => new BearerTokenHandler(accessTokenProvider, "https://your-azure-tenant.onmicrosoft.com/api"));
  }
}