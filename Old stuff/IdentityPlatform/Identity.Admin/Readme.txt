1. Nuget Packages:
dotnet add package Microsoft.AspNetCore.Components.WebAssembly
dotnet add package Microsoft.AspNetCore.Components.WebAssembly.Authentication
dotnet add package Microsoft.Extensions.Http
dotnet add package System.IdentityModel.Tokens.Jwt
dotnet add package Microsoft.JSInterop
dotnet add package CommunityToolkit.Mvvm
dotnet add package Blazored.Toast

2. Folders (next time ask for Idempotent to skip Pages warning)
# Root project folder
mkdir Identity.Admin
cd Identity.Admin

# Core folders
mkdir Services
mkdir Services\Auth
mkdir Services\Api
mkdir Models
mkdir Models\Auth
mkdir Models\Users
mkdir Models\Apps
mkdir Components
mkdir Components\Users
mkdir Components\Apps
mkdir Pages
mkdir Shared
mkdir Utils

3. Boilerplate
ni Services\Auth\TokenStorage.cs
ni Services\Auth\AuthService.cs
ni Services\Auth\RefreshTokenHandler.cs
ni Services\Auth\JwtParser.cs
ni Services\Api\IdentityApiClient.cs
ni Services\Api\HttpClientExtensions.cs
ni Pages\Login.razor
ni Pages\Users.razor
ni Pages\Apps.razor
ni Shared\MainLayout.razor
ni Shared\NavMenu.razor
ni Utils\LocalStorage.cs