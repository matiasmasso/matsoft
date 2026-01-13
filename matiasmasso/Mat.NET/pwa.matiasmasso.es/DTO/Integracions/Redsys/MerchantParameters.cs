using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Integracions.Redsys
{
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
            DS_MERCHANT_TERMINAL = tpv.MerchantTerminal.ToString() ?? "1";
            DS_MERCHANT_TRANSACTIONTYPE = tpv.TransactionType.ToString() ?? "0";
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

}
