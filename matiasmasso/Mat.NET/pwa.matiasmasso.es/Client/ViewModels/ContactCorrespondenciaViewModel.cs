using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Formatting;
using System.Net.Http.Json;

namespace Client.ViewModels
{
    public class ContactCorrespondenciaViewModel
    {
        private HttpClient Http;
        private Shared.AppState AppState;
        public string SearchTerm;
        public ContactCorrespondenciaDTO Model = new();
        private Guid Contact;

        public Shared.AppState.StatusEnum LoadStatus = Shared.AppState.StatusEnum.Empty;
        public delegate void StateChange();
        public event StateChange? OnStateChange;

        public ContactCorrespondenciaViewModel(HttpClient http, Shared.AppState appstate, Guid contact)
        {
            Http = http;
            AppState = appstate;
            Contact = contact;
        }

        public List<CorrespondenciaDTO> Items()
        {
            if (string.IsNullOrEmpty(SearchTerm))
                return Model.Items;
            else
                 return Model.Items.Where(x => x.Matches(SearchTerm)).ToList();
        }


        public async void Fetch()
        {
            LoadStatus = Shared.AppState.StatusEnum.Loading;
            AppState.ProblemDetails = null;
            try
            {
                var url = AppState.ApiUrl("ContactCorrespondencia", Contact.ToString());
                var response = await Http.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    Model = Newtonsoft.Json.JsonConvert.DeserializeObject<ContactCorrespondenciaDTO> (responseText);
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
                    Title = "Error on reading Correspondence",
                    Detail = ex.Message
                };
                LoadStatus = Shared.AppState.StatusEnum.Failed;
            }
        }
    }
}
