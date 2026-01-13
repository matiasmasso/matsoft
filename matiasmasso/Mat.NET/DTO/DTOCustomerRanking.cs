using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOCustomerRanking
    {
        public DTOUser User { get; set; }
        public DTOProduct Product { get; set; }
        public DTOArea Area { get; set; }
        public DateTime FchFrom { get; set; }
        public DateTime FchTo { get; set; }

        public List<DTOCustomerRankingItem> items { get; set; }
        public decimal Sum { get; set; }

        public DTOCustomerRanking() : base()
        {
            items = new List<DTOCustomerRankingItem>();
        }

        public static DTOCustomerRanking Factory(DTOUser oUser, DTOProduct oProduct = null/* TODO Change to default(_) if this is not a reference type */, DTOArea oArea = null/* TODO Change to default(_) if this is not a reference type */, DateTime DtFchFrom = default(DateTime), DateTime DtFchTo = default(DateTime))
        {
            DTOCustomerRanking retval = new DTOCustomerRanking();
            {
                var withBlock = retval;
                withBlock.User = oUser;
                withBlock.Area = oArea;
                withBlock.Product = oProduct;
                withBlock.FchFrom = DtFchFrom == default(DateTime) ? DTO.GlobalVariables.Today().AddYears(-1) : DtFchFrom;
                withBlock.FchTo = DtFchTo == default(DateTime) ? DTO.GlobalVariables.Today() : DtFchTo;
            }
            return retval;
        }

        public void AddItem(DTOCustomer oCustomer, decimal DcEur)
        {
            DTOCustomerRankingItem item = new DTOCustomerRankingItem(oCustomer, DcEur);
            items.Add(item);
            Sum = items.Sum(x => x.Eur);
        }

        public decimal Quota(decimal DcEur)
        {
            decimal retval = 0;
            if (Sum != 0)
                retval = DcEur / Sum;
            return retval;
        }

        public decimal Accumulated(ref decimal Arrastre, decimal DcEur)
        {
            Arrastre = Arrastre + DcEur / Sum;
            decimal retval = Arrastre;
            return retval;
        }

        public string Rank(DTOCustomerRankingItem item)
        {
            int i = items.IndexOf(item) + 1;
            string retval = TextHelper.VbFormat(i, "0000");
            return retval;
        }

        public DTOCsv Csv()
        {
            string sFilename = string.Format("M+O Customer ranking {0:yyyy.MM.dd}-{1:yyyy.MM.dd}.csv", FchFrom, FchTo);
            DTOCsv retval = new DTOCsv(sFilename);
            foreach (DTOCustomerRankingItem item in items)
            {
                var oRow = retval.AddRow();
                oRow.AddCell(item.Customer.FullNom);
                oRow.AddCell(item.Eur.ToString());
            }
            return retval;
        }
    }

    public class DTOCustomerRankingItem
    {
        public DTOCustomer Customer { get; set; }
        public decimal Eur { get; set; }

        public DTOCustomerRankingItem(DTOCustomer oCustomer, decimal DcEur) : base()
        {
            Customer = oCustomer;
            Eur = DcEur;
        }
    }
}
