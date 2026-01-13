using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Integracions.Redsys
{
    public class TpvLog : BaseGuid, IModel
    {
        public UserModel? User { get; set; }
        public Common.Modes Mode { get; set; }

        public Guid? SrcGuid { get; set; }

        public Guid? Titular { get; set; }
        public string? Ds_Date { get; set; }
        public string? Ds_Hour { get; set; }
        public string? Ds_Amount { get; set; }
        public string Ds_Currency { get; set; } = "978";
        public string? Ds_Order { get; set; }
        public string? Ds_MerchantCode { get; set; }
        public int? Ds_Terminal { get; set; }
        public string? Ds_Signature { get; set; }
        public string? Ds_Response { get; set; }
        public string? Ds_MerchantData { get; set; }
        public string? Ds_ProductDescription { get; set; }
        public string? Ds_SecurePayment { get; set; }
        public string Ds_TransactionType { get; set; } = "0";
        public string? Ds_Card_Country { get; set; }
        public string? Ds_AuthorisationCode { get; set; }
        public string? Ds_ConsumerLanguage { get; set; }
        public string? Ds_Card_Type { get; set; }
        public string? Ds_ProcessedPayMethod { get; set; } // not used yet
        public string? Ds_ErrorCode { get; set; } 

        // response:
        public string? Ds_MerchantParameters { get; set; }
        public string? Ds_SignatureReceived { get; set; }
        public string? ErrDsc { get; set; }
        public DateTime FchCreated { get; set; }

        public TpvLog() : base() { }
        public TpvLog(Guid guid) : base(guid) { }

        public static TpvLog Factory(Request response)
        {
            var retval = new TpvLog();
            var paramsDictionary = DTO.Helpers.CryptoHelper.FromUrlFriendlyBase64Json(response.Ds_MerchantParameters);
            if (paramsDictionary != null)
            {
                if (paramsDictionary.ContainsKey("Ds_Date"))
                    retval.Ds_Date = paramsDictionary["Ds_Date"];
                if (paramsDictionary.ContainsKey("Ds_Hour"))
                    retval.Ds_Hour = paramsDictionary["Ds_Hour"];
                if (paramsDictionary.ContainsKey("Ds_Amount"))
                    retval.Ds_Amount = paramsDictionary["Ds_Amount"];
                if (paramsDictionary.ContainsKey("Ds_Currency"))
                    retval.Ds_Currency = paramsDictionary["Ds_Currency"];
                if (paramsDictionary.ContainsKey("Ds_Order"))
                    retval.Ds_Order = paramsDictionary["Ds_Order"];
                if (paramsDictionary.ContainsKey("Ds_MerchantCode"))
                    retval.Ds_MerchantCode = paramsDictionary["Ds_MerchantCode"];
                if (paramsDictionary.ContainsKey("Ds_Terminal"))
                    retval.Ds_Terminal = int.Parse(paramsDictionary["Ds_Terminal"]);
                //retval.Ds_Signature = paramsDictionary["Ds_Signature"];
                if (paramsDictionary.ContainsKey("Ds_Response"))
                    retval.Ds_Response = paramsDictionary["Ds_Response"];
                if (paramsDictionary.ContainsKey("Ds_ErrorCode"))
                    retval.Ds_ErrorCode = paramsDictionary["Ds_ErrorCode"];
                if (paramsDictionary.ContainsKey("Ds_MerchantData"))
                    retval.Ds_MerchantData = paramsDictionary["Ds_MerchantData"];
                //retval.Ds_ProductDescription = paramsDictionary["Ds_ProductDescription"];
                if (paramsDictionary.ContainsKey("Ds_SecurePayment"))
                    retval.Ds_SecurePayment = paramsDictionary["Ds_SecurePayment"];
                if (paramsDictionary.ContainsKey("Ds_TransactionType"))
                    retval.Ds_TransactionType = paramsDictionary["Ds_TransactionType"];
                if (paramsDictionary.ContainsKey("Ds_Card_Country"))
                    retval.Ds_Card_Country = paramsDictionary["Ds_Card_Country"];
                if (paramsDictionary.ContainsKey("Ds_AuthorisationCode"))
                    retval.Ds_AuthorisationCode = paramsDictionary["Ds_AuthorisationCode"];
                if (paramsDictionary.ContainsKey("Ds_ConsumerLanguage"))
                    retval.Ds_ConsumerLanguage = paramsDictionary["Ds_ConsumerLanguage"];
                if (paramsDictionary.ContainsKey("Ds_Card_Brand"))
                    retval.Ds_Card_Type = paramsDictionary["Ds_Card_Brand"];
                if (paramsDictionary.ContainsKey("Ds_ProcessedPayMethod"))
                    retval.Ds_ProcessedPayMethod = paramsDictionary["Ds_ProcessedPayMethod"];

                // response:
                retval.Ds_MerchantParameters = response.Ds_MerchantParameters;
                retval.Ds_SignatureReceived = response.Ds_Signature;
                retval.FchCreated = DateTime.Now;
            }

            return retval;
        }

        public bool Success() => Ds_Response == "0000" && !string.IsNullOrEmpty(Ds_AuthorisationCode);

        //used to build string at delivery document
        public string FormaDePago()
        {
            var lang = Redsys.Common.Lang(Ds_ConsumerLanguage);
            var pattern = lang.Tradueix("Operación {0} con tarjeta de crédito", "Operació {0} amb tarja de crèdit");
            var retval = string.Format(pattern, Ds_Order);
            return retval;
        }
        public string PropertyPageUrl() => Globals.PageUrl("to do", Guid.ToString());

    }

}
