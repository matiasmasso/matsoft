using DTO;
using System.Net.Http.Json;

namespace Wasm.Services
{
    public class UsersService
    {
        private readonly ApiHttpClient _api;

        public UsersService(ApiHttpClient api)
        {
            _api = api;
        }

        private HttpClient CreateClient()
        {
            var http = _api.CreateAuthenticated();   // IMPORTANT
            Console.WriteLine($"[UsersService] Using BaseAddress: {http.BaseAddress}");
            return http;
        }

        public Task<List<UserModel>?> GetAllAsync()
        {
            var http = _api.CreateAuthenticated();   // IMPORTANT
            return http.GetFromJsonAsync<List<UserModel>>("users");
        }


        // ---------------------------------------------------------
        // GET SINGLE USER
        // ---------------------------------------------------------
        public async Task<UserModel?> Find(UserModel user)
        {
            var http = CreateClient();
            var url = $"users/{user.Guid}";

            var response = await http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error reading user: {msg}");
            }

            return await response.Content.ReadFromJsonAsync<UserModel>();

        }

        // ---------------------------------------------------------
        // SAVE USER
        // ---------------------------------------------------------
        public async Task SaveAsync(UserModel user)
        {
            var http = CreateClient();
            var url = "users";

            Console.WriteLine($"[UsersService] POST {http.BaseAddress}{url}");

            var response = await http.PostAsJsonAsync(url, user);

            if (!response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error saving user: {msg}");
            }
        }

        // ---------------------------------------------------------
        // DELETE USER
        // ---------------------------------------------------------
        public async Task DeleteAsync(UserModel user)
        {
            var http = CreateClient();
            var url = "users/delete";

            Console.WriteLine($"[UsersService] POST {http.BaseAddress}{url}");

            var response = await http.PostAsJsonAsync(url, user);

            if (!response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error deleting user: {msg}");
            }
        }
    }
}