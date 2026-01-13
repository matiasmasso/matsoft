using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace Spa.ViewModels
{
    public class TargetDownloadsViewModel
    {
        private Shared.AppState AppState;
        public string SearchTerm = String.Empty;
        private BaseGuid Target;
        public List<DocfileDTO> Model = new();
        private List<DocfileDTO> items = new();

        public Globals.LoadStatusEnum LoadStatus = Globals.LoadStatusEnum.Empty;
        public delegate void StateChange();
        public event StateChange? OnStateChange;

        public TargetDownloadsViewModel(Shared.AppState appstate, BaseGuid target)
        {
            AppState = appstate;
            Target = target;
        }

        public List<DocfileDTO> Items()
        {
            if (string.IsNullOrEmpty(SearchTerm))
                return items;
            else
                return items.Where(x => x.Matches(SearchTerm)).ToList();
        }


        public async void Fetch(HttpClient http)
        {
            LoadStatus = Globals.LoadStatusEnum.Loading;
            AppState.ProblemDetails = null;
            try
            {
                var url = AppState.ApiUrl("Docfiles/FromTargetDownloads", Target.Guid.ToString());
                var response = await http!.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DocfileDTO>>(responseText);
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
                    Title = "Error on reading Target Downloads",
                    Detail = ex.Message
                };
                LoadStatus = Globals.LoadStatusEnum.Failed;
            }
        }
    }
}

