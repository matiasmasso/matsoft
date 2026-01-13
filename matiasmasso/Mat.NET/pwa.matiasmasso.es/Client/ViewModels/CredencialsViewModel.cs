using DTO;
using Client.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Formatting;
using System.Net.Http.Json;

namespace Client.ViewModels
{
    public class CredencialViewModel
    {
        private Shared.AppState AppState;
        private Guid Guid;
        public CredencialModel? Model;
        public AppState.StatusEnum LoadStatus = Shared.AppState.StatusEnum.Empty;
        public delegate void StateChange();
        public event StateChange? OnStateChange;


        public CredencialViewModel(AppState appstate, Guid guid)
        {
            AppState = appstate;
            Guid = guid;
        }

        public async void Fetch()
        {
            LoadStatus = Shared.AppState.StatusEnum.Loading;
            AppState.ProblemDetails = null;
            try
            {
                var url = AppState.ApiUrl("Credencial", Guid.ToString());
                var response = await AppState.Http!.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    var dto = Newtonsoft.Json.JsonConvert.DeserializeObject<CredencialDTO>(responseText);
                    Model = dto.Model;
                    AppState.Layout.PrivateMenuCustomItems = dto.MenuItems;
                    AppState.NotifyLayoutChange();

                    LoadStatus = AppState.StatusEnum.Loaded;
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
                    Title = "Error on Credencial download",
                    Detail = ex.Message
                };
                LoadStatus = Shared.AppState.StatusEnum.Failed;
            }
        }



    }


    public class CredencialsViewModel
    {
        private Shared.AppState AppState;
        public string? SearchTerm;
        public CredencialListDTO Model = new();
        private List<CredencialModel> items = new();

        public Shared.AppState.StatusEnum LoadStatus = Shared.AppState.StatusEnum.Empty;
        public delegate void StateChange();
        public event StateChange? OnStateChange;

        public CredencialsViewModel(Shared.AppState appstate)
        {
            AppState = appstate;
        }

        public List<CredencialModel> Items()
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
                var url = AppState.ApiUrl("Credencials");
                var response = await AppState.Http!.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    Model = Newtonsoft.Json.JsonConvert.DeserializeObject<CredencialListDTO>(responseText);
                    items = Model.Items;
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
                    Title = "Error on Credencials download",
                    Detail = ex.Message
                };
                LoadStatus = Shared.AppState.StatusEnum.Failed;
            }
        }
    }
}
