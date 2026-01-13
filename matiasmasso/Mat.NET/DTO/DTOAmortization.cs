using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOAmortization : DTOBaseGuid
    {
        public DTOEmp Emp { get; set; }
        public DTOPgcCta Cta { get; set; }
        public DateTime Fch { get; set; }
        public DTOAmt Amt { get; set; }
        public decimal Tipus { get; set; }
        public string Dsc { get; set; }
        public DTOCca Alta { get; set; }
        public DTOCca Baixa { get; set; }
        public List<DTOAmortizationItem> Items { get; set; }


        public DTOAmortization() : base()
        {
        }

        public DTOAmortization(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOAmortization Factory(DTOCca oAltaCca)
        {
            DTOAmortization retval = null;
            var oActivableCcbs = oAltaCca.Items.Where(x => DTOPgcCta.IsActivable(x.Cta)).ToList();
            foreach (var oCcb in oActivableCcbs)
            {
                retval = new DTOAmortization();
                {
                    var withBlock = retval;
                    withBlock.Cta = oCcb.Cta;
                    withBlock.Alta = oAltaCca;
                    withBlock.Fch = oAltaCca.Fch;
                    withBlock.Dsc = oAltaCca.Concept;
                    withBlock.Amt = oCcb.Amt;
                    withBlock.Items = new List<DTOAmortizationItem>();
                }
                {
                    var withBlock = oAltaCca;
                    withBlock.Ccd = DTOCca.CcdEnum.InmovilitzatAlta;
                }
                break;
            }
            return retval;
        }

        public static DTOAmt Saldo(DTOAmortization value)
        {
            DTOAmt retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (value.Baixa == null)
            {
                decimal DcValorAdquisicio = value.Amt.Eur;
                decimal DcAmortitzat = value.Items.Sum(x => x.Amt.Eur);
                retval = DTOAmt.Factory(DcValorAdquisicio - DcAmortitzat);
            }
            else
                retval = DTOAmt.Empty();

            return retval;
        }

        public static DTOAmt Amortitzat(DTOAmortization value)
        {
            decimal DcAmortitzat = value.Items.Sum(x => x.Amt.Eur);
            DTOAmt retval = DTOAmt.Factory(DcAmortitzat);
            return retval;
        }


        public static DTOAmortizationItem BaixaItem(DTOAmortization oAmortization)
        {
            DTOAmortizationItem retval = new DTOAmortizationItem();
            {
                var withBlock = retval;
                withBlock.Parent = oAmortization;
                withBlock.Fch = DTO.GlobalVariables.Today();
                withBlock.Cod = DTOAmortizationItem.Cods.Baixa;
                withBlock.Amt = DTOAmortization.Saldo(oAmortization);
                withBlock.Tipus = withBlock.Amt.Eur / oAmortization.Amt.Eur;
            }
            return retval;
        }


        public static DTOPgcPlan.Ctas CtaCodAmrtAcumulada(DTOPgcPlan.Ctas oCodImmobilitzat)
        {
            DTOPgcPlan.Ctas retval = DTOPgcPlan.Ctas.AmrtAcumAltres;
            switch (oCodImmobilitzat)
            {
                case DTOPgcPlan.Ctas.InmobilitzatIntangible:
                    {
                        retval = DTOPgcPlan.Ctas.AmrtAcumIntangible;
                        break;
                    }

                case DTOPgcPlan.Ctas.InmobilitzatSoftware:
                    {
                        retval = DTOPgcPlan.Ctas.AmrtAcumSoftware;
                        break;
                    }

                case DTOPgcPlan.Ctas.InmobilitzatTerrenys:
                    {
                        retval = DTOPgcPlan.Ctas.NotSet;
                        break;
                    }

                case DTOPgcPlan.Ctas.InmobilitzatConstruccions:
                    {
                        retval = DTOPgcPlan.Ctas.AmrtAcumConstruccions;
                        break;
                    }

                case DTOPgcPlan.Ctas.InmobilitzatInstalacionsAltres:
                    {
                        retval = DTOPgcPlan.Ctas.AmrtAcumInstalacionsAltres;
                        break;
                    }

                case DTOPgcPlan.Ctas.InmobilitzatMobles:
                    {
                        retval = DTOPgcPlan.Ctas.AmrtAcumMobles;
                        break;
                    }

                case DTOPgcPlan.Ctas.InmobilitzatVehicles:
                    {
                        retval = DTOPgcPlan.Ctas.AmrtAcumVehicles;
                        break;
                    }

                case DTOPgcPlan.Ctas.InmobilitzatHardware:
                    {
                        retval = DTOPgcPlan.Ctas.AmrtAcumHardware;
                        break;
                    }

                case DTOPgcPlan.Ctas.InmobilitzatAltres:
                    {
                        retval = DTOPgcPlan.Ctas.AmrtAcumAltres;
                        break;
                    }
            }
            return retval;
        }

        public static DTOPgcPlan.Ctas CtaCodDotacio(DTOPgcPlan.Ctas oCodImmobilitzat)
        {
            DTOPgcPlan.Ctas retval = DTOPgcPlan.Ctas.DotacioAmortitzacioMaterial;
            switch (oCodImmobilitzat)
            {
                case DTOPgcPlan.Ctas.InmobilitzatSoftware:
                    {
                        retval = DTOPgcPlan.Ctas.DotacioAmortitzacioIntangible;
                        break;
                    }

                case DTOPgcPlan.Ctas.InmobilitzatConstruccions:
                    {
                        retval = DTOPgcPlan.Ctas.DotacioAmortitzacioMaterial;
                        break;
                    }

                case DTOPgcPlan.Ctas.InmobilitzatInstalacionsAltres:
                    {
                        retval = DTOPgcPlan.Ctas.DotacioAmortitzacioMaterial;
                        break;
                    }

                case DTOPgcPlan.Ctas.InmobilitzatMobles:
                    {
                        retval = DTOPgcPlan.Ctas.DotacioAmortitzacioMaterial;
                        break;
                    }

                case DTOPgcPlan.Ctas.InmobilitzatHardware:
                    {
                        retval = DTOPgcPlan.Ctas.DotacioAmortitzacioMaterial;
                        break;
                    }

                case DTOPgcPlan.Ctas.InmobilitzatVehicles:
                    {
                        retval = DTOPgcPlan.Ctas.DotacioAmortitzacioMaterial;
                        break;
                    }

                case DTOPgcPlan.Ctas.InmobilitzatAltres:
                    {
                        retval = DTOPgcPlan.Ctas.DotacioAmortitzacioMaterial;
                        break;
                    }
            }
            return retval;
        }
    }

    public class DTOAmortizationItem : DTOBaseGuid
    {
        public DTOAmortization Parent { get; set; }
        public DateTime Fch { get; set; }
        public decimal Tipus { get; set; }
        public DTOAmt Amt { get; set; }
        public DTOCca Cca { get; set; }
        public DTOAmt Saldo { get; set; }
        public Cods Cod { get; set; }

        public enum Cods
        {
            Amortitzacio,
            Baixa
        }

        public DTOAmortizationItem() : base()
        {
        }

        public DTOAmortizationItem(Guid oGuid) : base(oGuid)
        {
        }

        public static string ConcepteAmortitzacio(DTOAmortizationItem oItem)
        {
            DTOAmortization oAmortization = oItem.Parent;
            DTOPgcCta oCtaImmobilitzat = oAmortization.Cta;
            string retval = "Cta." + oCtaImmobilitzat.Id + " " + oItem.Tipus + "% s/" + DTOAmt.CurFormatted(oAmortization.Amt) + "-" + oAmortization.Dsc;
            retval = TextHelper.VbLeft(retval, 60);
            return retval;
        }
    }

    public class DTOAmortizationTipus
    {
        public DTOPgcCta Cta { get; set; }
        public decimal Tipus { get; set; }

        public static decimal ForCta(List<DTOAmortizationTipus> oDefaultTipus, DTOPgcCta oCta)
        {
            decimal retval = 0;
            var item = oDefaultTipus.FirstOrDefault(x => x.Cta.Equals(oCta));
            if (item != null)
                retval = item.Tipus;
            return retval;
        }
    }
}
