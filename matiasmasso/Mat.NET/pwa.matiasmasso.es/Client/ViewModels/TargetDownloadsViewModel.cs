using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace Client.ViewModels
{
    public class TargetDownloadsViewModel
    {
        private Shared.AppState AppState;
        public string SearchTerm = String.Empty;
        private BaseGuid Target;
        public List<DocfileDTO> Model = new();
        private List<DocfileDTO> items = new();

        public Shared.AppState.StatusEnum LoadStatus = Shared.AppState.StatusEnum.Empty;
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


        public async void Fetch()
        {
            LoadStatus = Shared.AppState.StatusEnum.Loading;
            AppState.ProblemDetails = null;
            try
            {
                var url = AppState.ApiUrl("Docfiles/FromTargetDownloads", Target.Guid.ToString());
                var response = await AppState.Http!.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DocfileDTO>>(responseText);
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
                    Title = "Error on reading Target Downloads",
                    Detail = ex.Message
                };
                LoadStatus = Shared.AppState.StatusEnum.Failed;
            }
        }
    }
}

