using DTO;
using Client.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace Client.ViewModels
{
    public class ContactViewModel
    {
        private AppState AppState;
        private Guid Guid;
        public ContactModel Model;
        public Shared.AppState.StatusEnum LoadStatus = Shared.AppState.StatusEnum.Empty;
        public delegate void StateChange();
        public event StateChange? OnStateChange;


        public ContactViewModel(AppState appstate, Guid guid)
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
                var url = AppState.ApiUrl("Contact",Guid.ToString());
                var response = await AppState.Http!.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    var dto = Newtonsoft.Json.JsonConvert.DeserializeObject<ContactDTO>(responseText);
                    Model = dto.Contact;
                    AppState.Layout.PrivateMenuCustomItems = dto.MenuItems;
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
                    Title = "Error on Escripturas download",
                    Detail = ex.Message
                };
                LoadStatus = Shared.AppState.StatusEnum.Failed;
            }
        }

    }
}
