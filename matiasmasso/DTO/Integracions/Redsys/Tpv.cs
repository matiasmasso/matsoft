using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Integracions.Redsys
{

    public class Tpv : BaseGuid, IModel
    {
        public EmpModel.EmpIds Emp { get; set; }
        public Ids Id { get; set; }
        public LangDTO Lang { get; set; }

        public string? GatewayUrlDevelopment { get; set; } = "https://sis-t.redsys.es:25443/sis/realizarPago";
        public string? GatewayUrlProduction { get; set; } = "https://sis.redsys.es/sis/realizarPago";
        public string? PrivateKeyDevelopment { get; set; }
        public string? PrivateKeyProduction { get; set; }
        public string SignatureVersion { get; set; } = "HMAC_SHA256_V1";
        public string? MerchantCode { get; set; }
        public int? MerchantTerminal { get; set; }
        public int? TransactionType { get; set; } = 0;

        public string? MerchantUrl { get; set; }
        public string? UrlOk { get; set; }
        public string? UrlKo { get; set; }
        public BancModel? Banc { get; set; }
        public Common.Environments Environment { get; set; } = Common.Environments.Development;

        public enum Ids
        {
            MatiasMasso,
            Shop4moms
        }

        public Tpv() : base() { }
        public Tpv(Guid guid) : base(guid) { }


        public Tpv(Ids id, DTO.Integracions.Redsys.Common.Environments environment, LangDTO lang)
        {
            Id = id;
            Environment = environment;
            Lang = lang;

            switch (id)
            {
                case Ids.MatiasMasso:
                    MerchantCode = "22425573";
                    MerchantTerminal = 1;
                    MerchantUrl = LocalizedUrl(id, LangDTO.Esp(), "tpv/feed");
                    UrlOk = LocalizedUrl(id, lang, "tpv/Ok");
                    UrlKo = LocalizedUrl(id, lang, "tpv/Ko");
                    PrivateKeyDevelopment = "sq7HjrUOBfKmC576ILgskD5srU870gJ7";
                    PrivateKeyProduction = "tkfxAgYmmhjrccMj4uRHtbKEOxDYGj9+";
                    Banc = BancModel.Wellknown(BancModel.Wellknowns.CaixaBank);
                    break;
                case Ids.Shop4moms:
                    MerchantCode = "357592922";
                    MerchantTerminal = 1;
                    MerchantUrl = LocalizedUrl(id, LangDTO.Esp(), "tpv/feed");
                    UrlOk = LocalizedUrl(id, lang, "tpv/Ok");
                    UrlKo = LocalizedUrl(id, lang, "tpv/Ko");
                    PrivateKeyDevelopment = "sq7HjrUOBfKmC576ILgskD5srU870gJ7";
                    PrivateKeyProduction = "tkfxAgYmmhjrccMj4uRHtbKEOxDYGj9+";
                    Banc = BancModel.Wellknown(BancModel.Wellknowns.CaixaBank);
                    break;
            }
        }

        private string LocalizedUrl(Ids id, LangDTO lang, string path)
        {
            var baseEs = id == Ids.Shop4moms ? "www.4moms.es" : "www.matiasmasso.es";
            var basePt = id == Ids.Shop4moms ? "www.4moms.pt" : "www.matiasmasso.pt";
            var retval = baseEs;

            if (lang.IsCat() || lang.IsEng())
                retval = string.Format("https://{0}/{1}/{2}", baseEs, lang.Culture2Digits(), path);
            else if (lang.IsPor())
                retval = string.Format("https://{0}/{1}", basePt, path);
            else
                retval = string.Format("https://{0}/{1}", baseEs, path);

            return retval;
        }

        public static Tpv? Wellknown(Tpv.Ids id)
        {
            Tpv? retval = null;
            switch (id)
            {
                case Ids.MatiasMasso:
                    break;
                case Ids.Shop4moms:
                    retval = new Tpv(new Guid("3b313508-83f3-4186-8578-4753261bbf40"));
                    break;
            }
            return retval;
        }


        public string PropertyPageUrl() => Globals.PageUrl("tpv/config", Guid.ToString());
        public string Gateway() => Environment == DTO.Integracions.Redsys.Common.Environments.Development ? GatewayUrlDevelopment : GatewayUrlProduction;
        public string PrivateKey()
        {
            string retval = string.Empty;
            if (Environment == DTO.Integracions.Redsys.Common.Environments.Development)
                retval = PrivateKeyDevelopment!;
            else
                retval = PrivateKeyProduction!;
            return retval;
        }

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
            var tpvOrderNum = response.TpvOrderNum();
            var privateKey = PrivateKey();
            var computedHash = CreateMerchantSignature(response.Ds_MerchantParameters, tpvOrderNum, privateKey);
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

        public DTO.Integracions.Redsys.TpvLog BookRequest(UserModel user, string concept, decimal eur)
        {
            return new DTO.Integracions.Redsys.TpvLog()
            {
                User = user,
                Mode = DTO.Integracions.Redsys.Common.Modes.Free,
                Ds_Amount = (eur * 100).ToString("F0"),
                Ds_MerchantCode = MerchantCode!,
                Ds_Terminal = MerchantTerminal,
                Ds_ProductDescription = concept,
                Ds_ConsumerLanguage = DTO.Integracions.Redsys.Common.LangCode(Lang.Tag()),
                Titular = user.DefaultContact ?? DTO.CustomerModel.Wellknown(CustomerModel.Wellknowns.consumidor)!.Guid,
                SrcGuid = Guid.NewGuid()
            };
        }

        public static string CreateMerchantSignature(string? ds_MerchantParameters, string? tpvOrderNum, string privateKey)
        {
            var retval = string.Empty;
            if (ds_MerchantParameters != null)
            {
                //var orderNum = paramsDictionary?["Ds_Order"];
                if (tpvOrderNum != null)
                {
                    // Decode key to byte[]
                    byte[] SignatureKeyBytes = Convert.FromBase64String(privateKey);

                    // Calculate derivated key by encrypting with 3DES the "DS_MERCHANT_ORDER" with decoded key 
                    byte[] DerivatedKeyBytes = DTO.Helpers.CryptoHelper.Encrypt3DES(tpvOrderNum, SignatureKeyBytes);

                    // Calculate HMAC SHA256 with Encoded base64 JSON string using derivated key calculated previously
                    byte[] resultBytes = DTO.Helpers.CryptoHelper.GetHMACSHA256(ds_MerchantParameters, DerivatedKeyBytes);

                    retval = Convert.ToBase64String(resultBytes);
                }
            }
            return retval;
        }


    }

}
