using Components;
using DTO;
using DTO.Helpers;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Text;

namespace Web.Services
{
    // Visa 4548812049400004 12/20 123
    public class TpvServiceOld
    {
        public const string managementUrl = "https://sis-t.redsys.es:25443/canales/";
        public enum Environments
        {
            Production,
            Development
        }
        public enum Ids
        {
            MatiasMasso,
            Web
        }

        public class Tpv
        {
            public Ids Id { get; set; }
            public Environments Environment { get; set; }

            public string GatewayUrlDevelopment { get; set; } = "https://sis-t.redsys.es:25443/sis/realizarPago";
            public string GatewayUrlProduction { get; set; } = "";
            public string? PrivateKeyDevelopment { get; set; }
            public string? PrivateKeyProduction { get; set; }
            public string SignatureVersion { get; set; } = "HMAC_SHA256_V1";
            public string? MerchantCode { get; set; }
            public int? MerchantTerminal { get; set; }
            public string TransactionType { get; set; } = "0";

            public string? MerchantUrl { get; set; }
            public string? UrlOk { get; set; }
            public string? UrlKo { get; set; }


            public Tpv(Ids id, Environments environment)
            {
                Id = id;
                Environment = environment;

                switch (id)
                {
                    case Ids.MatiasMasso:
                        MerchantCode = "22425573";
                        MerchantTerminal = 1;
                        MerchantUrl = "";
                        UrlOk = "";
                        UrlKo = "";
                        PrivateKeyDevelopment = "";
                        PrivateKeyProduction = "";
                        break;
                    case Ids.Web:
                        MerchantCode = "357592922";
                        MerchantTerminal = 1;
                        MerchantUrl = "https://4moms.matiasmasso.es/tpv/feed";
                        UrlOk = "https://4moms.matiasmasso.es/tpv/Ok";
                        UrlKo = "https://4moms.matiasmasso.es/tpv/KO";
                        PrivateKeyDevelopment = "sq7HjrUOBfKmC576ILgskD5srU870gJ7";
                        PrivateKeyProduction = "";
                        break;
                }
            }



            public string Gateway() => Environment == Environments.Development ? GatewayUrlDevelopment : GatewayUrlProduction;
            public string PrivateKey() => Environment == Environments.Development ? PrivateKeyDevelopment! : PrivateKeyProduction!;

            public Request Request(string tpvOrderNum, string basketOrderNum, decimal amount, string langISO)
            {
                var merchantParams = new MerchantParameters(this, tpvOrderNum, amount, langISO, basketOrderNum);
                var ds_MerchantParameters = merchantParams.Base64JsonEncoded();
                var ds_Signature = CreateMerchantSignature(ds_MerchantParameters, tpvOrderNum, PrivateKey());
                return new Request
                {
                    Ds_MerchantParameters = ds_MerchantParameters,
                    Ds_Signature = ds_Signature,
                    Ds_SignatureVersion = SignatureVersion
                };
            }

            public bool IsValidSignature(Request response)
            {
                var computedHash = CreateMerchantSignature(response.Ds_MerchantParameters, response.OrderNum(), PrivateKey());
                var normalizedResponseSignature = response.Ds_Signature?.Replace("_", "/").Replace("-", "+");
                return computedHash == normalizedResponseSignature;
            }

            public DTO.Integracions.Redsys.TpvLog BookRequest(ShoppingBasketModel basket)
            {
               return new DTO.Integracions.Redsys.TpvLog()
                {
                    User = basket.User!,
                    Mode = DTO.Integracions.Redsys.Common.Modes.Consumer,
                    Ds_Amount = (basket.Cash() * 100).ToString("F0"),
                    Ds_MerchantCode = MerchantCode!,
                    Ds_Terminal = MerchantTerminal,
                    Ds_ProductDescription = string.Format("4moms ticket {0} {1}", basket.OrderNum, basket.Fullnom() ?? ""),
                    Ds_ConsumerLanguage = DTO.Integracions.Redsys.Common.LangCode(basket.Lang!.Tag()),
                    Titular = DTO.CustomerModel.Wellknown(CustomerModel.Wellknowns.consumidor)!.Guid,
                    SrcGuid = basket.Guid
               };
            }

