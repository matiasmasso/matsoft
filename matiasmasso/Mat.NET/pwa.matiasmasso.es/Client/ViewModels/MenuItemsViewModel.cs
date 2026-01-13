using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace Client.ViewModels
{
    public class MenuItemsViewModel
    {
        private Shared.AppState AppState;
        public string SearchTerm;
        public List<MenuItemModel> Model = new();

        public Shared.AppState.StatusEnum LoadStatus = Shared.AppState.StatusEnum.Empty;
        public delegate void StateChange();
        public event StateChange? OnStateChange;

        public MenuItemsViewModel(Shared.AppState appstate)
        {
            AppState = appstate;
        }

        public List<MenuItemModel> Items()
        {
            if (string.IsNullOrEmpty(SearchTerm))
                return Model;
            else
                 return Model.Where(x => x.Matches(SearchTerm)).ToList();
        }


        public async void Fetch()
        {
            LoadStatus = Shared.AppState.StatusEnum.Loading;
            AppState.ProblemDetails = null;
            try
            {
                var url = AppState.ApiUrl("MenuItems");
                var response = await AppState.Http!.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    Model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MenuItemModel>>(responseText);
                    AppState.NotifyLayoutChange();
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
                    Title = "Error on MenuItems download",
                    Detail = ex.Message
                };
                LoadStatus = Shared.AppState.StatusEnum.Failed;
            }
        }
    }
}
