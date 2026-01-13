using DTO;
using Spa.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Formatting;
using System.Net.Http.Json;

namespace Spa.ViewModels
{
    public class CredencialViewModel
    {
        private Shared.AppState AppState;
        private Guid Guid;
        public CredencialModel? Model;
        public Globals.LoadStatusEnum LoadStatus = Globals.LoadStatusEnum.Empty;
        public delegate void StateChange();
        public event StateChange? OnStateChange;


        public CredencialViewModel(AppState appstate, Guid guid)
        {
            AppState = appstate;
            Guid = guid;
        }

        public async void Fetch(HttpClient http)
        {
            LoadStatus = Globals.LoadStatusEnum.Loading;
            AppState.ProblemDetails = null;
            try
            {
                var url = AppState.ApiUrl("Credencial", Guid.ToString());
                var response = await http.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    var dto = Newtonsoft.Json.JsonConvert.DeserializeObject<CredencialDTO>(responseText);
                    Model = dto.Model;
                    AppState.Layout.PrivateMenuCustomItems = dto.MenuItems;
                    AppState.NotifyLayoutChange();

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
                    Title = "Error on Credencial download",
                    Detail = ex.Message
                };
                LoadStatus = Globals.LoadStatusEnum.Failed;
            }
        }



    }


    public class CredencialsViewModel
    {
        private Shared.AppState AppState;
        public string? SearchTerm;
        public CredencialListDTO Model = new();
        private List<CredencialModel> items = new();

        public Globals.LoadStatusEnum LoadStatus = Globals.LoadStatusEnum.Empty;
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


        public async void Fetch(HttpClient http)
        {
            LoadStatus = Globals.LoadStatusEnum.Loading;
            AppState.ProblemDetails = null;
            try
            {
                var url = AppState.ApiUrl("Credencials");
                var response = await http.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    Model = Newtonsoft.Json.JsonConvert.DeserializeObject<CredencialListDTO>(responseText);
                    items = Model.Items;
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
                    Title = "Error on Credencials download",
                    Detail = ex.Message
                };
                LoadStatus = Globals.LoadStatusEnum.Failed;
            }
        }
    }
}
