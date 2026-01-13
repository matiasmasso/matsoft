using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Formatting;
using System.Net.Http.Json;

namespace Spa4.ViewModels
{
    public class RafflesViewModel
    {
        private HttpClient Http;
        private Shared.AppState AppState;
        public RafflesDTO Model = new();

        public Globals.LoadStatusEnum LoadStatus = Globals.LoadStatusEnum.Empty;
        public delegate void StateChange();
        public event StateChange? OnStateChange;

        public RafflesViewModel(HttpClient http, Shared.AppState appstate)
        {
            Http = http;
            AppState = appstate;
        }

        public string Title()
        {
            return "Sorteos"; //TO DO: lang
        }

        public async void Fetch()
        {
            LoadStatus = Globals.LoadStatusEnum.Loading;
            AppState.ProblemDetails = null;
            try
            {
                var url = Globals.ApiUrl("Raffles/Esp");
                var response = await Http.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    Model = Newtonsoft.Json.JsonConvert.DeserializeObject<RafflesDTO>(responseText);
                    LoadStatus = Globals.LoadStatusEnum.Loaded;
                    OnStateChange!.Invoke();
                }
                else
                {
                    AppState.ProblemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>() ?? new ProblemDetails();
                    LoadStatus = Globals.LoadStatusEnum.Failed;
                    OnStateChange!.Invoke();
                }
            }
            catch (Exception ex)
            {
                AppState.ProblemDetails = new ProblemDetails
                {
                    Title = "Error on raffles download",
                    Detail = ex.Message
                };
                LoadStatus = Globals.LoadStatusEnum.Failed;
            }
        }
    }
}
