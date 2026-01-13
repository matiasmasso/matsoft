using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace Web2.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            //RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            //Severity	Code	Description	Project	File	Line	Suppression State
            //Error CS0433  The type 'Activity' exists in both 'IronBarCode, Version=2023.10.0.1, Culture=neutral, PublicKeyToken=b971bb3971bdf306' and 'System.Diagnostics.DiagnosticSource, Version=7.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'    Web2 C:\Users\matia\Source\Workspaces\MMO\pwa.matiasmasso.es\Web2\Pages\Error.cshtml.cs  24  Active

        }
    }
}