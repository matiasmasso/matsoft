using Web.Services;
using DTO.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Web;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Blazor.Analytics;
using Thinktecture.Blazor.FileHandling;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Custom
builder.Services.AddGoogleAnalytics(); // TODO: add TrackingKeyId

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddFileHandlingService(); // for file association handlers
builder.Services.AddSingleton<AppStateService>();
builder.Services.AddSingleton<EmpsService>();
builder.Services.AddSingleton<TaxesService>();
builder.Services.AddSingleton<ClaimsService>();
builder.Services.AddSingleton<StringsLocalizerService>();
builder.Services.AddSingleton<NavService>();
builder.Services.AddSingleton<BannersService>();
builder.Services.AddSingleton<LangTextsService>();
builder.Services.AddSingleton<ProductBrandsService>();
builder.Services.AddSingleton<ProductDeptsService>();
builder.Services.AddSingleton<ProductCategoriesService>();
builder.Services.AddSingleton<ProductSkusService>();
builder.Services.AddSingleton<ProductSkuStocksService>();
builder.Services.AddSingleton<ProductSkuPncsService>();
builder.Services.AddSingleton<RetailPricesService>();
builder.Services.AddSingleton<PriceListSuppliersService>();
builder.Services.AddSingleton<ChannelDtosOnRrppService>();
builder.Services.AddSingleton<ExplicitDiscountsService>();
builder.Services.AddSingleton<CanonicalUrlsService>();
builder.Services.AddSingleton<ProductPluginsService>();
builder.Services.AddSingleton<CustomerProductsService>();
builder.Services.AddSingleton<ProductsService>();
builder.Services.AddSingleton<PortadaImgsService>();
builder.Services.AddSingleton<NoticiasService>();
builder.Services.AddSingleton<CountriesService>();
builder.Services.AddSingleton<ZonasService>();
builder.Services.AddSingleton<ProvinciasService>();
builder.Services.AddSingleton<LocationsService>();
builder.Services.AddSingleton<ZipsService>();
builder.Services.AddSingleton<AddressesService>();
builder.Services.AddSingleton<ContactsService>();
builder.Services.AddSingleton<ContactClassesService>();
builder.Services.AddSingleton<AtlasService>();
builder.Services.AddSingleton<StoreLocatorRawService>();
builder.Services.AddSingleton<RafflesService>();
builder.Services.AddSingleton<MarketPlacesService>();
builder.Services.AddSingleton<CustomerDeptsService>();
builder.Services.AddSingleton<StaffsService>();
builder.Services.AddSingleton<RepsService>();
builder.Services.AddSingleton<BanksService>();
builder.Services.AddSingleton<BankBranchesService>();
builder.Services.AddSingleton<BancsService>();
builder.Services.AddSingleton<BancSaldosService>();
builder.Services.AddSingleton<JornadasLaboralsService>();
builder.Services.AddSingleton<NominasService>();
builder.Services.AddSingleton<CertificatsIrpfService>();
builder.Services.AddSingleton<PgcCtasService>();
builder.Services.AddSingleton<PgcClassesService>();
builder.Services.AddSingleton<PurchaseOrdersService>();
builder.Services.AddSingleton<DeliveriesService>();
builder.Services.AddSingleton<BancTransfersService>();
builder.Services.AddSingleton<IbanStructuresService>();
builder.Services.AddSingleton<IbansService>();
builder.Services.AddSingleton<CcasService>();
builder.Services.AddSingleton<TransmissionsService>();
builder.Services.AddSingleton<TasksService>();
builder.Services.AddSingleton<IncidenciasService>();

builder.Services.AddHttpClient();
builder.Services.AddScoped<ApiClientService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddTransient<CookieService>();
builder.Services.AddScoped<SessionStateService>();
builder.Services.AddScoped<CredencialsService>();
builder.Services.AddScoped<VehiclesService>();
builder.Services.AddScoped<ImmoblesService>();
builder.Services.AddScoped<CorrespondenciasService>();
builder.Services.AddScoped<ContractsService>();
builder.Services.AddScoped<EscripturasService>();
builder.Services.AddScoped<NominasEscuraService>();
builder.Services.AddScoped<MgzInventoryService>();
builder.Services.AddScoped<PgcCtaSdosService>();
builder.Services.AddScoped<InvoicesSentService>();
//builder.Services.AddScoped<Web.Services.Integracions.Sonae.LogisticLabel>();

builder.Services.AddTransient<CultureService>();
builder.Services.AddTransient<SocialNetworksService>();


//builder.Services.AddResponseCaching(); // added on 12/6/23



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

//app.UseResponseCaching(); // added on 12/6/23

app.UseRouting();

//-------- added for culture
var supportedCultures = new[] { "es", "ca", "en", "pt" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.MapControllers();


app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
