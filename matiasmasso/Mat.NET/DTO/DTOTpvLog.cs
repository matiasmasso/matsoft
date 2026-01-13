using MatHelperStd;
using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOTpvLog : DTOBaseGuid
    {
        public string Ds_Date { get; set; }
        public string Ds_Hour { get; set; }
        public string Ds_Amount { get; set; }
        public string Ds_Currency { get; set; }
        public string Ds_Order { get; set; }
        public string Ds_MerchantCode { get; set; }
        public string Ds_Terminal { get; set; }
        public string Ds_Signature { get; set; }
        public string Ds_Response { get; set; }
        public string Ds_MerchantData { get; set; }
        public string Ds_ProductDescription { get; set; }
        public string Ds_SecurePayment { get; set; }
        public string Ds_TransactionType { get; set; }
        public string Ds_Card_Country { get; set; }
        public string Ds_AuthorisationCode { get; set; }
        public string Ds_ConsumerLanguage { get; set; }
        public string Ds_Card_Type { get; set; }

        // response:
        public string Ds_MerchantParameters { get; set; }
        public string Ds_SignatureReceived { get; set; }
        public string ErrDsc { get; set; }


        public DTOTpvRequest.Modes Mode { get; set; }
        public DTOBaseGuid Request { get; set; }
        public DTOCca Result { get; set; }
        public DTOUser User { get; set; }
        public DTOContact Titular { get; set; }
        public DateTime FchCreated { get; set; }
        public bool SignatureValidated { get; set; }
        public bool ProcessedSuccessfully { get; set; }

        public string Exceptions { get; set; }


        public DTOTpvLog() : base()
        {
        }

        public DTOTpvLog(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOLang Lang(DTOTpvLog oTpvLog)
        {
            var retval = DTOLang.Factory(DTOLang.Ids.ESP);
            if (TextHelper.VbIsNumeric(oTpvLog.Ds_ConsumerLanguage))
            {
                switch (System.Convert.ToInt32(oTpvLog.Ds_ConsumerLanguage))
                {
                    case 2:
                        {
                            retval = DTOLang.Factory(DTOLang.Ids.ENG);
                            break;
                        }

                    case 3:
                        {
                            retval = DTOLang.Factory(DTOLang.Ids.CAT);
                            break;
                        }
                }
            }
            return retval;
        }

        public static DTOAmt Amt(DTOTpvLog oTpvLog)
        {
            decimal dcAmt = 0;
            string sAmt = oTpvLog.Ds_Amount;
            if (TextHelper.VbIsNumeric(sAmt))
                dcAmt = System.Convert.ToDecimal(sAmt) / 100;
            DTOAmt retVal = DTOAmt.Factory(dcAmt);
            return retVal;
        }

        public static DTOPgcPlan.Ctas CreditCtaCod(DTOContact oTitular)
        {
            var retval = DTOPgcPlan.Ctas.TransferenciesDesconegudes;
            if (oTitular != null)
                retval = DTOPgcPlan.Ctas.Clients_Anticips;
            return retval;
        }

        public static DTOPgcPlan.Ctas DebitCtaCod()
        {
            return DTOPgcPlan.Ctas.VisasCobradas;
        }

        public static bool isValidResponse(DTOTpvLog oLog, List<Exception> exs)
        {
            bool retval = false;
            if (oLog.Ds_Response == "0000")
                retval = true;
            else
                exs.Add(new Exception(string.Format("Codi de error Redsys {0}: {1}", oLog.Ds_Response, oLog.ErrDsc)));

            if (!oLog.SignatureValidated)
            {
                exs.Add(new Exception("Les signatures no coincideixen"));
                retval = false;
            }

            if (!TextHelper.VbIsNumeric(oLog.Ds_AuthorisationCode))
            {
                exs.Add(new Exception("Operació no autoritzada"));
                retval = false;
            }
            return retval;
        }

        public static DTOMailMessage MailMessage(DTOTpvLog oTpvLog)
        {
            var sRecipient = DTOMailMessage.wellknownAddress(DTOMailMessage.wellknownRecipients.Info);
            var sSubject = string.Format("Avís de Tpv: {0}", oTpvLog.Result.Concept);
            var retval = DTOMailMessage.Factory(sRecipient, sSubject);
            return retval;
        }
    }
}
