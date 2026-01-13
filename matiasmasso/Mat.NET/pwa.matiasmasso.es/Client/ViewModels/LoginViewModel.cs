using Client.Shared;
using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Formatting;
using System.Net.Http.Json;

namespace Client.ViewModels
{
    public class LoginViewModel
    {
        private HttpClient Http;
        private AppState AppState;
        public delegate void StateChange();

        public AppState.StatusEnum LoadStatus = AppState.StatusEnum.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserDTO? User { get; set; }

        public LoginViewModel(HttpClient http, Shared.AppState appstate)
        {
            Http = http;
            AppState = appstate;
        }

        public async Task<bool> Submit()
        {
            LoadStatus = Shared.AppState.StatusEnum.Loading;
            AppState.ProblemDetails = null;
            try
            {
                var model = new LoginRequestDTO
                {
                    Email = Email,
                    Password = Password
                };
                var url = AppState.ApiUrl("layout/login");
                var response = await Http.PostAsync(url,model,new JsonMediaTypeFormatter());
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    AppState.Layout = Newtonsoft.Json.JsonConvert.DeserializeObject<LayoutDTO>(responseText);
                    LoadStatus = AppState.StatusEnum.Loaded;
                    return true;
                }
                else
                {
                    AppState.ProblemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>() ?? new ProblemDetails();
                    LoadStatus = AppState.StatusEnum.Failed;
                    return false;
                }
            }
            catch (Exception ex)
            {
                AppState.ProblemDetails = new ProblemDetails
                {
                    Title = "Error on menu download",
                    Detail = ex.Message
                };
                LoadStatus = Shared.AppState.StatusEnum.Failed;
                return false;
            }

        }

    }
}
