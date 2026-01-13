using Services;
using DTO;

namespace Web.Services
{
    public class AppStateService
    {
        private HttpClient http;
        public DateTime FchFrom { get; set; } = DateTime.Now;
        public EmpModel.EmpIds EmpId { get; set; }

        public bool UseLocalApi { get; set; } 

        public AppStateService(HttpClient http)
        {
            this.http = http;
            EmpId = EmpModel.EmpIds.MatiasMasso;
        }

        public string ApiUrl(params string[] segments)=>HttpService.ApiUrl(UseLocalApi, segments);
        public async Task<T?> GetAsync<T>(params string[] segments) => await HttpService.Get2Async<T>(http, UseLocalApi, segments) ?? throw new ArgumentException();

        public async Task<T> PostAsync<U, T>(U payload, params string[] segments) => await HttpService.Post2Async<U,T>(http, UseLocalApi, payload, segments) ?? throw new ArgumentException();

        public async Task<T> PostMultipartAsync<U, T>(U payload, DocfileModel? docfile, params string[] segments)
        {
            var docfiles = new List<DocfileModel>();
            if (docfile != null) docfiles.Add(docfile);
            return await PostMultipartAsync<U,T>(payload, docfiles, segments);
        }
        public async Task<T> PostMultipartAsync<U, T>(U payload, List<DocfileModel> docfiles, params string[] segments) => await HttpService.PostMultipartAsync<U, T>(http, UseLocalApi, payload, docfiles, segments) ?? throw new ArgumentException();

    }
}
