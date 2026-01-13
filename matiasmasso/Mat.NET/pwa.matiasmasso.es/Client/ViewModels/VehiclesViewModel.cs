using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Formatting;
using System.Net.Http.Json;

namespace Client.ViewModels
{
    public class VehiclesViewModel
    {
        private Shared.AppState AppState;
        public string SearchTerm;
        private bool ShowObsolets = false;
        public VehicleListDTO Model = new();
        private List<VehicleListDTO.Item> items = new();

        public Shared.AppState.StatusEnum LoadStatus = Shared.AppState.StatusEnum.Empty;
        public delegate void StateChange();
        public event StateChange? OnStateChange;


        public VehiclesViewModel(Shared.AppState appstate)
        {
            AppState = appstate;
            Shared.AppState.OnToggleRequest += Toggle;
        }

        private void Toggle(string command)
        {
            if (command == "ShowObsolets")
                ShowObsolets = !ShowObsolets;
            OnStateChange!.Invoke();
        }
        public List<VehicleListDTO.Item> Items()
        {
            var retval = ShowObsolets ? items : items.Where(x => x.Baixa == null || x.Baixa > DateTime.Today).ToList();
            if (!string.IsNullOrEmpty(SearchTerm))
                 retval = retval.Where(x => x.Matches(SearchTerm)).ToList();
            return retval;
        }


        public async void Fetch()
        {
            LoadStatus = Shared.AppState.StatusEnum.Loading;
            AppState.ProblemDetails = null;
            try
            {
                var url = AppState.ApiUrl("Vehicles");
                var response = await AppState.Http!.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    Model = Newtonsoft.Json.JsonConvert.DeserializeObject<VehicleListDTO>(responseText);
                    items = Model.Items;
                    AppState.SetPrivateMenuCustomItems(MenuItems());
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
                    Title = "Error on Vehicles download",
                    Detail = ex.Message
                };
                LoadStatus = Shared.AppState.StatusEnum.Failed;
            }
        }

        private List<MenuItemModel> MenuItems()
        {
            var retval = new List<MenuItemModel>();
            retval.Add(new MenuItemModel()
            {
                Caption = new LangTextModel("Ver obsoletos", "Veure obsolets", "See outdated"),
                Mode = MenuItemModel.Modes.Toggle,
                Action = "ShowObsolets"
            });
            return retval;
        }
    }
}
