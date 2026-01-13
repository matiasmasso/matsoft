Blazor server authentication

Create CustomAuthenticationStateProvider.cs

Add the service before builder.build:
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

Wrap app.razor with <CascadingAuthenticationState> component

Insert LoginDistplay.razor  component on Layout header:
<AuthorizeView>
    <Authorized>
        <a href="Identity/Account/Manage">Hello, @context.User.Identity?.Name!</a>
        <form method="post" action="Identity/Account/LogOut">
            <button type="submit" class="nav-link btn btn-link">Log out</button>
        </form>
    </Authorized>
    <NotAuthorized>
        <a href="Identity/Account/Register">Register</a>
        <a href="/Login">Log in</a>
    </NotAuthorized>
</AuthorizeView>


Create a Login page:

@code {
    [CascadingParameter] public Task<AuthenticationState> AuthTask { get; set; }
    [Inject] private AuthenticationStateProvider AuthState { get; set; }
    private System.Security.Claims.ClaimsPrincipal user;

    protected async override Task OnInitializedAsync()
    {
        var authState = await AuthTask;
        this.user = authState.User;
    }

    public async Task Login()
    {
        //Todo:  Validate against actual database.
        var authState = await ((CustomAuthenticationStateProvider)AuthState).ChangeUser("matias@matiasmasso.es", "myId", "Associate");
        this.user = authState.User;
    }
}