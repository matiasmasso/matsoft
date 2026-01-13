using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace Spa.ViewModels
{
    public class ImmobleViewModel
    {
        private Shared.AppState AppState;
        public ImmobleDTO Model = new();

        public Globals.LoadStatusEnum LoadStatus = Globals.LoadStatusEnum.Empty;
        public delegate void StateChange();
        public event StateChange? OnStateChange;

        public ImmobleViewModel(Shared.AppState appstate, ImmobleDTO model)
        {
            //from editor, already fetched
            AppState = appstate;
            Model = model;
        }

        public ImmobleViewModel(Shared.AppState appstate, Guid guid)
        {
            //from landing page, to fetch
            AppState = appstate;
            Model = new ImmobleDTO(guid);
        }


        public async void Fetch(HttpClient http)
        {
            LoadStatus = Globals.LoadStatusEnum.Loading;
            AppState.ProblemDetails = null;
            try
            {
                var url = AppState.ApiUrl("Immoble",Model.Guid.ToString());
                var response = await http!.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    Model = Newtonsoft.Json.JsonConvert.DeserializeObject<ImmobleDTO>(responseText);
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
                    Title = "Error on reading Immoble",
                    Detail = ex.Message
                };
                LoadStatus = Globals.LoadStatusEnum.Failed;
            }
        }

        public async Task<bool> Submit()
        {
            return false;
        }

        private async Task<bool> Update(ImmobleDTO item)
        {
            return false;
        }
        private async Task<bool> Create(ImmobleDTO item)
        {
            return false;
        }

        public async Task<bool> Delete()
        {
            return false;
        }

    }
}
