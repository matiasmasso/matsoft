using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Formatting;
using System.Net.Http.Json;
using System.Text;

namespace Spa.ViewModels
{
    public class ContentViewModel
    {
        private Shared.AppState AppState;
        private string Catchall;
        private ContentDTO? Model;

        public Globals.LoadStatusEnum LoadStatus = Globals.LoadStatusEnum.Empty;
        public string? Title { get; set; }
        public string? Content { get; set; }

        public delegate void StateChange();
        public event StateChange? OnStateChange;


        public ContentViewModel(Shared.AppState appstate, string catchall)
        {
            AppState = appstate;
            Catchall = catchall;
        }

        public async void Fetch(HttpClient http)
        {
            LoadStatus = Globals.LoadStatusEnum.Loading;
            AppState.ProblemDetails = null;
            try
            {
                var url = AppState.ApiUrl("Content");
                var urlSegment = Catchall.Replace(".html", "");
                var dto = new ContentRequestDTO{ UrlSegment = urlSegment };
                var s = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
                var response = await http!.PostAsync(url, dto, new JsonMediaTypeFormatter());
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    Model = Newtonsoft.Json.JsonConvert.DeserializeObject<ContentDTO>(responseText);
                    Title = Model.Title;
                    Content = Model.Text;
                    LoadStatus = Globals.LoadStatusEnum.Loaded;
                    OnStateChange?.Invoke();
                }
                else
                {
                    AppState.ProblemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>() ?? new ProblemDetails();
                    LoadStatus = Globals.LoadStatusEnum.Failed;
                    OnStateChange?.Invoke();
                }
            }
            catch (Exception ex)
            {
                AppState.ProblemDetails = new ProblemDetails
                {
                    Title = "Error on content download",
                    Detail = ex.Message
                };
                LoadStatus = Globals.LoadStatusEnum.Failed;
            }

        }

    }
}
