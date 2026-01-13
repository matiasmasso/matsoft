On client project:
install nuget Microsoft.Extensions.Localization
On Program.cs, between add HttpClient and builder.build, add:
builder.Services.AddLocalization();

In a RazorClassLibrary
create resources folder
add ApplicationResource.resx
add ApplicationResource.es.resx
set access modifier -> public

In client project:
@using Microsoft.Extensions.Localization
@using RazorClassLibrary.Resources
In page: @inject IStringLocalizer<ApplicationResource> Localizer
@Localizer["HelloWorld"]