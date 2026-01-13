using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOPricelistCustomer : DTOBaseGuid
    {
        public DateTime Fch { get; set; }
        public DateTime FchEnd { get; set; }
        public string Concepte { get; set; }
        public DTOCur Cur { get; set; }
        public DTOCustomer Customer { get; set; }
        public List<DTOPricelistItemCustomer> Items { get; set; }

        public DTOPricelistCustomer() : base()
        {
            Fch = DTO.GlobalVariables.Today();
            Concepte = "(nova tarifa de preus de venda)";
            Cur = DTOCur.Eur();
            Items = new List<DTOPricelistItemCustomer>();
        }

        public DTOPricelistCustomer(Guid oGuid) : base(oGuid)
        {
            Items = new List<DTOPricelistItemCustomer>();
        }

        public static DTOPricelistCustomer Factory(DTOCustomer customer)
        {
            DTOPricelistCustomer retval = new DTOPricelistCustomer();
            retval.Customer = customer;
            retval.Fch = DTO.GlobalVariables.Today();
            retval.Cur = DTOCur.Eur();
            return retval;
        }

        public static string FullNom(DTOPricelistCustomer value, DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */)
        {
            if (oLang == null)
                oLang = DTOApp.Current.Lang;
            string retval = string.Format("{0} {1} {2} {3:dd/MM/yy}", oLang.Tradueix("Tarifa", "Tarifa", "Price list"), value.Concepte, oLang.Tradueix("del", "del", "from"), value.Fch);
            return retval;
        }

        public static MatHelper.Excel.Sheet ExcelSheet(DTOPricelistCustomer value)
        {
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet();
            retval.AddRowWithCells("marca", "categoría", "producto", "PVP");
            AddExcelRows(ref retval, value.Items);
            return retval;
        }

        public static MatHelper.Excel.Sheet ExcelSheet(List<DTOPricelistCustomer> values)
        {
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet();
            retval.AddRowWithCells("marca", "categoría", "producto", "tarifa", "tarifa B", "PVP");
            foreach (DTOPricelistCustomer value in values)
                AddExcelRows(ref retval, value.Items);
            return retval;
        }

        public static void AddExcelRows(ref MatHelper.Excel.Sheet oSheet, List<DTOPricelistItemCustomer> values)
        {
            foreach (DTOPricelistItemCustomer Item in values)
            {
                string[] sNoms = DTOProduct.GetNom(Item.Sku).Split('/');
                var oRow = oSheet.AddRow();
                oRow.AddCell(Item.Sku.Guid.ToString());
                oRow.AddCell(sNoms[0]);
                if (sNoms.Count() > 1)
                {
                    oRow.AddCell(sNoms[1]);
                    if (sNoms.Count() > 2)
                        oRow.AddCell(sNoms[2]);
                    else
                        oRow.AddCell();
                }
                else
                {
                    oRow.AddCell();
                    oRow.AddCell();
                }
                oRow.AddCell(Item.Retail.Val);
                oSheet.Rows.Add(oRow);
            }
        }
    }

    public class DTOPricelistItemCustomer
    {
        public DTOPricelistCustomer Parent { get; set; }
        public DTOProductSku Sku { get; set; }
        public DTOAmt Retail { get; set; }

        public string BrandNom { get; set; }
        public string CategoryNom { get; set; }
        public string ProductNom { get; set; }

        public DTOPricelistItemCustomer(DTOPricelistCustomer oPriceList) : base()
        {
            Parent = oPriceList;
        }

        public DTOPricelistItemCustomer Clon(DTOPricelistCustomer oParent)
        {
            DTOPricelistItemCustomer retval = new DTOPricelistItemCustomer(oParent);
            {
                retval.Sku = Sku;
                retval.Retail = Retail;
                retval.BrandNom = BrandNom;
                retval.CategoryNom = CategoryNom;
                retval.ProductNom = ProductNom;
            }
            return retval;
        }
    }
}
