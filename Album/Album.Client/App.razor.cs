using Album.Client.Services;
using Microsoft.AspNetCore.Components;

public partial class App : ComponentBase
{
    [Inject] public UserSettingsService Settings { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        //await Settings.InitializeAsync();
    }
}

