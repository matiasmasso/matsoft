using DTO;
using Spa.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace Spa.ViewModels
{
    public class ContactViewModel
    {
        private AppState AppState;
        private Guid Guid;
        public ContactModel Model;
        public Globals.LoadStatusEnum LoadStatus = Globals.LoadStatusEnum.Empty;
        public delegate void StateChange();
        public event StateChange? OnStateChange;


        public ContactViewModel(AppState appstate, Guid guid)
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
                var url = AppState.ApiUrl("Contact",Guid.ToString());
                var response = await http!.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    var dto = Newtonsoft.Json.JsonConvert.DeserializeObject<ContactDTO>(responseText);
                    Model = dto.Contact;
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
                    Title = "Error on Escripturas download",
                    Detail = ex.Message
                };
                LoadStatus = Globals.LoadStatusEnum.Failed;
            }
        }

    }
}
