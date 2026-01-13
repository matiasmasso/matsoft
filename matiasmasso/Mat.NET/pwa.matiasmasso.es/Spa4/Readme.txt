new blazor server with individual accounts

Update-Database
Add New ScaffoldedItem Identity (select default context) Override all files Skip overriding logout
comment duplicated stuff on program.cs:
//var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));;

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

AddShare references to DTO
Copy ViewModels
Install Microsoft.AspNet.WebApi.Client

_Imports.razor
@using DTO
@using Spa3
@using Spa3.Shared
@using Spa3.Components
@using Spa3.ViewModels
