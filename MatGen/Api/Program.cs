using Api.Entities;
using Api.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using JsonOptions = Microsoft.AspNetCore.Mvc.JsonOptions;

var builder = WebApplication.CreateBuilder(args);

// CORS: allow Authorization header from your client origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("https://localhost:7172", "https://album.matiasmasso.es") // adjust origin
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// JWT config
var key = builder.Configuration["Jwt:Key"];
var issuer = builder.Configuration["Jwt:Issuer"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)),
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.ClaimsIdentity.UserIdClaimType = System.Security.Claims.ClaimTypes.NameIdentifier;
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//added on 13/11/2022 to move SQL connection string out of the code:
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<MatGenContext>(options => options.UseSqlServer(connectionString));

var test = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MatGenContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<Api.Services.AlbumService>();
builder.Services.AddScoped<Api.Services.CitaService>();
builder.Services.AddScoped<Api.Services.CitasService>();
builder.Services.AddScoped<Api.Services.CognomService>();
builder.Services.AddScoped<Api.Services.CognomsService>();
builder.Services.AddScoped<Api.Services.DocCodService>();
builder.Services.AddScoped<Api.Services.DocCodsService>();
builder.Services.AddScoped<Api.Services.DocFileService>();
builder.Services.AddScoped<Api.Services.DocFilesService>();
builder.Services.AddScoped<Api.Services.DocRelService>();
builder.Services.AddScoped<Api.Services.DocRelsService>();
builder.Services.AddScoped<Api.Services.DocService>();
builder.Services.AddScoped<Api.Services.DocsService>();
builder.Services.AddScoped<Api.Services.DocSrcService>();
builder.Services.AddScoped<Api.Services.DocSrcsService>();
builder.Services.AddScoped<Api.Services.DocTargetService>();
builder.Services.AddScoped<Api.Services.DocTargetsService>();
builder.Services.AddScoped<Api.Services.EnlaceService>();
builder.Services.AddScoped<Api.Services.EnlacesService>();
builder.Services.AddScoped<Api.Services.FirstnomService>();
builder.Services.AddScoped<Api.Services.FirstnomsService>();
builder.Services.AddScoped<Api.Services.LocationService>();
builder.Services.AddScoped<Api.Services.LocationsService>();
builder.Services.AddScoped<Api.Services.PersonService>();
builder.Services.AddScoped<Api.Services.PersonsService>();
builder.Services.AddScoped<Api.Services.ProfessionService>();
builder.Services.AddScoped<Api.Services.ProfessionsService>();
builder.Services.AddScoped<Api.Services.PubService>();
builder.Services.AddScoped<Api.Services.PubsService>();
builder.Services.AddScoped<Api.Services.RepoService>();
builder.Services.AddScoped<Api.Services.ReposService>();
builder.Services.AddScoped<Api.Services.UserService>();
builder.Services.AddScoped<Api.Services.UsersService>();

builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 30000483648;
});


//--- prevents Required field exceptions when serializing
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});


//--- prevents Swagger crashing due to enumerators
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});

//added on 02/11/22 to deliver mvc razor pages
builder.Services.AddRazorPages(c => c.RootDirectory = "/Views"); //adds services for Razor Pages to the app.


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// IMPORTANT: Authentication must come before Authorization and before endpoints
app.UseCors("AllowBlazorClient"); // must be before UseAuthentication/UseAuthorization
app.UseAuthentication();
app.UseAuthorization();

//------------------to allow Index.html-added on 19/03/2025
var options = new DefaultFilesOptions();
options.DefaultFileNames.Clear();
options.DefaultFileNames.Add("index.html");
app.UseDefaultFiles(options);

//=========================== added on 02/11/22 to deliver mvc razor pages
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//==============================



app.UseAuthorization();



//=========================== added on 02/11/22 to deliver mvc razor pages

app.MapRazorPages();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//    endpoints.MapRazorPages();
//});

//===========================

app.MapControllers();

app.Run();
