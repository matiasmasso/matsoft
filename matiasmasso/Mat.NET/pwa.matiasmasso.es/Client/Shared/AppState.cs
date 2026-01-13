using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using DTO;
using Client.ViewModels;
using System.Net.Http.Headers;

namespace Client.Shared
{
    public class AppState
    {
        public HttpClient? Http;
        public UserDTO? User;
        public LangDTO? Lang;
        public LayoutDTO Layout { get; set; } = new();
        public StatusEnum LoadStatus { get; set; } = StatusEnum.Empty;

        private bool isMenuVisible = false;
        public bool IsMenuVisible {
            get { return isMenuVisible; }
            set { 
                isMenuVisible = value;
                NotifyLayoutChange();
            }
        }


        public bool UseLocalApi = false;
        public Stack<DTO.BrowseHistory> History = new Stack<DTO.BrowseHistory>();
        public ProblemDetails? ProblemDetails { get; set; }

        public delegate void StateChange();
        public event StateChange? OnStateChange;


        public enum StatusEnum
        {
            Empty,
            Loading,
            Loaded,
            Failed
        }

        public void Init(HttpClient http, Guid? userToken, LangDTO? lang)
        {
            if(Http == null)
                Http = http;
            if(userToken != null)
                Http!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiKey", userToken.ToString());
            if (lang != null)
                Lang = lang;
                Http!.DefaultRequestHeaders.Add("Lang", lang!.ToString());
        }

        public void SetPublicMenuCustomItems(List<MenuItemModel>? values = null)
        {
            Layout.PublicMenuCustomItems = values == null ? new List<MenuItemModel>() : values;
            NotifyLayoutChange();
        }

        public void SetPrivateMenuCustomItems(List<MenuItemModel>? values = null)
        {
            Layout.PrivateMenuCustomItems = values == null ? new List<MenuItemModel>() : values;
            NotifyLayoutChange();
        }

        public string ApiUrl(params string[] segments)
        {
           UseLocalApi = true;
            var host = UseLocalApi ? "localhost:7198" : "api2.matiasmasso.es";
            var segmentList = new List<string>() { host };
            segmentList.AddRange(segments);
            var retval = string.Format("https://{0}", string.Join("/", segmentList));
            return retval;
        }

        public static event Action<string>? OnMenuActionRequest;
        public void NotifyMenuActionRequest(string command) => OnMenuActionRequest?.Invoke(command);


        public delegate void ToggleRequest(string command);
        public static event ToggleRequest? OnToggleRequest;
        public static void NotifyMenuToggleRequest(string command) => OnToggleRequest?.Invoke(command);
        
        
        public void NotifyLayoutChange()
        {
            OnStateChange?.Invoke();
        }


        public async void Fetch(Guid? userToken = null)
        {
            await FetchAsync(userToken);
        }

        public async Task FetchAsync(Guid? userToken = null)
        {
            LoadStatus = StatusEnum.Loading;
            ProblemDetails = null;
            try
            {
                string url = ApiUrl("Layout");
                var response = await Http!.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    Layout = Newtonsoft.Json.JsonConvert.DeserializeObject<LayoutDTO>(responseText);
                    LoadStatus = StatusEnum.Loaded;
                }
                else
                {
                    ProblemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>() ?? new ProblemDetails();
                    LoadStatus = StatusEnum.Failed;
                }
                NotifyLayoutChange();
            }
            catch (Exception ex)
            {
                ProblemDetails = new ProblemDetails
                {
                    Title = "Error on layout download",
                    Detail = ex.Message
                };
                LoadStatus = StatusEnum.Failed;
            }

        }

        public void LogOut()
        {
            Layout.User = null;
            NotifyLayoutChange();
        }


    }

}
