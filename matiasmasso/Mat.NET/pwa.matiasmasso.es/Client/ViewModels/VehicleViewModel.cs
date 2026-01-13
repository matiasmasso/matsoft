using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace Client.ViewModels
{
    public class VehicleViewModel
    {
        private Shared.AppState AppState;
        public VehicleModel Model;

        public Shared.AppState.StatusEnum LoadStatus = Shared.AppState.StatusEnum.Empty;
        public delegate void StateChange();
        public event StateChange? OnStateChange;

        public VehicleViewModel(Shared.AppState appstate, VehicleModel model)
        {
            //from editor, already fetched
            AppState = appstate;
            Model = model;
        }

        public VehicleViewModel(Shared.AppState appstate, Guid guid)
        {
            //from landing page, to fetch
            AppState = appstate;
            Model = new VehicleModel(guid);
        }


        public async void Fetch()
        {
            LoadStatus = Shared.AppState.StatusEnum.Loading;
            AppState.ProblemDetails = null;
            try
            {
                var url = AppState.ApiUrl("Vehicle",Model.Guid.ToString());
                var response = await AppState.Http!.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    Model = Newtonsoft.Json.JsonConvert.DeserializeObject<VehicleModel>(responseText);
                    AppState.SetPrivateMenuCustomItems();
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
                    Title = "Error on reading Vehicle",
                    Detail = ex.Message
                };
                LoadStatus = Shared.AppState.StatusEnum.Failed;
            }
        }

        public async Task<bool> Submit()
        {
            return false;
        }

        private async Task<bool> Update(VehicleModel item)
        {
            return false;
        }
        private async Task<bool> Create(VehicleModel item)
        {
            return false;
        }

        public async Task<bool> Delete()
        {
            return false;
        }

    }
}
