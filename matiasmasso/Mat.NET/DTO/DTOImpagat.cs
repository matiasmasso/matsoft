using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOImpagat : DTOBaseGuid
    {
        public DTOCsb Csb { get; set; }
        public string RefBanc { get; set; }
        public DTOAmt Gastos { get; set; }
        public DTOAmt PagatACompte { get; set; }
        public DateTime FchAFP { get; set; }
        public DateTime FchSdo { get; set; }
        // Property Insolvencia As Insolvencia
        public StatusCodes Status { get; set; }
        public DateTime AsnefAlta { get; set; }
        public DateTime AsnefBaixa { get; set; }

        public DTOCca CcaIncobrable { get; set; }
        public string Obs { get; set; }

        public DateTime LastMemFch { get; set; }

        public enum StatusCodes
        {
            notSet,
            enNegociacio,
            conveni,
            saldat,
            insolvencia
        }

        public enum OrderBy
        {
            vto,
            cliNomVto
        }


        public DTOImpagat() : base()
        {
        }

        public DTOImpagat(Guid oGuid) : base(oGuid)
        {
        }

        public DTOAmt Nominal
        {
            get
            {
                DTOAmt retval = null/* TODO Change to default(_) if this is not a reference type */;
                if (Csb != null)
                    retval = Csb.Amt;
                return retval;
            }
        }

        public static DTOAmt PendentAmbGastos(DTOImpagat oImpagat)
        {
            DTOAmt retval = oImpagat.Nominal.Clone();
            if (oImpagat.PagatACompte != null)
                retval = retval.Substract(oImpagat.PagatACompte);
            if (oImpagat.Gastos != null)
            {
                DTOAmt oGastos = oImpagat.Gastos;
                if (oGastos.Eur < 40)
                    oGastos = DTOAmt.Factory(40); // minim despeses
                retval = retval.Add(oGastos);
            }
            return retval;
        }

        public static DTOAmt Pendent(DTOImpagat oImpagat)
        {
            DTOAmt retval = oImpagat.Nominal;
            if (oImpagat.PagatACompte != null)
                retval = retval.Substract(oImpagat.PagatACompte);
            return retval;
        }

        public static DTOAmt GetGastos(DTOImpagat oImpagat)
        {
            DTOAmt oMinimGastos = DTOAmt.Factory(40);
            DTOAmt retval = oImpagat.Gastos;
            if (retval == null)
                retval = oMinimGastos;
            else if (retval.Eur < oMinimGastos.Eur)
                retval = oMinimGastos;
            return retval;
        }
    }


    public class DTOImpagats
    {
        public List<DTOImpagat> Impagats { get; set; }
        public DTOCca Cca { get; set; }
    }
}
