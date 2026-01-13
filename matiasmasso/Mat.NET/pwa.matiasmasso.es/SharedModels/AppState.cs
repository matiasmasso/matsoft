using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
//using Newtonsoft.Json;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;

namespace SharedModels
{
    public class AppState
    {

        public readonly HttpClient Client;
        public static bool UseLocalApi = false;
        public static Statuses Status { get; private set; }
        public static Model Model { get; set; } = new Model();

        public static event Action? OnDataLoaded;

        private static event Action? RequestToFetch;
        public Stack<BrowseHistory> History = new Stack<BrowseHistory>();

        public ProblemDetails? ProblemDetails { get; set; }
        public enum Statuses
        {
            NotSet,
            Loading,
            Loaded,
            Error
        }

        public enum Tables
        {
            All,
            Persons,
            Docs,
            DocTargets,
            DocSrcs,
            DocRels,
            Locations,
            Professions,
            FirstNoms,
            Cognoms,
            Enlaces
        }


        public AppState()
        {
            RequestToFetch += FetchRequest;
            Client = new HttpClient();

        }

        private void NotifyStateChanged() => OnDataLoaded?.Invoke();


        public static bool IsLoaded()
        {
            var retval = false;
            if (Status == Statuses.Loaded)
                retval = true;
            else if (Status == Statuses.NotSet)
                RequestToFetch?.Invoke();
            return retval;
        }

        public async void FetchRequest()
        {
            //---------to deprecate
            //Status = Statuses.Loaded;
            //-----------
            await Fetch();
        }

        public async Task Fetch()
        {
            Status = Statuses.Loading;
            //Model = await Client.GetFromJsonAsync<Model>(ApiUrl("Model")) ?? new Model();
            ProblemDetails = null;
            var url = ApiUrl("Model");
            var response = await new HttpClient().GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                Model = Newtonsoft.Json.JsonConvert.DeserializeObject<Model>(responseText) ?? new Model();
                Status = Statuses.Loaded;
            }
            else
            {
                ProblemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>() ?? new ProblemDetails();
                Status = Statuses.Error;
            }

            NotifyStateChanged();
        }


        public static string ApiUrl(params string[] segments)
        {
            var retval = "";
            var host = UseLocalApi ? "localhost:7198" : "api2.matiasmasso.es"; // "matgenapi.azurewebsites.net";
            if (segments.Length > 0)
            {
                var segment = string.Join("/", segments);
                retval = string.Format("https://{0}/{1}", host, segment);
            }
            else
                retval = string.Format("https://{0}", host);
            return retval;
        }

        public static event Action<string>? OnMenuActionRequest;

        public void NotifyMenuActionRequest(string command) => OnMenuActionRequest?.Invoke(command);


    }
}
