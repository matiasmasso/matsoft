using System;

namespace DTO
{
    public class DTOPaymentGateway : DTOBaseGuid
    {
        public string Nom { get; set; }
        public string MerchantCode { get; set; }
        public string SignatureKey { get; set; }
        public string SermepaUrl { get; set; }
        public string MerchantURL { get; set; }
        public string UrlOK { get; set; }
        public string UrlKO { get; set; }
        public string UserAdmin { get; set; }
        public string Pwd { get; set; }
        public DateTime FchFrom { get; set; }
        public DateTime FchTo { get; set; }

        public enum Environments
        {
            NotSet,
            Production,
            Testing
        }
        public DTOPaymentGateway() : base()
        {
        }

        public DTOPaymentGateway(Guid oGuid) : base(oGuid)
        {
        }
    }
}
