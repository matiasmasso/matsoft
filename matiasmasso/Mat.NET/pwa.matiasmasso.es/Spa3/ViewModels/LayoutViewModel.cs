using DTO;
using Microsoft.AspNetCore.Components.Authorization;
using Spa3.Shared;
using System.Net.Http.Headers;

namespace Spa3.ViewModels
{
    public class LayoutViewModel
    {
        private AppState AppState;
        public LayoutDTO? Model;
        public bool IsLogin = false;
        public event Action? OnStateChange;

        public LayoutViewModel(AppState appState)
        {
            AppState = appState;
        }

        private void NotifyStateChanged() => OnStateChange?.Invoke();


        public async void Fetch(HttpClient http)
        {
            try
            {
                var lang = LangDTO.Esp();
                var url = Globals.ApiUrl("Layout");
                var authState = await AppState.authenticationState;
                var user = authState.User;
                var isAuthenticated = user?.Identity?.IsAuthenticated ?? false;
                var userId = user?.GetUserId();
                if (isAuthenticated && userId != null)
                {
                    http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiKey", authState.User.GetUserId().ToString());
                }
                var response = await http.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    Model = Newtonsoft.Json.JsonConvert.DeserializeObject<LayoutDTO>(responseText);
                    NotifyStateChanged();
                }
                else
                {
                    var x = response;
                }
           }
            catch (Exception ex)
            {
           }
        }



    }
}
