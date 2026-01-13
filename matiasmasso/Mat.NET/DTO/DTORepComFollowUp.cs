using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTORepComFollowUp : DTOBaseGuid
    {
        public object Source { get; set; }

        public DTORepComFollowUp Parent { get; set; }
        public Levels Level { get; set; }
        public string Period { get; set; }
        public decimal Ordered { get; set; }
        public decimal Delivered { get; set; }
        public decimal Invoiced { get; set; }
        public decimal Liquid { get; set; }

        public enum Levels
        {
            Month,
            Day,
            Order
        }

        public DTORepComFollowUp() : base()
        {
        }

        public bool Equals(DTORepComFollowUp oCandidate)
        {
            bool retval = false;
            if (oCandidate != null)
                retval = base.Guid.Equals(oCandidate.Guid);
            return retval;
        }

        public static List<DTORepComFollowUp> Months(List<DTOPurchaseOrder> oOrders, DTOLang oLang)
        {
            List<DTORepComFollowUp> retval = oOrders.GroupBy(g => new { g.Fch.Year, g.Fch.Month }).Select(group => new DTORepComFollowUp()
            {
                Source = new DTOYearMonth(group.Key.Year, (DTOYearMonth.Months)group.Key.Month),
                Level = DTORepComFollowUp.Levels.Month,
                Period = string.Format("{0:0000} {1}", group.Key.Year, oLang.MesAbr(group.Key.Month)),
                Ordered = group.Sum(o => o.Items.Sum(item => item.Amount().Eur)),
                Delivered = (decimal)group.Sum(o => o.Items.Sum(item => item.Deliveries?.Sum(d => d.Import().Eur))),
                Invoiced = (decimal)group.Sum(o => o.Items.Sum(item => item.Deliveries?.Where(arc => arc.Delivery.Invoice != null).Sum(d => d.Import().Eur))),
                Liquid = (decimal)group.Sum(o => o.Items.Sum(item => item.Deliveries?.Where(arc => arc.RepLiq != null).Sum(d => d.Import().Eur)))
            }).ToList();

            return retval;
        }

        public static List<DTORepComFollowUp> Days(List<DTOPurchaseOrder> oOrders, DTORepComFollowUp oParent, DTOLang oLang)
        {
            DTOYearMonth oYearMonth = (DTOYearMonth)oParent.Source;
            List<DTORepComFollowUp> retval = oOrders.Where(o => o.Fch.Year == oYearMonth.Year & (int)o.Fch.Month == (int)oYearMonth.Month).GroupBy(g => new { g.Fch }).Select(group => new DTORepComFollowUp()
            {
                Source = group.Key.Fch,
                Parent = oParent,
                Level = DTORepComFollowUp.Levels.Day,
                Period = string.Format("    {0:00} {1}", group.Key.Fch.Day, oLang.WeekDay(group.Key.Fch)),
                Ordered = group.Sum(o => o.Items.Sum(item => item.Amount().Eur)),
                Delivered = (decimal)group.Sum(o => o.Items.Sum(item => item.Deliveries?.Sum(d => d.Import().Eur))),
                Invoiced = (decimal)group.Sum(o => o.Items.Sum(item => item.Deliveries?.Where(arc => arc.Delivery.Invoice != null).Sum(d => d.Import().Eur))),
                Liquid = (decimal)group.Sum(o => o.Items.Sum(item => item.Deliveries?.Where(arc => arc.RepLiq != null).Sum(d => d.Import().Eur)))
            }).ToList();

            return retval;
        }

        public static List<DTORepComFollowUp> Orders(List<DTOPurchaseOrder> oOrders, DTORepComFollowUp oParent)
        {
            DateTime DtFch = (DateTime)oParent.Source;
            List<DTORepComFollowUp> retval = oOrders.Where(o => o.Fch == DtFch).Select(x => new DTORepComFollowUp()
            {
                Source = x,
                Parent = oParent,
                Level = DTORepComFollowUp.Levels.Order,
                Period = string.Format("        {0}", x.Contact().FullNom),
                Ordered = x.Items.Sum(item => item.Amount().Eur),
                Delivered = (decimal)x.Items.Sum(item => item.Deliveries?.Sum(d => d.Import().Eur)),
                Invoiced = (decimal)x.Items.Sum(item => item.Deliveries?.Where(d => d.Delivery.Invoice != null).Sum(inv => inv.Import().Eur)),
                Liquid = (decimal)x.Items.Sum(item => item.Deliveries?.Where(d => d.RepLiq != null).Sum(liq => liq.Import().Eur))
            }).ToList();
            return retval;
        }
    }
}
