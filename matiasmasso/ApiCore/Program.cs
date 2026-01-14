using Api.Extensions;
using Api.Services.Implementations;
using Api.Services.Interfaces;
using Api.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<AppState>();



//-------added on 21/8 for JWT authentication

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    //options.TokenValidationParameters = new TokenValidationParameters()
    //{
    //    ValidateIssuer = true,
    //    ValidateAudience = true,
    //    ValidAudience = builder.Configuration["Jwt:Audience"],
    //    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    //};
});



//------------------CORS----------------------------
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("*")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});
//------------------CORS----------------------------


builder.Services.AddOutputCache(options => options.AddCustomPolicies());


//--- prevents Required field exceptions when serializing
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition =  JsonIgnoreCondition.WhenWritingNull;
});


//--- prevents Swagger crashing due to enumerators
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});

builder.Services.AddSingleton<IExcelBuilderService, ExcelBuilderService>();
builder.Services.AddTransient<IExcelBookfrasService, ExcelBookfrasService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

//temporary out of if (app.Environment.IsDevelopment())
app.UseSwagger();
app.UseSwaggerUI();


//app.MapGet("/purge/{tag}", async (IOutputCacheStore cache, string tag) =>
//{
//    await cache.EvictByTagAsync(tag, default);
//    return Task.CompletedTask;
//});


app.UseHttpsRedirection();

//------------------to allow Index.html----------------------------
var options = new DefaultFilesOptions();
options.DefaultFileNames.Clear();
options.DefaultFileNames.Add("index.html");
app.UseDefaultFiles(options);

app.UseStaticFiles();


app.Use(async (context, next) =>
{
    string path = context.Request.Path;

    if (path.EndsWith(".css") || path.EndsWith(".js"))
    {
        //Set css and js files to be cached for 1 days
        TimeSpan maxAge = new TimeSpan(1, 0, 0, 0);     //1 days
        context.Response.Headers.Append("Cache-Control", "max-age=" + maxAge.TotalSeconds.ToString("0"));

    }
    else if (path.EndsWith(".gif") || path.EndsWith(".jpg") || path.EndsWith(".png") || path.EndsWith(".svg"))
    {
        TimeSpan maxAge = new TimeSpan(1, 0, 0, 0);     //1 days
        context.Response.Headers.Append("Cache-Control", "max-age=" + maxAge.TotalSeconds.ToString("0"));
    }
    else
    {
        //Request for views fall here.
        context.Response.Headers.Append("Cache-Control", "no-cache");
        context.Response.Headers.Append("Cache-Control", "private, no-store");

    }
    await next();
});

app.UseRouting();
//----------------------------------------------


//------------------CORS----------------------------
app.UseCors(); //before useAuthorisation
//------------------CORS----------------------------

app.UseOutputCache();

app.UseAuthentication();

//---------------------

app.UseAuthorization();

app.MapControllers();

app.Run();

