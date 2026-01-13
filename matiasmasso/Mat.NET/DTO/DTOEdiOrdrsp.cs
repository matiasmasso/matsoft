using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DTO
{
    public class DTOEdiOrdrsp
    {
        public DTOEdiOrdrsp() { }

        private DTOPurchaseOrder PurchaseOrder { get; set; }
        private DTOContact Sender { get; set; }

        private DateTime FchCreated { get; set; }
        private DateTime DeliveryDate { get; set; }
        private DateTime DocumentDate { get; set; }

        private List<DTODeliveryItem> deliveredItems { get; set; }

        private Results Result = Results.AcceptedWithNoAmendments;

        private enum Results
        {
            Change = 4,
            NotProcessed = 12,
            NotAccepted = 27,
            AcceptedWithNoAmendments = 29,
            AcceptedWithReserves = 45
        }


        public static DTOEdiOrdrsp Factory(DTOPurchaseOrder oPurchaseOrder, DTOContact oSender, DateTime documentDate, DateTime deliveryDate)
        {
            DTOEdiOrdrsp retval = new DTOEdiOrdrsp();
            retval.PurchaseOrder = oPurchaseOrder;
            retval.Sender = oSender;
            retval.FchCreated = DTO.GlobalVariables.Now();
            retval.DocumentDate = documentDate;
            retval.DeliveryDate = deliveryDate;
            return retval;
        }

        public string DefaultFileName()
        {
            String retval = String.Format("ORDRSP_{0}_{1:yyyyMMddHHmm}.txt", PurchaseOrder.formattedId(), FchCreated);
            return retval;
        }

        public Byte[] ByteArray(out string warnMsg)
        {
            return Encoding.UTF8.GetBytes(Stream(out warnMsg));
        }

        public string Stream(out string warnMsg )
        {
            StringBuilder sb = new StringBuilder();
            string msgId = string.Format("{0:yyMMddHHmmssfff}", DTO.GlobalVariables.Now());
            List<string> segments = new List<string>();
            segments.Add("UNA:+.? '");
            segments.Add(String.Format("UNB+UNOC:3+{0}:14+{1}:14+{2:yyMMdd:HHmm}+{3}'", Sender.GLN.Value, PurchaseOrder.Customer.GLN.Value, FchCreated, msgId));
            segments.Add("UNH+1+ORDRSP:D:96A:UN:EAN005'");
            segments.Add(String.Format("BGM+231+{0}+{1}'", PurchaseOrder.formattedId(), ((int)Result).ToString()));
            segments.Add(String.Format("DTM+137:{0:yyyyMMdd}:102'", DocumentDate)); //137 =doc/mesage datetime,  102 = format CCYYMMDD
            segments.Add(String.Format("DTM+2:{0:yyyyMMdd}:102'", DeliveryDate)); //137 =doc/mesage datetime,  102 = format CCYYMMDD
            segments.Add(String.Format("RFF+ON:{0}'", PurchaseOrder.Concept));
            segments.Add(String.Format("NAD+BY+{0}::9++:++++'", PurchaseOrder.Customer.GLN.Value));
            segments.Add(String.Format("NAD+SU+{0}::9++:++++'", Sender.GLN.Value));
            segments.Add(String.Format("NAD+DP+{0}::9++:++++'", PurchaseOrder.Customer.GLN.Value));
            segments.Add(String.Format("NAD+IV+{0}::9++:++++'", PurchaseOrder.Customer.GLN.Value));
            segments.Add(String.Format("TAX+7+VAT+++:::21+S'", PurchaseOrder.Customer.GLN.Value));
            segments.Add(String.Format("MOA+124:{0}'", DTOPurchaseOrder.taxAmt(PurchaseOrder).Eur.ToString("F2", CultureInfo.InvariantCulture)));
            segments.Add("CUX +2:EUR:9'");

            foreach (DTOPurchaseOrderItem item in PurchaseOrder.Items)
            {
                segments.Add(string.Format("LIN+{0}++{1}:EN'", PurchaseOrder.Items.IndexOf(item) + 1, DTOProductSku.Ean(item.Sku)));
                segments.Add(string.Format("PIA+1+{0}:SA'", DTOProductSku.refPrvOrSkuid(item.Sku)));
                segments.Add(string.Format("IMD+F++:::{0}:B'", item.Sku.NomLlarg.Esp));
                segments.Add(string.Format("QTY+21:{0}:PCE'", item.Qty));

                switch (item.ErrCod)
                {
                    case DTOPurchaseOrderItem.ErrCods.Success:
                        if (item.Qty > item.Pending)
                        {
                            segments.Add(string.Format("QTY+12:{0}:PCE'", item.Qty - item.Pending));
                        }
                        if (item.Pending > 0)
                        {
                            segments.Add(string.Format("QTY+185:{0}:PCE'", item.Pending));
                            sb.AppendLine(string.Format("eliminar {0:N0} unitats no servides de {1} pendents de comanda {2}", item.Pending, item.Sku.RefYNomLlarg().Esp, PurchaseOrder.caption()));
                        }
                        segments.Add(String.Format("DTM+2:{0:yyyyMMdd}:102'", DeliveryDate)); //137 =doc/mesage datetime,  102 = format CCYYMMDD
                        break;
                    case DTOPurchaseOrderItem.ErrCods.MoqNotMet:
                    case DTOPurchaseOrderItem.ErrCods.OutOfStock:
                        segments.Add(string.Format("QTY+185:{0}:PCE'", item.Qty));
                        sb.AppendLine(string.Format("eliminar {0:N0} unitats fora de stock de {1} pendents de comanda {2}", item.Pending, item.Sku.RefYNomLlarg().Esp, PurchaseOrder.caption()));
                        break;
                    default:
                        segments.Add(string.Format("QTY+182:{0}:PCE'", item.Qty));
                        break;
                }

                //segments.Add(string.Format("DTM+2:{0:yyyyMMdd}:102'", PurchaseOrder.FchDeliveryMin));
                segments.Add(string.Format("PRI+AAB:{0}'", item.Price.Eur.ToString("F2", CultureInfo.InvariantCulture)));
                if (item.Dto != 0)
                {
                    segments.Add(string.Format("PRI+AAA:{0}'", item.preuNet().Eur.ToString("F2", CultureInfo.InvariantCulture)));
                    segments.Add("ALC+A++++ZZZ'");
                    segments.Add(string.Format("PCD+3:{0}'", item.Dto.ToString(CultureInfo.InvariantCulture)));

                }
            }

            segments.Add("UNS+S'"); //header detail separator
            segments.Add(string.Format("MOA+79:{0}'", PurchaseOrder.Suma().Eur.ToString("F2", CultureInfo.InvariantCulture))); //monetary amount 79=Total lines amount
            segments.Add(string.Format("UNT+{0}+1'", segments.Count.ToString() + 2));
            segments.Add(string.Format("UNZ+1+{0}'", msgId));

            string retval = TextHelper.StringListToMultiline(segments);
            warnMsg = sb.ToString();
            return retval;
        }



    }
}
