using MatHelperStd;
using System;

namespace DTO
{
    public class DTOTpvRequest
    {
        public Modes Mode { get; set; }
        public Guid Ref { get; set; }
        public string Concept { get; set; }
        public decimal Eur { get; set; }
        //public DTOAmt.Compact Amt { get; set; }

        public enum Modes
        {
            NotSet,
            Free,
            Alb,
            Pdc,
            Impagat,
            Consumer
        }

        public DTOTpvRequest() : base()
        {
            //Amt = DTOAmt.Compact.Factory();
        }

        public DTOTpvRequest(DTOTpvRequest.Modes oMode) : base()
        {
            Mode = oMode;
            //Amt = DTOAmt.Compact.Factory();
        }

        public static DTOTpvRequest FromFreeConcept(DTOLang oLang, string sConcept, decimal DcEur, Guid oUserGuid)
        {
            DTOTpvRequest retval = new DTOTpvRequest(DTOTpvRequest.Modes.Free);
            {
                var withBlock = retval;
                // .Lang = oLang
                withBlock.Ref = oUserGuid;
                withBlock.Concept = sConcept;
                withBlock.Eur = DcEur;
            }
            return retval;
        }

        public static DTOTpvRequest FromPdc(DTOPurchaseOrder oPurchaseOrder, DTOLang oLang)
        {
            DTOTpvRequest retval = new DTOTpvRequest(DTOTpvRequest.Modes.Pdc);
            {
                var withBlock = retval;
                // .Lang = oLang
                withBlock.Ref = oPurchaseOrder.Guid;
                withBlock.Concept = oPurchaseOrder.caption(oLang);
                // .OurConcepte = "comanda " & oPurchaseOrder.Num.ToString & " de " & oPurchaseOrder.Contact.FullNom
                withBlock.Eur = DTOPurchaseOrder.totalIvaInclos(oPurchaseOrder).Eur;
            }
            return retval;
        }

        public static DTOTpvRequest FromAlb(DTODelivery oDelivery, DTOLang oLang)
        {
            DTOTpvRequest retval = new DTOTpvRequest(DTOTpvRequest.Modes.Alb);
            {
                var withBlock = retval;
                // .Lang = oLang
                withBlock.Ref = oDelivery.Guid;
                withBlock.Concept = DTODelivery.caption(oDelivery, oLang);
                // .OurConcepte = "alb." & oDelivery.Id.ToString & " de " & oDelivery.Customer.FullNom
                withBlock.Eur = oDelivery.Import.Eur;
            }
            return retval;
        }

        public static DTOTpvRequest FromImpagat(DTOImpagat oImpagat, DTOLang oLang)
        {
            DTOTpvRequest retval = null;
            if (oImpagat != null)
            {
                DTOContact oContact = oImpagat.Csb.Contact;

                retval = new DTOTpvRequest(DTOTpvRequest.Modes.Impagat);
                {
                    var withBlock = retval;
                    // .Lang = oLang
                    withBlock.Ref = oImpagat.Guid;
                    withBlock.Concept = string.Format("{0} {1}", oLang.Tradueix("reposición impagado", "reposició impagat", "unpayment settlement"), oImpagat.Csb.Txt);
                    withBlock.Eur = DTOImpagat.PendentAmbGastos(oImpagat).Eur;
                }
            }

            return retval;
        }

        public static string EncodeMerchantData(DTOUser oUser, DTOTpvRequest.Modes oRequestMode, Guid oGuid)
        {
            Guid oUserGuid = Guid.Empty;
            if (oUser != null)
                oUserGuid = oUser.Guid;

            MatJSonObject oJson = new MatJSonObject();
            oJson.AddValuePair("User", oUserGuid.ToString());
            oJson.AddValuePair("Mode", ((int)oRequestMode).ToString());
            oJson.AddValuePair("Guid", oGuid.ToString());
            string retval = oJson.ToBase64();
            return retval;
        }

        public static string CreateMerchantSignature(DTOPaymentGateway oConfig, string sMerchantParameters, string sMerchantOrder)
        {
            // Decode key to byte[]
            byte[] SignatureKeyBytes = System.Convert.FromBase64String(oConfig.SignatureKey);

            // Calculate derivated key by encrypting with 3DES the "DS_MERCHANT_ORDER" with decoded key 
            byte[] DerivatedKeyBytes = CryptoHelper.Encrypt3DES(sMerchantOrder, SignatureKeyBytes);

            // Calculate HMAC SHA256 with Encoded base64 JSON string using derivated key calculated previously
            byte[] resultBytes = CryptoHelper.GetHMACSHA256(sMerchantParameters, DerivatedKeyBytes);

            string retval = System.Convert.ToBase64String(resultBytes);
            return retval;
        }

        public static string CreateMerchantParameters(DTOPaymentGateway oConfig, DTOTpvLog oTpvLog)
        {
            MatJSonObject oJson = new MatJSonObject();
            {
                var withBlock = oJson;
                withBlock.AddValuePair("Ds_Merchant_Amount", oTpvLog.Ds_Amount);
                withBlock.AddValuePair("Ds_Merchant_Order", oTpvLog.Ds_Order);
                withBlock.AddValuePair("Ds_Merchant_MerchantCode", oTpvLog.Ds_MerchantCode);
                withBlock.AddValuePair("Ds_Merchant_Currency", oTpvLog.Ds_Currency);
                withBlock.AddValuePair("Ds_Merchant_Terminal", oTpvLog.Ds_Terminal);
                withBlock.AddValuePair("Ds_Merchant_TransactionType", oTpvLog.Ds_TransactionType);
                withBlock.AddValuePair("Ds_Merchant_MerchantUrl", oConfig.MerchantURL); // url de notificacio operacions. Hauria de ser https://www.matiasmasso.es/tpv/log
                withBlock.AddValuePair("Ds_Merchant_UrlOK", oConfig.UrlOK);
                withBlock.AddValuePair("Ds_Merchant_UrlKO", oConfig.UrlKO);
                withBlock.AddValuePair("Ds_Merchant_ProductDescription", oTpvLog.Ds_ProductDescription);
                withBlock.AddValuePair("Ds_Merchant_ConsumerLanguage", oTpvLog.Ds_ConsumerLanguage);
                withBlock.AddValuePair("Ds_Merchant_MerchantData", DTOTpvRequest.EncodeMerchantData(oTpvLog.User, oTpvLog.Mode, oTpvLog.Guid)); // Callback a retornar via log
            }

            string sJsonRequest = oJson.ToString();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(sJsonRequest);
            string retval = Convert.ToBase64String(bytes);

            // tmp for testing ----------------------------------------------
            // Dim sCodedMerchantData As String = retval
            // Dim oBytes As Byte() = Convert.FromBase64String(sCodedMerchantData)
            // Dim sDecodedMerchantData As String = System.Text.Encoding.UTF8.GetString(oBytes)

            return retval;
        }
    }
}
