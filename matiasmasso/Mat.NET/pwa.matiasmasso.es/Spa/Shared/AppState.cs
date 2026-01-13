using Microsoft.AspNetCore.Mvc;
using DTO;
using Spa.ViewModels;
using System.Net.Http.Headers;

namespace Spa.Shared
{
    public class AppState
    {
        public UserDTO? User;
        public LangDTO? Lang;
        //public HttpClient Http;
        public LayoutDTO? Layout { get; set; } = new();
        public bool UseLocalApi = false;
        public Stack<DTO.BrowseHistory> History = new Stack<DTO.BrowseHistory>();
        public ProblemDetails? ProblemDetails { get; set; }

        //public delegate void InvokeAsync();
        public delegate void StateChange();
        public event StateChange? OnStateChange;

        public AppState()
        {
            Lang = LangDTO.Default();
        }

        public void Init(HttpClient http, Guid? userToken, LangDTO? lang)
        {
                //Http!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiKey", userToken.ToString());
               // Http!.DefaultRequestHeaders.Add("Lang", lang!.ToString());
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
            //UseLocalApi = true;
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
            //OnStateChange?.Invoke();
        }

    }

}
