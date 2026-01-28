# ============================
# Create solution + directories
# ============================
mkdir IdentityPlatform
cd IdentityPlatform

dotnet new sln -n IdentityPlatform

# ============================
# Create API project (.NET 10)
# ============================
dotnet new webapi -n OpenIddict.Api --framework net10.0
# Remove WeatherForecast boilerplate
Remove-Item -Recurse -Force .\OpenIddict.Api\Controllers
Remove-Item -Recurse -Force .\OpenIddict.Api\WeatherForecast.cs

# Set root namespace
Add-Content .\OpenIddict.Api\OpenIddict.Api.csproj '<PropertyGroup><RootNamespace>Identity.Api</RootNamespace></PropertyGroup>'


# ============================
# Add projects to solution
# ============================
dotnet sln add .\OpenIddict.Api\OpenIddict.Api.csproj

# ============================
# Add NuGet packages (API)
# ============================
dotnet add .\OpenIddict.Api package OpenIddict -v 7.1.0
dotnet add .\OpenIddict.Api package OpenIddict.AspNetCore -v 7.1.0
dotnet add .\OpenIddict.Api package OpenIddict.EntityFrameworkCore -v 7.1.0
dotnet add .\OpenIddict.Api package OpenIddict.Validation.AspNetCore -v 7.1.0

dotnet add .\OpenIddict.Api package Microsoft.EntityFrameworkCore -v 10.0.0
dotnet add .\OpenIddict.Api package Microsoft.EntityFrameworkCore.Sqlite -v 10.0.0
dotnet add .\OpenIddict.Api package Microsoft.EntityFrameworkCore.Design -v 10.0.0

# ============================
# Create folder structure (API)
# ============================
mkdir .\OpenIddict.Api\Data
mkdir .\OpenIddict.Api\Models
mkdir .\OpenIddict.Api\Endpoints
mkdir .\OpenIddict.Api\Configuration

# Create empty files
New-Item .\OpenIddict.Api\Data\AppDbContext.cs -ItemType File
New-Item .\OpenIddict.Api\Models\TestUser.cs -ItemType File
New-Item .\OpenIddict.Api\Configuration\OpenIddictConfig.cs -ItemType File
New-Item .\OpenIddict.Api\Endpoints\AuthEndpoints.cs -ItemType File

-AppDbContext.cs
-TestUser
-OpenIddictConfig
-AuthEndpoints
-Program.cs