using DTO;

namespace Shop4moms.Services
{
    public class AppStateService
    {
        public EmpModel.EmpIds EmpId { get; set; } 

        private HttpClient http;

        public AppStateService(HttpClient http)
        {
            this.http = http;
            EmpId = EmpModel.EmpIds.MatiasMasso;
        }


        public async Task<T?> GetAsync<T>(params string[] segments) => await HttpService.Get2Async<T>(http, segments) ?? throw new ArgumentException();
        public async Task<T> PostAsync<U, T>(U payload, params string[] segments) => await HttpService.Post2Async<U, T>(http, payload, segments) ?? throw new ArgumentException();
        public async Task<T> PostMultipartAsync<U, T>(U payload, List<DocFileModel> docfiles, params string[] segments) => await HttpService.PostMultipartAsync<U, T>(http, payload, docfiles, segments) ?? throw new ArgumentException();

    }
}
