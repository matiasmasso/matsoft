using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTORepLiq : DTOBaseGuid
    {
        public DTORep Rep { get; set; }
        public int Id { get; set; }
        public DateTime Fch { get; set; }

        public DTOAmt BaseFras { get; set; }
        public DTOAmt BaseImponible { get; set; }
        public decimal IvaPct { get; set; }
        public DTOAmt IvaAmt { get; set; }
        public decimal IrpfPct { get; set; }
        public DTOAmt IrpfAmt { get; set; }
        public DTOAmt Total { get; set; }

        public DTODocFile DocFile { get; set; }

        public DTOCca Cca { get; set; }

        public List<DTORepComLiquidable> Items { get; set; }

        public DTORepLiq() : base()
        {
        }

        public DTORepLiq(Guid oGuid) : base(oGuid)
        {
        }

        public static DTORepLiq Factory(DTORep oRep, DateTime DtFch)
        {
            DTORepLiq retval = new DTORepLiq();
            {
                var withBlock = retval;
                withBlock.Rep = oRep;
                withBlock.Fch = DtFch;
                withBlock.IrpfPct = DTORep.Irpf(withBlock.Rep, DtFch);
                withBlock.IvaPct = DTORep.IVAtipus(withBlock.Rep, DtFch);
                withBlock.Items = new List<DTORepComLiquidable>();
            }
            return retval;
        }

        public static DTOAmt GetBaseFacturas(DTORepLiq value)
        {
            DTOAmt retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (value != null)
            {
                if (value.Items.Count == 0)
                    retval = DTOAmt.Empty();
                else
                {
                    decimal DcEur = value.Items.Sum(x => x.BaseFras.Eur);
                    retval = DTOAmt.Factory(DcEur);
                }
            }
            return retval;
        }

        public static DTOAmt GetTotalComisions(DTORepLiq value)
        {
            DTOAmt retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (value != null)
            {
                if (value.Items.Count == 0)
                    retval = DTOAmt.Empty();
                else
                {
                    decimal DcEur = value.Items.Sum(x => x.Comisio.Eur);
                    retval = DTOAmt.Factory(DcEur);
                }
            }
            return retval;
        }

        public static DTOAmt GetLiquid(DTORepLiq oRepLiq)
        {
            DTOAmt retval = oRepLiq.BaseImponible.Clone();

            if (oRepLiq.IvaPct != 0 | oRepLiq.IrpfPct != 0)
            {
                if (oRepLiq.IvaPct != 0)
                    retval.Add(GetIVAAmt(oRepLiq));
                if (oRepLiq.IrpfPct != 0)
                    retval.Substract(GetIRPFAmt(oRepLiq));
            }
            return retval;
        }

        public static DTOAmt GetIVAAmt(DTORepLiq value)
        {
            DTOAmt retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (value != null)
            {
                if (value.IvaPct != 0)
                {
                    DTOAmt oBaseImponible = value.BaseImponible;
                    retval = oBaseImponible.Percent(value.IvaPct);
                }
            }
            return retval;
        }

        public static DTOAmt GetIRPFAmt(DTORepLiq value)
        {
            DTOAmt retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (value != null)
            {
                if (value.IrpfPct != 0)
                {
                    DTOAmt oBaseImponible = value.BaseImponible;
                    retval = oBaseImponible.Percent(value.IrpfPct);
                }
            }
            return retval;
        }


        public static string caption(DTORepLiq value, DTOLang oLang)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(oLang.Tradueix("Liquidación de Comisiones", "Liquidació de Comisions", "Commission Statement"));
            sb.Append(" " + value.Id);
            sb.Append(oLang.Tradueix(" del ", " del ", " from "));
            sb.Append(value.Fch.ToShortDateString());

            string retval = sb.ToString();
            return retval;
        }

        public string formattedId()
        {
            string retval = VbUtilities.Format(Fch.Year, "0000") + VbUtilities.Format(Id, "000");
            return retval;
        }

        public string urlSegment()
        {
            return base.urlSegment("representante/liquidacion");
        }
    }
}
