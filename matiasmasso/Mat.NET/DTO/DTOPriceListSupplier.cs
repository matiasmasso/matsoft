using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOPriceListSupplier : DTOBaseGuid
    {
        public DTOProveidor Proveidor { get; set; }
        public DateTime Fch { get; set; }
        public string Concepte { get; set; }
        public DTOCur Cur { get; set; }
        public decimal Discount_OnInvoice { get; set; }
        public decimal Discount_OffInvoice { get; set; }
        public DTODocFile DocFile { get; set; }
        public List<DTOPriceListItem_Supplier> Items { get; set; }

        public DTOPriceListSupplier() : base()
        {
            Items = new List<DTOPriceListItem_Supplier>();
        }

        public DTOPriceListSupplier(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOPriceListSupplier Factory(DTOProveidor oProveidor)
        {
            DTOPriceListSupplier retval = new DTOPriceListSupplier();
            {
                var withBlock = retval;
                withBlock.Proveidor = oProveidor;
                withBlock.Fch = DTO.GlobalVariables.Today();
                withBlock.Cur = withBlock.Proveidor.Cur;
            }
            return retval;
        }

        public static DTOPricelistCustomer GenerateCustomerPriceList(DTOPriceListSupplier oPriceList)
        {
            DTOCommercialMargin oCommercialMargin = DTOProveidor.GetCommercialMargin(oPriceList.Proveidor);

            // Load(oPriceList)
            DTOPricelistCustomer retval = new DTOPricelistCustomer();
            {
                var withBlock = retval;
                withBlock.Fch = DTO.GlobalVariables.Today().AddDays(1);
                withBlock.Concepte = "(nova tarifa de " + oPriceList.Proveidor.FullNom + ")";
                withBlock.Cur = DTOCur.Eur();
                foreach (DTOPriceListItem_Supplier oSupplierItem in oPriceList.Items)
                {
                    DTOPricelistItemCustomer oCustomerItem = DTOPriceListSupplier.GetSalePrice(oSupplierItem, retval, oCommercialMargin);
                    if (oCustomerItem != null)
                    {
                        if (withBlock.Items.Find(x => x.Sku.RefProveidor == oSupplierItem.Sku.RefProveidor) == null)
                            withBlock.Items.Add(oCustomerItem);
                    }
                }
            }

            return retval;
        }

        public static DTOPricelistItemCustomer GetSalePrice(DTOPriceListItem_Supplier oItem, DTOPricelistCustomer oParent, DTOCommercialMargin oCommercialMargin, decimal DcFixTarifaAFromSupplier = 0)
        {
            DTOPricelistItemCustomer retval = null/* TODO Change to default(_) if this is not a reference type */;
            oCommercialMargin.CostNet = oItem.CostNet();
            DTOProductSku oSku = oItem.Sku; // Art.FromProveidor(New Proveidor(oItem.Parent.Proveidor.Guid), oItem.Ref)
            if (oSku != null)
            {
                retval = new DTOPricelistItemCustomer(oParent);
                {
                    var withBlock = retval;
                    withBlock.Sku = oSku;
                    // .Nom = (New Product(oArt)).Nom(DTOLang.Factory("ESP"))


                    if (oItem.Retail == 0)
                        withBlock.Retail = DTOAmt.Factory(oCommercialMargin.GetRetail(DcFixTarifaAFromSupplier));
                    else
                        withBlock.Retail = DTOAmt.Factory(oItem.Retail);
                }
            }
            return retval;
        }

        public static MatHelper.Excel.Sheet Excel(DTOPriceListSupplier oPriceList)
        {
            string sCaption = string.Format("tarifa {0} {1:yyyy.MM.dd}", oPriceList.Proveidor.NomComercialOrDefault(), oPriceList.Fch);
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet(sCaption, sCaption);
            {
                var withBlock = retval;
                withBlock.AddColumn("ref.M+O", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("ref.proveidor", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("codi EAN", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("descripció", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("cost brut", MatHelper.Excel.Cell.NumberFormats.Euro);
                withBlock.AddColumn("cost net", MatHelper.Excel.Cell.NumberFormats.Euro);
                withBlock.AddColumn("RRPP", MatHelper.Excel.Cell.NumberFormats.Euro);
            }

            foreach (DTOPriceListItem_Supplier Itm in oPriceList.Items)
            {
                MatHelper.Excel.Row oRow = retval.AddRow();
                if (Itm.Sku == null)
                    oRow.AddCell();
                else
                    oRow.AddCell(Itm.Sku.Id);
                oRow.AddCell(Itm.Ref);
                oRow.AddCell(Itm.EAN);
                oRow.AddCell(Itm.Description);
                oRow.AddCell(Itm.Price);
                oRow.AddCell(Itm.CostNet());
                oRow.AddCell(Itm.Retail);
            }
            return retval;
        }

        public static MatHelper.Excel.Sheet ExcelTarifaVigent(List<DTOPriceListItem_Supplier> items)
        {
            string sCaption = string.Format("tarifa vigent de cost {0:yyyy.MM.dd}", DTO.GlobalVariables.Now());
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet(sCaption, sCaption);
            {
                var withBlock = retval;
                withBlock.AddColumn("ref.M+O", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("ref.proveidor", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("codi EAN", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("descripció", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("cost brut", MatHelper.Excel.Cell.NumberFormats.Euro);
                withBlock.AddColumn("cost net", MatHelper.Excel.Cell.NumberFormats.Euro);
                withBlock.AddColumn("RRPP", MatHelper.Excel.Cell.NumberFormats.Euro);
                withBlock.AddColumn("tarifa", MatHelper.Excel.Cell.NumberFormats.DDMMYY);
            }

            foreach (DTOPriceListItem_Supplier Itm in items)
            {
                MatHelper.Excel.Row oRow = retval.AddRow();
                if (Itm.Sku == null)
                    oRow.AddCell();
                else
                    oRow.AddCell(Itm.Sku.Id);
                oRow.AddCell(Itm.Ref);
                oRow.AddCell(Itm.EAN);
                oRow.AddCell(Itm.Description);
                oRow.AddCell(Itm.Price);
                oRow.AddCell(Itm.CostNet());
                oRow.AddCell(Itm.Retail);
                if (Itm.Parent.DocFile == null)
                    oRow.AddCell(Itm.Parent.Fch);
                else
                    oRow.AddCell(Itm.Parent.Fch, Itm.Parent.DocFile.DownloadUrl(true));
            }
            return retval;
        }
    }


    public class DTOPriceListItem_Supplier
    {
        public DTOPriceListSupplier Parent { get; set; }
        public string Ref { get; set; }
        public string EAN { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Retail { get; set; }
        public Guid SkuGuid { get; set; }
        public DTOProductSku Sku { get; set; }
        public int InnerPack { get; set; }
        public int Lin { get; set; }

        public bool IsNew { get; set; }
        public bool IsLoaded { get; set; }

        public static DTOPriceListItem_Supplier Clon(DTOPriceListItem_Supplier oSrcItem)
        {
            DTOPriceListItem_Supplier retval = new DTOPriceListItem_Supplier();
            {
                var withBlock = retval;
                withBlock.Parent = oSrcItem.Parent;
                withBlock.Ref = oSrcItem.Ref;
                withBlock.EAN = oSrcItem.EAN;
                withBlock.Description = oSrcItem.Description;
                withBlock.Price = oSrcItem.Price;
                withBlock.Sku = oSrcItem.Sku;
                withBlock.InnerPack = oSrcItem.InnerPack;
            }
            return retval;
        }

        public decimal CostNet()
        {
            decimal retval;
            decimal DcCostBrut = Price;
            retval = Math.Round(DcCostBrut * (100 - Parent.Discount_OnInvoice - Parent.Discount_OffInvoice) / 100, DTOCur.Eur().Decimals, MidpointRounding.AwayFromZero);
            return retval;
        }

        public static DTOAmt CostNet(DTOPriceListItem_Supplier oPriceItem)
        {
            DTOAmt retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oPriceItem != null)
            {
                retval = DTOAmt.Factory(oPriceItem.Price);
                if (oPriceItem.Parent.Discount_OnInvoice != 0)
                    retval.DeductPercent(oPriceItem.Parent.Discount_OnInvoice);
            }
            return retval;
        }

        public static string Ref_And_Description(DTOPriceListItem_Supplier oItem)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(oItem.Ref))
            {
                sb.Append(oItem.Ref);
                if (!string.IsNullOrEmpty(oItem.Description))
                    sb.Append(" ");
            }
            sb.Append(oItem.Description);
            string retval = sb.ToString();
            return retval;
        }
    }
}
