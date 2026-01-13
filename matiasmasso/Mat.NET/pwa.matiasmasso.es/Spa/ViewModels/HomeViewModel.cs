using DTO;
using Spa.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Spa.ViewModels
{
    public class HomeViewModel
    {
        private AppState AppState;
        public HomeDTO? Model;
        public Globals.LoadStatusEnum LoadStatus { get; set; } = Globals.LoadStatusEnum.Empty;
        public event Action? OnStateChange;


        public HomeViewModel(AppState appState)
        {
            AppState = appState;
        }

        public async void Fetch(HttpClient http)
        {
            LoadStatus = Globals.LoadStatusEnum.Loading;
            try
            {
                var url = AppState.ApiUrl("Home");
                if (!http.DefaultRequestHeaders.Contains("Lang"))
                    http.DefaultRequestHeaders.Add("Lang", AppState.Lang!.ToString());
                var response = await http.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    Model = Newtonsoft.Json.JsonConvert.DeserializeObject<HomeDTO>(responseText);
                    LoadStatus = Globals.LoadStatusEnum.Loaded;
                    NotifyStateChanged();
                }
                else
                {
                    AppState.ProblemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>() ?? new ProblemDetails();
                    LoadStatus = Globals.LoadStatusEnum.Failed;
                    NotifyStateChanged();
                }
            }
            catch (Exception ex)
            {
                AppState.ProblemDetails = new ProblemDetails
                {
                    Title = "Error on menu download",
                    Detail = ex.Message
                };
                LoadStatus = Globals.LoadStatusEnum.Failed;
            }
        }

        public bool IsLoaded() => LoadStatus == Globals.LoadStatusEnum.Loaded;
        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
