using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOTax : DTOBaseGuid
    {
        public DTOTax.Codis codi { get; set; }
        public DateTime fch { get; set; }
        public decimal tipus { get; set; }

        public enum Codis
        {
            exempt,
            iva_Standard,
            iva_Reduit,
            iva_SuperReduit,
            recarrec_Equivalencia_Standard,
            recarrec_Equivalencia_Reduit,
            recarrec_Equivalencia_SuperReduit,
            irpf_Professionals_Standard,
            irpf_Professionals_Reduit
        }

        public DTOTax() : base()
        {
        }

        public DTOTax(Guid oGuid) : base(oGuid)
        {
        }


        public static List<DTOTax> closest(DateTime DtFch = default(DateTime))
        {
            if (DtFch == default(DateTime))
                DtFch = DTO.GlobalVariables.Today();
            List<DTOTax> pastTaxes = DTOApp.Current.Taxes.Where(x => x.fch <= DtFch).OrderByDescending(y => y.fch).ToList();
            List<DTOTax> retval = pastTaxes.GroupBy(x => x.codi).Select(y => y.First()).ToList();
            return retval;
        }


        public static DTOTax closest(DTOTax.Codis oCodi, DateTime DtFch = default(DateTime))
        {
            if (DtFch == default(DateTime))
                DtFch = DTO.GlobalVariables.Today();
            var oTaxes = closest(DtFch);
            var retval = oTaxes.FirstOrDefault(x => x.codi == oCodi);
            return retval;
        }

        public static decimal closestTipus(DTOTax.Codis oCodi, DateTime DtFch = default(DateTime))
        {
            decimal retval = 0;
            var oTax = DTOTax.closest(oCodi, DtFch);
            if (oTax != null)
                retval = oTax.tipus;
            return retval;
        }

        public static DTOTax.Codis reqForIvaCod(DTOTax.Codis oIvaCod)
        {
            DTOTax.Codis retval = DTOTax.Codis.exempt;
            switch (oIvaCod)
            {
                case DTOTax.Codis.iva_Standard:
                    {
                        retval = DTOTax.Codis.recarrec_Equivalencia_Standard;
                        break;
                    }

                case DTOTax.Codis.recarrec_Equivalencia_Reduit:
                    {
                        retval = DTOTax.Codis.recarrec_Equivalencia_Reduit;
                        break;
                    }

                case DTOTax.Codis.recarrec_Equivalencia_SuperReduit:
                    {
                        retval = DTOTax.Codis.recarrec_Equivalencia_SuperReduit;
                        break;
                    }
            }
            return retval;
        }


        public static string nom(DTOTax.Codis oCodi, DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */)
        {
            if (oLang == null)
                oLang = DTOApp.Current.Lang;
            string sRetVal = "";
            switch (oCodi)
            {
                case DTOTax.Codis.iva_Standard:
                    {
                        sRetVal = oLang.Tradueix("IVA", "IVA", "VAT");
                        break;
                    }

                case DTOTax.Codis.iva_Reduit:
                    {
                        sRetVal = oLang.Tradueix("IVA reducido", "IVA reduit", "short VAT");
                        break;
                    }

                case DTOTax.Codis.iva_SuperReduit:
                    {
                        sRetVal = oLang.Tradueix("IVA super reducido", "IVA super reduit", "super short VAT");
                        break;
                    }

                case DTOTax.Codis.recarrec_Equivalencia_Standard:
                case DTOTax.Codis.recarrec_Equivalencia_Reduit:
                case DTOTax.Codis.recarrec_Equivalencia_SuperReduit:
                    {
                        sRetVal = oLang.Tradueix("recargo de equivalencia", "recarrec d'equivalencia", "additional VAT");
                        break;
                    }

                case DTOTax.Codis.irpf_Professionals_Standard:
                case DTOTax.Codis.irpf_Professionals_Reduit:
                    {
                        sRetVal = oLang.Tradueix("retención IRPF", "retenció IRPF", "IRPF income tax deduction");
                        break;
                    }
            }
            return sRetVal;
        }

        public static DTOPgcPlan.Ctas ctaCod(DTOTax.Codis oTaxCod)
        {
            DTOPgcPlan.Ctas retval = (DTOPgcPlan.Ctas)DTOTax.Codis.exempt;
            switch (oTaxCod)
            {
                case DTOTax.Codis.iva_Standard:
                    {
                        retval = DTOPgcPlan.Ctas.IvaRepercutitNacional;
                        break;
                    }

                case DTOTax.Codis.iva_Reduit:
                    {
                        retval = DTOPgcPlan.Ctas.IvaReduit;
                        break;
                    }

                case DTOTax.Codis.iva_SuperReduit:
                    {
                        retval = DTOPgcPlan.Ctas.IvaSuperReduit;
                        break;
                    }

                case DTOTax.Codis.recarrec_Equivalencia_Standard:
                    {
                        retval = DTOPgcPlan.Ctas.IvaRecarrecEquivalencia;
                        break;
                    }

                case DTOTax.Codis.recarrec_Equivalencia_Reduit:
                    {
                        retval = DTOPgcPlan.Ctas.IvaRecarrecReduit;
                        break;
                    }

                case DTOTax.Codis.recarrec_Equivalencia_SuperReduit:
                    {
                        retval = DTOPgcPlan.Ctas.IvaRecarrecSuperReduit;
                        break;
                    }
            }
            return retval;
        }

        public static DTOAmt quota(DTOAmt oBase, DTOTax oTax)
        {
            DTOAmt retval = oBase.Clone().Percent(oTax.tipus);
            return retval;
        }
    }
}
