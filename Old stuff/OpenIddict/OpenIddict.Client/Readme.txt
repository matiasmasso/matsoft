# ============================
# Add NuGet packages (Client)
# ============================
dotnet add .\OpenIddict.Client package Microsoft.AspNetCore.Components.WebAssembly.Authentication -v 10.0.0
dotnet add .\OpenIddict.Client package Microsoft.AspNetCore.Components.WebAssembly.DevServer -v 10.0.0


# ============================
# Create folder structure (Client)
# ============================
mkdir .\OpenIddict.Client\Services
mkdir .\OpenIddict.Client\Pages\Auth
mkdir .\OpenIddict.Client\Configuration

# Create empty files
New-Item .\OpenIddict.Client\Services\ApiClient.cs -ItemType File
New-Item .\OpenIddict.Client\Configuration\OidcConfig.cs -ItemType File
New-Item .\OpenIddict.Client\Pages\Auth\Login.razor -ItemType File
New-Item .\OpenIddict.Client\Pages\Auth\Logout.razor -ItemType File

Program.cs
wwwroot/appsetings.json
Index.html: add the Authentication script
Authentication endpoints
login/logout flow
a protected fetch data example
wrap app.razor in a CascadingAuthenticationState