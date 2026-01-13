using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Integracions.Verifactu
{
    public class VfInvoiceResult
    {
        public int ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public ReturnData Return { get; set; }

        public class ReturnData
        {
            public string SellerID { get; set; }
            public string CompanyName { get; set; }
            public string IndustryClassificationID { get; set; }
            public string BusinessGroupID { get; set; }
            public string BatchID { get; set; }
            public string InvoiceID { get; set; }
            public string Status { get; set; }
            public string InvoiceType { get; set; }
            public string RectificationType { get; set; }
            public bool IsInvoiceFix { get; set; }
            public bool IsRejected { get; set; }
            public DateTime InvoiceDate { get; set; }
            public DateTime? OperationDate { get; set; }
            public DateTime PostingDate { get; set; }
            public string PostingYear { get; set; }
            public string PostingMonth { get; set; }
            public string PostingQuarter { get; set; }
            public DateTime ValueDate { get; set; }
            public DateTime TaxDate { get; set; }
            public string? RelatedPartyID { get; set; }
            public string? RelatedPartyName { get; set; }
            public string? RelatedPartyIDType { get; set; }
            public string? CountryID { get; set; }
            public string? DocumentCurrencyID { get; set; }
            public string? CompanyCurrencyID { get; set; }
            public string? BusinessGroupCurrencyID { get; set; }
            public decimal TotalAmount { get; set; }
            public string? ExternKey { get; set; }
            public string? Text { get; set; }
            public string? StatusResponse { get; set; }
            public string? ErrorCode { get; set; }
            public string? ErrorDescription { get; set; }
            public string? CSV { get; set; }
            public List<TaxItem>? TaxItems { get; set; }
            public List<object>? RectificationItems { get; set; }
            public decimal RectificationTaxBase { get; set; }
            public decimal RectificationTaxAmount { get; set; }
            public decimal RectificationTaxAmountSurcharge { get; set; }
            public string QrCode { get; set; }
            public string Xml { get; set; }
            public string Response { get; set; }
            public string QrCodeUrl { get; set; }
            public string ValidationUrl { get; set; }
            public string InstanceID { get; set; }
            public string Version { get; set; }
            public string UserID { get; set; }
            public DateTime Created { get; set; }
            public string SessionID { get; set; }
            public string TransactionID { get; set; }
        }

        public class TaxItem
        {
            public string SellerID { get; set; }
            public string BatchID { get; set; }
            public string InvoiceID { get; set; }
            public string PostingYear { get; set; }
            public object Tax { get; set; }
            public string TaxScheme { get; set; }
            public object TaxType { get; set; }
            public string TaxException { get; set; }
            public decimal TaxBase { get; set; }
            public decimal TaxRate { get; set; }
            public decimal TaxAmount { get; set; }
            public decimal TaxRateSurcharge { get; set; }
            public decimal TaxAmountSurcharge { get; set; }
            public int ItemPosition { get; set; }
            public object InstanceID { get; set; }
            public object Version { get; set; }
            public object UserID { get; set; }
            public DateTime Created { get; set; }
            public object SessionID { get; set; }
            public object TransactionID { get; set; }
        }
    }
}
