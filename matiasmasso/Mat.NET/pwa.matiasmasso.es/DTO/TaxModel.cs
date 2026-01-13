using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TaxModel : BaseGuid
    {
        public TaxModel.Codis Codi { get; set; }
        public DateOnly Fch { get; set; }
        public decimal Tipus { get; set; }

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

        public TaxModel() : base()
        {
        }

        public TaxModel(Guid oGuid) : base(oGuid)
        {
        }


        public static List<TaxModel> Closest(List<TaxModel> allTaxes, DateOnly DtFch = default(DateOnly))
        {
            if (DtFch == default(DateOnly))
                DtFch = DateOnly.FromDateTime(DateTime.Now);
            List<TaxModel> pastTaxes = allTaxes.Where(x => x.Fch <= DtFch).OrderByDescending(y => y.Fch).ToList();
            List<TaxModel> retval = pastTaxes.GroupBy(x => x.Codi).Select(y => y.First()).ToList();
            return retval;
        }


        public static TaxModel? Closest(List<TaxModel> allTaxes, TaxModel.Codis oCodi, DateOnly DtFch = default(DateOnly))
        {
            if (DtFch == default(DateOnly))
                DtFch = DateOnly.FromDateTime(DateTime.Now);
            var oTaxes = Closest(allTaxes, DtFch);
            var retval = oTaxes.FirstOrDefault(x => x.Codi == oCodi);
            return retval;
        }

        public static decimal ClosestTipus(List<TaxModel> allTaxes, TaxModel.Codis oCodi, DateOnly DtFch = default(DateOnly))
        {
            decimal retval = 0;
            var oTax = TaxModel.Closest(allTaxes, oCodi, DtFch);
            if (oTax != null)
                retval = oTax.Tipus;
            return retval;
        }

        public static TaxModel.Codis ReqForIvaCod(TaxModel.Codis oIvaCod)
        {
            TaxModel.Codis retval = TaxModel.Codis.exempt;
            switch (oIvaCod)
            {
                case TaxModel.Codis.iva_Standard:
                    {
                        retval = TaxModel.Codis.recarrec_Equivalencia_Standard;
                        break;
                    }

                case TaxModel.Codis.recarrec_Equivalencia_Reduit:
                    {
                        retval = TaxModel.Codis.recarrec_Equivalencia_Reduit;
                        break;
                    }

                case TaxModel.Codis.recarrec_Equivalencia_SuperReduit:
                    {
                        retval = TaxModel.Codis.recarrec_Equivalencia_SuperReduit;
                        break;
                    }
            }
            return retval;
        }


        public static string Nom(TaxModel.Codis oCodi, LangDTO oLang)
        {
            string sRetVal = "";
            switch (oCodi)
            {
                case TaxModel.Codis.iva_Standard:
                    {
                        sRetVal = oLang.Tradueix("IVA", "IVA", "VAT");
                        break;
                    }

                case TaxModel.Codis.iva_Reduit:
                    {
                        sRetVal = oLang.Tradueix("IVA reducido", "IVA reduit", "short VAT");
                        break;
                    }

                case TaxModel.Codis.iva_SuperReduit:
                    {
                        sRetVal = oLang.Tradueix("IVA super reducido", "IVA super reduit", "super short VAT");
                        break;
                    }

                case TaxModel.Codis.recarrec_Equivalencia_Standard:
                case TaxModel.Codis.recarrec_Equivalencia_Reduit:
                case TaxModel.Codis.recarrec_Equivalencia_SuperReduit:
                    {
                        sRetVal = oLang.Tradueix("recargo de equivalencia", "recarrec d'equivalencia", "additional VAT");
                        break;
                    }

                case TaxModel.Codis.irpf_Professionals_Standard:
                case TaxModel.Codis.irpf_Professionals_Reduit:
                    {
                        sRetVal = oLang.Tradueix("retención IRPF", "retenció IRPF", "IRPF income tax deduction");
                        break;
                    }
            }
            return sRetVal;
        }

        public static PgcPlanModel.Ctas ctaCod(TaxModel.Codis oTaxCod)
        {
            PgcPlanModel.Ctas retval = (PgcPlanModel.Ctas)TaxModel.Codis.exempt;
            switch (oTaxCod)
            {
                case TaxModel.Codis.iva_Standard:
                    {
                        retval = PgcPlanModel.Ctas.IvaRepercutitNacional;
                        break;
                    }

                case TaxModel.Codis.iva_Reduit:
                    {
                        retval = PgcPlanModel.Ctas.IvaReduit;
                        break;
                    }

                case TaxModel.Codis.iva_SuperReduit:
                    {
                        retval = PgcPlanModel.Ctas.IvaSuperReduit;
                        break;
                    }

                case TaxModel.Codis.recarrec_Equivalencia_Standard:
                    {
                        retval = PgcPlanModel.Ctas.IvaRecarrecEquivalencia;
                        break;
                    }

                case TaxModel.Codis.recarrec_Equivalencia_Reduit:
                    {
                        retval = PgcPlanModel.Ctas.IvaRecarrecReduit;
                        break;
                    }

                case TaxModel.Codis.recarrec_Equivalencia_SuperReduit:
                    {
                        retval = PgcPlanModel.Ctas.IvaRecarrecSuperReduit;
                        break;
                    }
            }
            return retval;
        }

        public static Amt Quota(Amt oBase, TaxModel oTax)
        {
            var retval = oBase.Clone().Percent(oTax.Tipus);
            return retval;
        }

        public class TipusBaseQuota
        {
            public TaxModel Tax { get; set; }
            public Amt BaseImponible { get; set; }

            public Amt Quota { get; set; }

            public TipusBaseQuota(TaxModel.Codis oTaxCodi, decimal DcBase, decimal DcTipus, decimal DcQuota = 0) : base()
            {
                Tax = new TaxModel();
                Tax.Codi = oTaxCodi;
                Tax.Tipus = DcTipus;
                BaseImponible = new Amt(DcBase);
                if (DcQuota == 0)
                    Quota = BaseImponible.Percent(Tax.Tipus);
                else
                    Quota = new Amt(DcQuota);
            }

            public TipusBaseQuota(TaxModel oTax, Amt oBaseImponible) : base()
            {
                Tax = oTax;
                BaseImponible = oBaseImponible.Clone();
                Quota = BaseImponible.Percent(oTax.Tipus);
            }

            public TipusBaseQuota() : base()
            {
            }

            public void AddBase(Amt oBase)
            {
                BaseImponible.Add(oBase);
                Quota = BaseImponible.Percent(Tax.Tipus);
            }
        }
    }

}
