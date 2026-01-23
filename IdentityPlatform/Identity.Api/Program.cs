using Identity.Api.Application.Apps;
using Identity.Api.Application.Auth;
using Identity.Api.Domain.Users;
using Identity.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// EF Core
builder.Services.AddDbContext<IdentityDbContext>(options =>
{
    var cs = builder.Configuration.GetConnectionString("IdentityDatabase")
        ?? throw new InvalidOperationException("Connection string 'IdentityDatabase' not found.");

    options.UseSqlServer(cs);
});


// Controllers
builder.Services.AddControllers();


var jwtSection = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<JwtOptions>(jwtSection);

var jwt = jwtSection.Get<JwtOptions>()!;

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SigningKey))
        };
    });

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed(_ => true); // temporary during development
    });
});

builder.Services.AddScoped<AppService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<EnrollmentService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultFiles();   // <-- activa index.html com a p gina per defecte
app.UseStaticFiles();    // <-- serveix wwwroot


app.MapControllers();

app.Run();