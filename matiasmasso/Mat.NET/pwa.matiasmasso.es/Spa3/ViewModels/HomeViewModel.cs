using DTO;
using Spa3.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Spa3.ViewModels
{
    public class HomeViewModel
    {
        private AppState AppState;
        public HomeDTO? Model;
        public Globals.LoadStatusEnum LoadStatus { get; set; } = Globals.LoadStatusEnum.Empty;
        public event Action? OnStateChange;


        public HomeViewModel(AppState appState)
        {
            AppState = appState;
        }


        public async void Fetch(HttpClient http)
        {
                var lang = LangDTO.Esp();
                var url = Globals.ApiUrl("Home");
            try
            {
                var authState = await AppState.authenticationState;
                if ((authState.User.Identity?.IsAuthenticated ?? false) && authState.User.GetUserId() != null)
                {
                    http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiKey", authState.User.GetUserId().ToString());
                }
                var response = await http.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    Model = Newtonsoft.Json.JsonConvert.DeserializeObject<HomeDTO>(responseText);
                    LoadStatus = Globals.LoadStatusEnum.Loaded;
                    NotifyStateChanged();
                }
                else
                {
                    var x = response;
                }
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                // Its a timeout issue
                AppState.ProblemDetails = new ProblemDetails
                {
                    Title = "TimeOut Exception at HomeViewModel.Fetch with url: " + url + " and lang " + (AppState.Lang?.ToString() ?? "(NoLang)"),
                    Detail = ex.Message + ' ' + ex.StackTrace
                };
                LoadStatus = Globals.LoadStatusEnum.Failed;
                NotifyStateChanged();
            }
            catch (Exception ex)
            {
                AppState.ProblemDetails = new ProblemDetails
                {
                    Title = "Exception at HomeViewModel.Fetch with url: " + url + " and lang " + (AppState.Lang?.ToString() ?? "(NoLang)"),
                    Detail = ex.Message + ' ' + ex.StackTrace
                };
                LoadStatus = Globals.LoadStatusEnum.Failed;
                NotifyStateChanged();
            }
        }



        public async void Fetch2(HttpClient http)
        {
            LoadStatus = Globals.LoadStatusEnum.Loading;
            var url = Globals.ApiUrl("Home");
            try
            {
                if (!http.DefaultRequestHeaders.Contains("Lang"))
                    http.DefaultRequestHeaders.Add("Lang", AppState.Lang!.ToString());
                var response = await http.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    Model = Newtonsoft.Json.JsonConvert.DeserializeObject<HomeDTO>(responseText);
                    LoadStatus = Globals.LoadStatusEnum.Loaded;
                    NotifyStateChanged();
                }
                else
                {
                    AppState.ProblemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>() ?? new ProblemDetails();
                    LoadStatus = Globals.LoadStatusEnum.Failed;
                    NotifyStateChanged();
                }
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                // Its a timeout issue
                AppState.ProblemDetails = new ProblemDetails
                {
                    Title = "TimeOut Exception at HomeViewModel.Fetch with url: " + url + " and lang " + (AppState.Lang?.ToString() ?? "(NoLang)"),
                    Detail = ex.Message + ' ' + ex.StackTrace
                };
                LoadStatus = Globals.LoadStatusEnum.Failed;
                NotifyStateChanged();
            }
            catch (Exception ex)
            {
                AppState.ProblemDetails = new ProblemDetails
                {
                    Title = "Exception at HomeViewModel.Fetch with url: " + url + " and lang " + (AppState.Lang?.ToString() ?? "(NoLang)"),
                    Detail = ex.Message + ' ' + ex.StackTrace
                };
                LoadStatus = Globals.LoadStatusEnum.Failed;
                NotifyStateChanged();
            }
        }

        public bool IsLoaded() => LoadStatus == Globals.LoadStatusEnum.Loaded;
        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
