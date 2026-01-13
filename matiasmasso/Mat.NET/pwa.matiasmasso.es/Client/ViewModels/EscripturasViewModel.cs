using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Formatting;
using System.Net.Http.Json;

namespace Client.ViewModels
{
    public class EscripturasViewModel
    {
        private Shared.AppState AppState;
        public string SearchTerm;
        public List<EscripturaDTO> Model = new();
        private List<EscripturaDTO> items = new();

        public Shared.AppState.StatusEnum LoadStatus = Shared.AppState.StatusEnum.Empty;
        public delegate void StateChange();
        public event StateChange? OnStateChange;

        public EscripturasViewModel(Shared.AppState appstate)
        {
            AppState = appstate;
        }

        public List<EscripturaDTO> Items()
        {
            if (string.IsNullOrEmpty(SearchTerm))
                return items;
            else
                 return items.Where(x => x.Matches(SearchTerm)).ToList();
        }


        public async void Fetch()
        {
            LoadStatus = Shared.AppState.StatusEnum.Loading;
            AppState.ProblemDetails = null;
            try
            {
                var url = AppState.ApiUrl("Escripturas");
                var response = await AppState.Http!.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EscripturaDTO>>(responseText);
                    LoadStatus = Shared.AppState.StatusEnum.Loaded;
                    OnStateChange!.Invoke();
                }
                else
                {
                    AppState.ProblemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>() ?? new ProblemDetails();
                    LoadStatus = Shared.AppState.StatusEnum.Failed;
                    OnStateChange!.Invoke();
                }
            }
            catch (Exception ex)
            {
                AppState.ProblemDetails = new ProblemDetails
                {
                    Title = "Error on Escripturas download",
                    Detail = ex.Message
                };
                LoadStatus = Shared.AppState.StatusEnum.Failed;
            }
        }
    }
}
