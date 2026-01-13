using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTORecall : DTOBaseGuid
    {
        public DateTime Fch { get; set; }
        public string Nom { get; set; }
        public List<DTORecallCli> Clis { get; set; }

        public enum Wellknowns
        {
            Notset,
            DualfixR44
        }

        public DTORecall() : base()
        {
        }

        public DTORecall(Guid oGuid) : base(oGuid)
        {
        }

        public static DTORecall Wellknown(DTORecall.Wellknowns id)
        {
            DTORecall retval = null;
            switch (id)
            {
                case DTORecall.Wellknowns.DualfixR44:
                    {
                        retval = new DTORecall(new Guid("9C76E9AF-E0BF-41BD-9601-3C047A519770"));
                        break;
                    }
            }
            return retval;
        }

        public static MatHelper.Excel.Sheet Excel(DTORecall oRecall)
        {
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet("Recall", "Recall");
            {
                var withBlock = retval;
                withBlock.AddColumn("Customer", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("Sku code", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("Product", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("Serial number", MatHelper.Excel.Cell.NumberFormats.W50);
            }

            foreach (var cli in oRecall.Clis)
            {
                foreach (var product in cli.Products)
                {
                    MatHelper.Excel.Row oRow = retval.AddRow();
                    oRow.AddCell(cli.Customer.FullNom);
                    if (product.Sku == null)
                    {
                        oRow.AddCell();
                        oRow.AddCell();
                    }
                    else
                    {
                        oRow.AddCell(product.Sku.RefProveidor);
                        oRow.AddCell(product.Sku.NomProveidor);
                    }
                    oRow.AddCell(product.SerialNumber);
                }
            }
            return retval;
        }
    }

    public class DTORecallCli : DTOBaseGuid
    {
        public DTORecall Recall { get; set; }
        public string ContactNom { get; set; }
        public string ContactTel { get; set; }
        public string ContactEmail { get; set; }
        public DTOCustomer Customer { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string Location { get; set; }
        public DTOCountry Country { get; set; }
        public DateTime FchVivace { get; set; }

        public string RegMuelle { get; set; }

        public DTOPurchaseOrder PurchaseOrder { get; set; }
        public DTODelivery Delivery { get; set; }

        public List<DTORecallProduct> Products { get; set; }

        public DTOUsrLog UsrLog { get; set; }

        public DTORecallCli() : base()
        {
            UsrLog = new DTOUsrLog();
            Products = new List<DTORecallProduct>();
        }

        public DTORecallCli(Guid oGuid) : base(oGuid)
        {
            UsrLog = new DTOUsrLog();
            Products = new List<DTORecallProduct>();
        }

        public int Bultos()
        {
            return Products.Count;
        }

        public static string RemiteLocation(DTORecallCli value)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(value.Zip))
                sb.AppendFormat("{0} ", value.Zip);
            if (!string.IsNullOrEmpty(value.Location))
                sb.AppendFormat("{0} ", value.Location);
            if (value.Country != null)
            {
                if (value.Country.UnEquals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Spain)))
                    sb.AppendFormat("({0})", value.Country.LangNom.Esp);
            }

            string retval = sb.ToString();
            return retval;
        }
    }

    public class DTORecallProduct
    {
        public DTOProductSku Sku { get; set; }
        public string SerialNumber { get; set; }
    }
}
