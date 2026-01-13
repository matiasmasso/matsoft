using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTORepCertRetencio
    {
        public DTORep Rep { get; set; }
        public DateTime Fch { get; set; }
        public List<DTORepLiq> RepLiqs { get; set; }
        public string Url { get; set; }


        public static int Year(DTORepCertRetencio oCert)
        {
            int retval = oCert.Fch.Year;
            return retval;
        }

        public static int Quarter(DTORepCertRetencio oCert)
        {
            int retval = TimeHelper.Quarter(oCert.Fch);
            return retval;
        }

        public static string Title(DTORepCertRetencio oCert)
        {
            DTOLang oLang = oCert.Rep.Lang;
            string sText = oLang.Tradueix("Certificado Trimestral Decimal Retenciones", "Certificat Trimestral Decimal Retencions", "Quarterly Tax Certificate");
            string retval = string.Format("{0} {1}.T{2}", sText, Year(oCert), Quarter(oCert));
            return retval;
        }

        public static DTOAmt BaseImponible(DTORepCertRetencio oCert)
        {
            decimal DcEur = oCert.RepLiqs.Sum(x => x.BaseImponible.Eur);
            DTOAmt retval = DTOAmt.Factory(DcEur);
            return retval;
        }

        public static DTOAmt IVA(DTORepCertRetencio oCert)
        {
            decimal DcEur = oCert.RepLiqs.Sum(x => DTORepLiq.GetIVAAmt(x).Eur);
            DTOAmt retval = DTOAmt.Factory(DcEur);
            return retval;
        }

        public static DTOAmt IRPF(DTORepCertRetencio oCert)
        {
            decimal DcEur = oCert.RepLiqs.Sum(x => DTORepLiq.GetIRPFAmt(x).Eur);
            DTOAmt retval = DTOAmt.Factory(DcEur);
            return retval;
        }

        public static DTOAmt Liquid(DTORepCertRetencio oCert)
        {
            decimal DcEur = oCert.RepLiqs.Sum(x => DTORepLiq.GetLiquid(x).Eur);
            DTOAmt retval = DTOAmt.Factory(DcEur);
            return retval;
        }

        public string GetUrl()
        {
            return GetUrl(Rep, Fch);
        }

        public static string GetUrl(DTORep oRep, DateTime DtFch)
        {
            MatJSonObject oJson = new MatJSonObject();
            oJson.AddValuePair("Guid", oRep.Guid.ToString());
            oJson.AddValuePair("Year", DtFch.Year.ToString());
            oJson.AddValuePair("Quarter", TimeHelper.Quarter(DtFch).ToString());
            string sBase64 = oJson.ToBase64();

            string retval = DTOWebDomain.Default(true).Url("doc", ((int)DTODocFile.Cods.repcertretencio).ToString(), sBase64);
            return retval;
        }
    }
}
