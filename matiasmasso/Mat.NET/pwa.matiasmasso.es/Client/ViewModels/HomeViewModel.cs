using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace Client.ViewModels
{
    public class HomeViewModel
    {
        private Shared.AppState AppState;
        private HttpClient Http;
        public HomeDTO? Model;
        public Shared.AppState.StatusEnum LoadStatus { get; set; } = Shared.AppState.StatusEnum.Empty;
        public delegate void StateChange();
        public event StateChange OnStateChange;
        private static event Action? RequestToFetch;

        public HomeViewModel( HttpClient http, Shared.AppState appState)
        {
            Http= http;
            AppState = appState;
            RequestToFetch += Fetch;
        }

        public bool IsLoaded()
        {
            var retval = false;
            if (LoadStatus == Shared.AppState.StatusEnum.Loaded)
                retval = true;
            else if (LoadStatus != Shared.AppState.StatusEnum.Loading && LoadStatus != Shared.AppState.StatusEnum.Failed)
                RequestToFetch?.Invoke();
            return retval;
        }

        public async void Fetch()
        {
            LoadStatus = Shared.AppState.StatusEnum.Loading;
            AppState.ProblemDetails = null;
            var lang = LangDTO.Esp();
            var url = AppState.ApiUrl("Home");
            try
            {
                var response = await Http.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    Model = Newtonsoft.Json.JsonConvert.DeserializeObject<HomeDTO>(responseText);
                    LoadStatus = Shared.AppState.StatusEnum.Loaded;
                    OnStateChange.Invoke();
                }
                else
                {
                    AppState.ProblemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>() ?? new ProblemDetails();
                    LoadStatus = Shared.AppState.StatusEnum.Failed;
                    OnStateChange.Invoke();
                }
            }
            catch (Exception ex)
            {
                AppState.ProblemDetails = new ProblemDetails
                {
                    Title = "Error on menu download",
                    Detail = ex.Message + "<br/>Url: " + url
                };
                LoadStatus = Shared.AppState.StatusEnum.Failed;
            }

            //NotifyStateChanged();
        }
    }
}