            public string HtmlReport(Request response, Components.ProblemDetails? problemDetails = null)
            {
                var paramsDictionary = DTO.Helpers.CryptoHelper.FromUrlFriendlyBase64Json(response.Ds_MerchantParameters)!;
                var computedHash = CreateMerchantSignature(response.Ds_MerchantParameters, response.OrderNum(), PrivateKey());
                var normalizedResponseSignature = response.Ds_Signature?.Replace("_", "/");
                var sb = new StringBuilder();
                sb.AppendLine("<style>");
                sb.AppendLine("table.Rpt {border-collapse: collapse;max-width:600px;}");
                sb.AppendLine("table.Rpt tr:nth-child(even){background-color: #f2f2f2;}");
                sb.AppendLine("table.Rpt tr td {border: 1px solid #ddd; padding: 8px;}");
                sb.AppendLine("table.Rpt tr td:first-child {white-space:nowrap;}");
                sb.AppendLine("</style>");

                sb.AppendLine("<table class='Rpt'>");
                if (problemDetails?.Exception != null)
                {
                    sb.AppendLine("<tr><td>" + "Ex.message" + "</td><td>" + problemDetails?.Exception.Message + "</td></tr>");
                    if (problemDetails?.Exception.InnerException != null)
                    {
                        sb.AppendLine("<tr><td>" + "Ex.InnerException" + "</td><td>" + problemDetails?.Exception.InnerException.Message + "</td></tr>");
                    }
                }
                else if (problemDetails != null)
                {
                    sb.AppendLine("<tr><td>" + "problemDetails title" + "</td><td>" + problemDetails.Title + "</td></tr>");
                    if (!string.IsNullOrEmpty(problemDetails.Details()))
                    {
                        sb.AppendLine("<tr><td>" + "problemDetails details" + "</td><td>" + problemDetails.Details() + "</td></tr>");
                    }
                }
                sb.AppendLine("<tr><td>" + "IsValidSignature" + "</td><td>" + (IsValidSignature(response) ? "Yes" : "No") + "</td></tr>");
                sb.AppendLine("<tr><td>" + "Encoded parameters" + "</td><td style='ms-word-break:break-word;'>" + response.Ds_MerchantParameters + "</td></tr>");
                sb.AppendLine("<tr><td>" + "Signature Version" + "</td><td>" + response.Ds_SignatureVersion + "</td></tr>");
                sb.AppendLine("<tr><td>" + "Signature" + "</td><td style='ms-word-break:break-word;'>" + normalizedResponseSignature + "</td></tr>");
                sb.AppendLine("<tr><td>" + "Computed hash" + "</td><td style='ms-word-break:break-word;'>" + computedHash + "</td></tr>");
                foreach (var p in paramsDictionary ?? new Dictionary<string, string>())
                {
                    sb.AppendLine("<tr><td>" + p.Key + "</td><td>" + p.Value + "</td></tr>");
                }
                sb.Append("</table>");

                return sb.ToString();
            }

        }

        public class Request
        {
            public string? Ds_MerchantParameters { get; set; }
            public string? Ds_SignatureVersion { get; set; }
            public string? Ds_Signature { get; set; }

            public string OrderNum() => TpvServiceOld.OrderNum(Ds_MerchantParameters);

            public Dictionary<string, string> FormData()
            {
                return new Dictionary<string, string> {
                    {"Ds_MerchantParameters", Ds_MerchantParameters ?? "" },
                    {"Ds_Signature", Ds_Signature ?? "" },
                    {"Ds_SignatureVersion", Ds_SignatureVersion ?? "" }
                };
            }
        }

        public class MerchantParameters
        {
            public string? DS_MERCHANT_AMOUNT { get; set; }
            public string? DS_MERCHANT_ORDER { get; set; }
            public string? DS_PRODUCTDESCRIPTION { get; set; }
            public string? DS_MERCHANT_CONSUMERLANGUAGE { get; set; }
            public string DS_MERCHANT_CURRENCY { get; set; } = "978"; //Eur
            public string DS_MERCHANT_MERCHANTCODE { get; set; }
            public string DS_MERCHANT_MERCHANTURL { get; set; }
            public string DS_MERCHANT_TERMINAL { get; set; }
            public string DS_MERCHANT_TRANSACTIONTYPE { get; set; }
            public string DS_MERCHANT_URLKO { get; set; }
            public string DS_MERCHANT_URLOK { get; set; }

            public MerchantParameters(Tpv tpv, string orderNum, decimal amount, string langISO, string concept)
            {
                DS_MERCHANT_AMOUNT = (amount * 100).ToString("F0");
                DS_MERCHANT_ORDER = orderNum;
                DS_PRODUCTDESCRIPTION = concept;
                DS_MERCHANT_CONSUMERLANGUAGE = DTO.Integracions.Redsys.Common.LangCode(langISO);
                DS_MERCHANT_MERCHANTCODE = tpv.MerchantCode ?? "";
                DS_MERCHANT_MERCHANTURL = tpv.MerchantUrl ?? "";
                DS_MERCHANT_TERMINAL = tpv.MerchantTerminal.ToString() ?? "";
                DS_MERCHANT_TRANSACTIONTYPE = tpv.TransactionType;
                DS_MERCHANT_URLOK = tpv.UrlOk ?? "";
                DS_MERCHANT_URLKO = tpv.UrlKo ?? "";
            }

            public string Base64JsonEncoded()
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(this);
                var retval = DTO.Helpers.CryptoHelper.EncodeTo64(json);
                return retval;
            }

        }

        public static string CreateMerchantSignature(string? ds_MerchantParameters, string orderNum, string privateKey)
        {
            var retval = string.Empty;
            if (ds_MerchantParameters != null)
            {
                //var orderNum = paramsDictionary?["Ds_Order"];
                if (orderNum != null)
                {
                    // Decode key to byte[]
                    byte[] SignatureKeyBytes = Convert.FromBase64String(privateKey);

                    // Calculate derivated key by encrypting with 3DES the "DS_MERCHANT_ORDER" with decoded key 
                    byte[] DerivatedKeyBytes = DTO.Helpers.CryptoHelper.Encrypt3DES(orderNum, SignatureKeyBytes);

                    // Calculate HMAC SHA256 with Encoded base64 JSON string using derivated key calculated previously
                    byte[] resultBytes = DTO.Helpers.CryptoHelper.GetHMACSHA256(ds_MerchantParameters, DerivatedKeyBytes);

                    retval = Convert.ToBase64String(resultBytes);
                }
            }
            return retval;
        }

        public static string OrderNum(string? ds_MerchantParameters)
        {
            var paramsDictionary = DTO.Helpers.CryptoHelper.FromUrlFriendlyBase64Json(ds_MerchantParameters)!;
            var retval = paramsDictionary["Ds_Order"];
            return retval;
        }


    }
}
