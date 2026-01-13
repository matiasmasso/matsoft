
using DTO;
using Microsoft.JSInterop;
using static System.Net.WebRequestMethods;


namespace Web.Services
{

    public class PdfService : IPdfService
    {
        private readonly IJSRuntime JS;
        private readonly ApiService _apiservice;

        public PdfService(IJSRuntime js, ApiService apiService)
        {
            JS = js;
            _apiservice = apiService;
        }


        public async Task OpenLineageBranchesPdfAsync(List<AncestorModel> left, List<AncestorModel> right)
        {
            var request = new LineageBranchesRequest
            {
                Left = left,
                Right = right
            };

            var pdfBytes = await _apiservice.PostAsync<LineageBranchesRequest, Byte[]>(request, "person/lineage");

            var base64 = Convert.ToBase64String(pdfBytes);
            var url = $"data:application/pdf;base64,{base64}";

            await JS.InvokeVoidAsync("open", url, "_blank");
        }
    }
}
