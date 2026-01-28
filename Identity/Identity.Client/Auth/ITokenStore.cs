using System.Threading.Tasks;

namespace Identity.Client.Auth;

public interface ITokenStore
{
    Task SetToken(string token);
    Task<string?> GetToken();
    Task ClearToken();
}