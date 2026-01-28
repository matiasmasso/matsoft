using ApiCore.Http;
using ApiCore.Notifications;
using Identity.Admin.Auth;
using Identity.Admin.Client.Services;
using Identity.Admin.Components;
using Identity.Admin.Http;
using MatComponents.Services;


var builder = WebApplication.CreateBuilder(args);

// Razor components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped<JsTokenAccessor>();
builder.Services.AddScoped<ITokenProvider, Identity.Admin.Auth.LocalStorageTokenProvider>();
builder.Services.AddScoped<IRefreshTokenProvider, LocalStorageRefreshTokenProvider>();
builder.Services.AddScoped<TokenRefreshService>();
builder.Services.AddScoped<AuthHeaderHandler>();

builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService<AuthHeaderHandler>();
    return new HttpClient(handler)
    {
        BaseAddress = new Uri("https://localhost:5001/")
    };
});

builder.Services.AddScoped<ToastService>();
builder.Services.AddScoped<INotificationService, ToastNotificationService>();
builder.Services.AddScoped<SafeHttpClient>();
builder.Services.AddScoped<IdentityApiClient>();

var app = builder.Build();

app.UseDeveloperExceptionPage();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Identity.Admin.Client._Imports).Assembly);

app.Run();