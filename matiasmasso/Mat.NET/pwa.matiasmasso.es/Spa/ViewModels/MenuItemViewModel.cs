using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace Spa.ViewModels
{
    public class MenuItemViewModel
    {
        private Shared.AppState AppState;
        public Guid Guid;
        public MenuItemModel? Model;
        public List<GuidNom> ParentCandidates = new();
        public GuidNom? Parent;

        public Globals.LoadStatusEnum LoadStatus = Globals.LoadStatusEnum.Empty;
        public delegate void StateChange();
        public event StateChange? OnStateChange;

        public MenuItemViewModel(Shared.AppState appstate, Guid? guid)
        {
            AppState = appstate;
            if (guid == null)
            {
                Model = new MenuItemModel();
                Guid = Model.Guid;
            } else
                Guid = (Guid)guid!;
        }

        public async void Fetch(HttpClient http)
        {
            LoadStatus = Globals.LoadStatusEnum.Loading;
            AppState.ProblemDetails = null;
            try
            {
                var url = AppState.ApiUrl("MenuItem/fetch",Guid.ToString());
                var response = await http!.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();

                    var dto = Newtonsoft.Json.JsonConvert.DeserializeObject<MenuItemDTO>(responseText);
                    Model = dto.Model;
                    ParentCandidates = dto.ParentCandidates;
                    if (Model!.Parent != null) 
                        Parent = ParentCandidates.FirstOrDefault(x => x.Guid.Equals(Model!.Parent));
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
                    Title = "Error on reading the MenuItem",
                    Detail = ex.Message
                };
                LoadStatus = Globals.LoadStatusEnum.Failed;
            }
        }


        public async Task<bool> Update()
        {
            Model!.Parent = Parent == null ? null : Parent.Guid;
            var url = AppState.ApiUrl("MenuItem");
            HttpResponseMessage response;
            if(Model.IsNew)
                response = await new HttpClient().PostAsJsonAsync(url, Model);
            else
                response = await new HttpClient().PutAsJsonAsync(url, Model);

            return ((int)response.StatusCode == 200);
        }
    }
}
