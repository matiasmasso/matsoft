public interface ITokenProvider
{
    Task<string?> GetTokenAsync();
}