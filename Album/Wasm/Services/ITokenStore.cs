using DTO;

namespace Wasm.Services
{

    public interface ITokenStore
{
    /// <summary>
    /// Retrieves the current token bundle from storage.
    /// Returns null if no tokens are stored.
    /// </summary>
    Task<TokenBundle?> GetBundleAsync();

    /// <summary>
    /// Persists the provided token bundle.
    /// </summary>
    Task SetBundleAsync(TokenBundle bundle);

    /// <summary>
    /// Removes all stored authentication tokens.
    /// </summary>
    Task ClearAsync();
    }
}
