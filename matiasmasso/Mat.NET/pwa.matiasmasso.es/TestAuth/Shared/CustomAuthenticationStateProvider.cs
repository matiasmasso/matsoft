using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private HttpClient http;
    public CustomAuthenticationStateProvider( HttpClient http)
    {
        this.http = http;
        this.CurrentUser = this.GetAnonymous();
    }

    private ClaimsPrincipal CurrentUser { get; set; }


    private ClaimsPrincipal GetUser(string userName, string id, string role)
    {

        var identity = new ClaimsIdentity(new[]
        {
                    new Claim(ClaimTypes. Sid, id),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, role)
                }, "Authentication type");
        return new ClaimsPrincipal(identity);
    }


    private ClaimsPrincipal GetAnonymous()
    {
        var identity = new ClaimsIdentity(new[]
       {
                    new Claim(ClaimTypes.Sid, "0"),
                    new Claim(ClaimTypes.Name, "Anonymous"),
                    new Claim(ClaimTypes.Role, "Anonymous")
                }, null);

        return new ClaimsPrincipal(identity);
    }


    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var task = Task.FromResult(new AuthenticationState(this.CurrentUser));

        return task;
    }

    public Task<AuthenticationState> ChangeUser(string username, string id, string role)
    {
        var token = "thisIsAFakeToken";
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        this.CurrentUser = this.GetUser(username, id, role);
        var task = this.GetAuthenticationStateAsync();
        this.NotifyAuthenticationStateChanged(task);
        return task;
    }

    public Task<AuthenticationState> Logout()
    {
        this.CurrentUser = this.GetAnonymous();
        var task = this.GetAuthenticationStateAsync();
        this.NotifyAuthenticationStateChanged(task);
        return task;
    }
}