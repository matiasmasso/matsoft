using Cash.Components;
using Cash.Helpers;
using DTO;
using DTO.Integracions.Verifactu;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static DTO.Helpers.JsonConverters;
using static System.Net.WebRequestMethods;

namespace Cash.Services
{
    public class VerifactuService // API de IreneSolutions
    {
        private readonly HttpClient _httpClient;
        private readonly ApiService _apiService;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            //PropertyNamingPolicy = JsonNamingPolicy.PascalCase,
            //WriteIndented = false,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        public VerifactuService(HttpClient httpClient, ApiService apiService)
        {
            _httpClient = httpClient;
            _apiService = apiService;
            _jsonOptions.Converters.Add(new DateOnlyJsonConverter());
        }


        // Pujar factura a Verifactu
        public async Task SendAsync(CustomerInvoiceModel invoice)
        {
            var vfInvoice = invoice.ToVfInvoice();
            var json = JsonSerializer.Serialize(vfInvoice, _jsonOptions);
            using var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            //var IreneCreateInvoiceUrl= "https://facturae.irenesolutions.com:8050/Kivu/Taxes/Verifactu/Invoices/Create"; //entorn de proves
            var IreneCreateInvoiceUrl = "https://verifactu.irenesolutions.com:8026/Kivu/Taxes/Verifactu/Invoices/Create"; //entorn de producció

            using var response = await _httpClient.PostAsync(IreneCreateInvoiceUrl, content);

            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<VfInvoiceResult>(responseBody, _jsonOptions);
                var statusMessage = result?.ResultMessage ?? "Invoice created successfully.";
                if (string.IsNullOrEmpty(invoice.VfCsv))
                {
                    StringBuilder sb = new StringBuilder("CSV empty." + statusMessage);
                    if (!string.IsNullOrEmpty(result?.Return.ErrorDescription)) sb.AppendLine(result?.Return.ErrorDescription);
                    throw new Exception(sb.ToString());
                }
                invoice.VfCsv = result?.Return.CSV;
                invoice.VfQr = result?.Return.QrCode;
                var res = await _apiService.GetAsync<bool>("CustomerInvoice/setCsv", invoice.Guid.ToString(), invoice.VfCsv);
                if (!res) throw new Exception($"No s'ha pogut desar el Csv {invoice.VfCsv} a la factura");
                await RebuildPdfAsync(invoice);
            }
            else
            {
                throw new Exception($"Error creating invoice: {(int)response.StatusCode} {response.ReasonPhrase}. Response: {responseBody}");
            }

        }

        public async Task RebuildPdfAsync(CustomerInvoiceModel item)
        {
            if (!string.IsNullOrEmpty(item.VfCsv) && string.IsNullOrEmpty(item.VfQr)) 
                item.VfQr = await GetQrBase64Async(item);

            var pdf = await _apiService.PostAsync<CustomerInvoiceModel?, Byte[]>(item, "CustomerInvoice/Pdf");
            var media = new Media(Media.MimeCods.Pdf, pdf);
            item.Docfile = DocfileHelper.CreateDocfile(media, 350, 400);
            await _apiService.PostMultipartAsync<CustomerInvoiceModel, bool>(item.Docfile, item, "customerInvoice/rebuildPdf");
        }


        // Método para obtener el base64 del código QR
        public async Task<string?> GetQrBase64Async(CustomerInvoiceModel value)
        {
            string apiUrl = $"https://verifactu.irenesolutions.com:8026/Kivu/Taxes/Verifactu/Invoices/GetQrCode";

            var json = JsonSerializer.Serialize(value.ToVfInvoice(), _jsonOptions);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponse = await _httpClient.PostAsync(apiUrl, content);

            if (httpResponse.IsSuccessStatusCode)
            {
                var response = await httpResponse.Content.ReadFromJsonAsync<VfQrCodeResponse>(_jsonOptions);
                if (response != null && !string.IsNullOrEmpty(response.Return))
                {
                    return response.Return;
                }
            }
            return null;
        }
    }

}
