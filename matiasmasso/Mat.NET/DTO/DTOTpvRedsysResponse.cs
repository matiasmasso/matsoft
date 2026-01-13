using MatHelperStd;

namespace DTO
{
    public class DTOTpvRedsysResponse : DTOBaseGuid
    {
        public string Ds_Date { get; set; }
        public string Ds_Hour { get; set; }
        public string Ds_Amount { get; set; }
        public string Ds_Currency { get; set; }
        public string Ds_Order { get; set; }
        public string Ds_MerchantCode { get; set; }
        public string Ds_Terminal { get; set; }
        public string Ds_Response { get; set; }
        public string Ds_MerchantData { get; set; }
        public string Ds_SecurePayment { get; set; }
        public string Ds_TransactionType { get; set; }
        public string Ds_Card_Country { get; set; }
        public string Ds_AuthorisationCode { get; set; }
        public string Ds_ConsumerLanguage { get; set; }

        public DTOTpvRequest.Modes Mode { get; set; }
        public DTOBaseGuid Request { get; set; }
        public DTOCca Result { get; set; }
        public DTOUser User { get; set; }
        public bool SignatureValidated { get; set; }
        public bool ProcessedSuccessfully { get; set; }

        public DTOTpvRedsysResponse(string ds_Order) : base()
        {
            Ds_Order = ds_Order;
        }

        public DTOTpvRedsysResponse() : base()
        {
            IsNew = true;
        }

        public bool IsValid()
        {
            bool retval = false;
            if (Ds_Response == "0000")
                retval = true;
            if (!SignatureValidated)
                retval = false;
            if (!TextHelper.VbIsNumeric(Ds_AuthorisationCode))
                retval = false;
            return retval;
        }
    }
}
