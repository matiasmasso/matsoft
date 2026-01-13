using DTO;
using Spa.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Spa.ViewModels
{
    public class LayoutViewModel
    {
        private AppState AppState;
        public LayoutDTO? Model;
        public Globals.LoadStatusEnum LoadStatus { get; set; } = Globals.LoadStatusEnum.Empty;
        public event Action? OnStateChange;


        public LayoutViewModel(AppState appState)
        {
            AppState = appState;
        }

        public async void Fetch(HttpClient Http)
        {
            LoadStatus = Globals.LoadStatusEnum.Loading;
            try
            {
                var lang = LangDTO.Esp();
                var url = AppState.ApiUrl("Layout");
                var response = await Http.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    Model = Newtonsoft.Json.JsonConvert.DeserializeObject<LayoutDTO>(responseText);
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
