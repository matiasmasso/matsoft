using Identity.Api.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. CORS for the WASM client
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:7002",
            "https://identity.admin.matiasmasso.es",
            "https://album.matiasmasso.es",
            "https://gen.matiasmasso.es",
            "https://cash.tatita.eu")
              .AllowAnyHeader()
              .AllowAnyMethod();
        // .AllowCredentials() // not needed for token/discovery
    });
});

// 2. Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
});


// 4. JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"]!;
var jwtIssuer = builder.Configuration["Jwt:Issuer"]!;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});


builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex) when (IsSqlConnectionFailure(ex))
    {
        context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
        await context.Response.WriteAsJsonAsync(new
        {
            error = "Database connection failed. Check VPN or SQL Server availability."
        });
    }
});



// CORS must be before auth/authorization and before endpoints
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

//used to display index.htmml
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.Run();

static bool IsSqlConnectionFailure(Exception ex)
{
    // Direct SQL exception
    if (ex is SqlException) return true;

    // EF Core wraps SQL exceptions in InvalidOperationException or DbUpdateException
    if (ex.InnerException is SqlException) return true;

    // Async pipeline often wraps everything in AggregateException
    if (ex is AggregateException agg && agg.InnerException is SqlException) return true;

    return false;
}
