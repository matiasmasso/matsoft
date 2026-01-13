using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace Spa.ViewModels
{
    public class VehicleViewModel
    {
        private Shared.AppState AppState;
        public VehicleModel Model;

        public Globals.LoadStatusEnum LoadStatus = Globals.LoadStatusEnum.Empty;
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


        public async void Fetch(HttpClient http)
        {
            LoadStatus = Globals.LoadStatusEnum.Loading;
            AppState.ProblemDetails = null;
            try
            {
                var url = AppState.ApiUrl("Vehicle",Model.Guid.ToString());
                var response = await http!.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    Model = Newtonsoft.Json.JsonConvert.DeserializeObject<VehicleModel>(responseText);
                    AppState.SetPrivateMenuCustomItems();
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
                    Title = "Error on reading Vehicle",
                    Detail = ex.Message
                };
                LoadStatus = Globals.LoadStatusEnum.Failed;
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
