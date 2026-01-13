using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Integracions.Redsys
{
    public class Request
    {
        public string? Ds_MerchantParameters { get; set; }
        public string? Ds_SignatureVersion { get; set; }
        public string? Ds_Signature { get; set; }

        public string? TpvOrderNum() => TpvOrderNum(Ds_MerchantParameters);

        public static string? TpvOrderNum(string? ds_MerchantParameters)
        {
            var paramsDictionary = DTO.Helpers.CryptoHelper.FromUrlFriendlyBase64Json(ds_MerchantParameters)!;
            var retval = paramsDictionary?["Ds_Order"];
            return retval;
        }

        public Dictionary<string, string> FormData()
        {
            return new Dictionary<string, string> {
                    {"Ds_MerchantParameters", Ds_MerchantParameters ?? "" },
                    {"Ds_Signature", Ds_Signature ?? "" },
                    {"Ds_SignatureVersion", Ds_SignatureVersion ?? "" }
                };
        }
    }
}
