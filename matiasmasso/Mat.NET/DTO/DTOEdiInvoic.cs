using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    class DTOEdiInvoic
    {
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceFch { get; set; }
        public Interlocutor NadBy { get; set; }
        public Interlocutor NadIv { get; set; }
        public Interlocutor NadBco { get; set; }
        public Interlocutor NadSu { get; set; }
        public Interlocutor NadSco { get; set; }
        public Interlocutor NadII { get; set; }
        public Interlocutor NadDp { get; set; }
        public List<Item> Items { get; set; }

        public decimal IvaTipus { get; set; }
        public decimal RecarrecEquivalenciaTipus { get; set; }
        private string MsgId { get; set; } // Amazon max 14 digits
        private string Sender { get; set; }
        private string Receiver { get; set; }

        private string Centro;
        private string Depto;
        private List<string> Segments;
        private string InterchangeRef;


        public static DTOEdiInvoic Factory(string sender, string receiver)
        {
            DTOEdiInvoic retval = new DTOEdiInvoic();
            retval.Sender = sender;
            retval.Receiver = receiver;
            retval.Items = new List<Item>();
            return retval;
        }

        public string EdiMessage()
        {
            Segments = new List<string>();
            WriteMessageHeader();
            WriteInvoiceHeader();
            WriteInvoiceItems();
            WriteInvoiceFooter();
            WriteMessageFooter();
            return MessageFromSegments();
        }

        private void WriteMessageHeader()
        {
            DateTime now = DTO.GlobalVariables.Now();
            InterchangeRef = now.ToString("FFFFFF");
            MsgId = string.Format("{0}.{1:fff}", InterchangeRef, DTO.GlobalVariables.Now());
            Segments.Add(String.Format("UNB+UNOC:3+{0}:14+{1}:14+{2}:{3}+{4}'", Sender, Receiver, DTO.GlobalVariables.Now().ToString("yyMMdd"), DTO.GlobalVariables.Now().ToString("HHmm"), InterchangeRef));
        }
        private void WriteInvoiceHeader()
        {
            Segments.Add(String.Format("UNH+{0}+INVOIC:D:93A:UN:EAN007'", MsgId));
            Segments.Add(String.Format("BGM+380+{0}'", InvoiceNumber));
            Segments.Add(String.Format("DTM+137:{0:yyyyMMdd}:102'", InvoiceFch));

            if (IsSingleDelivery())
            {
                Segments.Add(String.Format("RFF+DQ:{0}'", Items.First().DeliveryNumber));
                Segments.Add(String.Format("DTM+171:{0:yyyyMMdd}:102'", Items.First().DeliveryFch));
            }

            string orderNumber = Items.First().PurchaseOrderNumber;
            if (Integracions.ElCorteIngles.Globals.matchesGln(NadBy.Ean))
                Integracions.ElCorteIngles.Globals.splitOrderConcept(Items.First().PurchaseOrderNumber, ref orderNumber, ref Centro, ref Depto);

            if (IsSinglePurchaseOrder())
            {
                Segments.Add(String.Format("RFF+ON:{0}'", orderNumber));
                Segments.Add(String.Format("DTM+171:{0:yyyyMMdd}:102'", Items.First().PurchaseOrderFch));
            }

            Segments.Add(String.Format("NAD+BY+{0}::9++{1}+{2}+{3}++{4}'", NadBy.Ean, NadBy.Nom.RemoveDiacritics(), NadBy.Address.RemoveDiacritics(), NadBy.Location.RemoveDiacritics(), NadBy.Zip));
            Segments.Add(String.Format("RFF+VA:{0}'", NadBy.Nif));
            if (!string.IsNullOrEmpty(NadBy.Departamento))
            {
                Segments.Add(String.Format("RFF+API:{0}'", NadBy.Departamento));
            }

            Segments.Add(String.Format("NAD+IV+{0}::9++{1}+{2}+{3}++{4}'", NadIv.Ean, NadIv.Nom, NadIv.Address, NadIv.Location, NadIv.Zip));
            if (!string.IsNullOrEmpty(NadIv.Nif))
                Segments.Add(String.Format("RFF+VA:{0}'", NadIv.Nif));

            Segments.Add(String.Format("NAD+BCO+{0}::9++{1}+{2}+{3}++{4}'", NadBco.Ean, NadBco.Nom, NadBco.Address, NadBco.Location, NadBco.Zip));
            if (!string.IsNullOrEmpty(NadBco.Nif))
                Segments.Add(String.Format("RFF+VA:{0}'", NadBco.Nif));

            Segments.Add(String.Format("NAD+SU+{0}::9++{1}::{2}+{3}+{4}++{5}'", NadSu.Ean, NadSu.Nom, NadSu.RegistroMercantil, NadSu.Address, NadSu.Location, NadSu.Zip));
            if (!string.IsNullOrEmpty(NadSu.Nif))
                Segments.Add(String.Format("RFF+VA:{0}'", NadSu.Nif));

            Segments.Add(String.Format("NAD+SCO+{0}::9++{1}::{2}+{3}+{4}++{5}'", NadSco.Ean, NadSco.Nom, NadSco.RegistroMercantil, NadSco.Address, NadSco.Location, NadSco.Zip));
            if (!string.IsNullOrEmpty(NadSco.Nif))
                Segments.Add(String.Format("RFF+VA:{0}'", NadSco.Nif));

            Segments.Add(String.Format("NAD+II+{0}::9++{1}+{2}+{3}++{4}'", NadII.Ean, NadII.Nom, NadII.Address, NadII.Location, NadII.Zip));
            if (!string.IsNullOrEmpty(NadII.Nif))
                Segments.Add(String.Format("RFF+VA:{0}'", NadII.Nif));

            Segments.Add(String.Format("NAD+DP+{0}::9++{1}+{2}+{3}++{4}'", NadDp.Ean, NadDp.Nom, NadDp.Address, NadDp.Location, NadDp.Zip));
            if (!string.IsNullOrEmpty(NadDp.Nif))
                Segments.Add(String.Format("RFF+VA:{0}'", NadDp.Nif));

            Segments.Add(String.Format("CUX+2:{0}:4'", "EUR"));
        }

        private void WriteInvoiceItems()
        {
            foreach (Item item in Items)
            {
                Segments.Add(String.Format("LIN+{0}++{1}:EN'", Items.IndexOf(item) + 1, item.Ean));
                Segments.Add(String.Format("PIA+1+{0}:SA'", item.SkuId));
                Segments.Add(String.Format("IMD+F+M+:::{0}'", item.SkuNom));
                Segments.Add(String.Format("QTY+47:{0}:PCE'", item.Qty));
                Segments.Add(String.Format("PRI+AAB:{0}'", item.GrossPrice.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)));
                Segments.Add(String.Format("PRI+AAA:{0}'", item.NetPrice().ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)));
                Segments.Add(String.Format("MOA+66:{0}'", item.NetAmount().ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)));

                if (!IsSingleDelivery())
                {
                    Segments.Add(String.Format("RFF+DQ:{0}'", item.DeliveryNumber));
                    Segments.Add(String.Format("DTM+171:{0:yyyyMMdd}:102'", item.DeliveryFch));
                }

                if (!IsSinglePurchaseOrder())
                {
                    string orderNumber = item.PurchaseOrderNumber;
                    if (Integracions.ElCorteIngles.Globals.matchesGln(NadBy.Ean))
                        Integracions.ElCorteIngles.Globals.splitOrderConcept(item.PurchaseOrderNumber, ref orderNumber, ref Centro, ref Depto);
                    Segments.Add(String.Format("RFF+ON:{0}'", orderNumber));
                    Segments.Add(String.Format("DTM+171:{0:yyyyMMdd}:102'", item.PurchaseOrderFch));
                }

            }
        }
        private void WriteInvoiceFooter()
        {
            int linCount = Segments.Where(x => x.StartsWith("LIN")).Count();
            Segments.Add(String.Format("CNT+2:{0}'", linCount)); //Total value of the quantity segments at line level in a message
            Segments.Add(String.Format("MOA+79:{0}'", BaseImponible().ToString("0.00", System.Globalization.CultureInfo.InvariantCulture))); //Total line items amount
            Segments.Add(String.Format("MOA+125:{0}'", TotalAPagar().ToString("0.00", System.Globalization.CultureInfo.InvariantCulture))); //Importe total a pagar 
            Segments.Add(String.Format("MOA+139:{0}'", TotalAPagar().ToString("0.00", System.Globalization.CultureInfo.InvariantCulture))); //Importe total a pagar 

            //modificació del 12/5/21: a solicitud de Juan Cenzano de TradeInn, posem MOA+176 despres del segment TAX i no abans com fins aleshores
            if (IvaTipus != 0)
                Segments.Add(String.Format("TAX+7+VAT+++:::{0}'", IvaTipus));
            if (RecarrecEquivalenciaTipus != 0)
                Segments.Add(String.Format("TAX+7+RE+++:::{0}'", RecarrecEquivalenciaTipus));
            Segments.Add(String.Format("MOA+176:{0}'", TotalTaxes().ToString("0.00", System.Globalization.CultureInfo.InvariantCulture))); //Total of all duty/tax/fee amounts
        }

        private decimal BaseImponible()
        {
            return Items.Sum(x => x.NetAmount());
        }

        private decimal IvaAmount()
        {
            decimal retval = 0;
            if (IvaTipus > 0)
                retval = Math.Round(BaseImponible() * IvaTipus / 100, 2);
            return retval;
        }

        private decimal RecarrecEquivalenciaAmount()
        {
            decimal retval = 0;
            if (RecarrecEquivalenciaTipus > 0)
                retval = Math.Round(BaseImponible() * RecarrecEquivalenciaTipus / 100, 2);
            return retval;
        }

        private decimal TotalTaxes()
        {
            decimal retval = IvaAmount() + RecarrecEquivalenciaAmount();
            return retval;
        }

        private decimal TotalAPagar()
        {
            decimal retval = BaseImponible() + TotalTaxes();
            return retval;
        }

        private void WriteMessageFooter()
        {
            Segments.Add(String.Format("UNT+{0}+{1}'", Segments.Count + 2, MsgId)); // total number of segments in the message + the message reference numbered detailed here should equal the one specified in the UNH segment.
            Segments.Add(String.Format("UNZ+{0}+{1}'", 1, InterchangeRef)); // total number of segments in the message + the message reference numbered detailed here should equal the one specified in the UNH segment.
        }

        private string MessageFromSegments()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string segment in Segments)
                sb.AppendLine(segment);

            string retval = sb.ToString();
            return retval;
        }

        public Boolean IsSingleDelivery()
        {
            return Items.All(x => x.DeliveryNumber == Items.First().DeliveryNumber);
        }

        public Boolean IsSinglePurchaseOrder()
        {
            return Items.All(x => x.PurchaseOrderNumber == Items.First().PurchaseOrderNumber);
        }

        public class Item
        {
            public string DeliveryNumber { get; set; }
            public DateTime DeliveryFch { get; set; }
            public string PurchaseOrderNumber { get; set; }
            public DateTime PurchaseOrderFch { get; set; }
            public string Ean { get; set; }
            public string SkuId { get; set; }
            public string SkuNom { get; set; }
            public int Qty { get; set; }
            public decimal Dto { get; set; }
            public decimal GrossPrice { get; set; }

            public decimal NetPrice()
            {
                decimal retval = Math.Round(GrossPrice * (100 - Dto) / 100, 2);
                return retval;
            }
            public decimal NetAmount()
            {
                decimal retval = Qty * NetPrice();
                return retval;
            }
        }

        public class Interlocutor
        {
            public string Ean { get; set; }
            public string Nom { get; set; }
            public string Address { get; set; }
            public string Location { get; set; }
            public string Zip { get; set; }
            public string RegistroMercantil { get; set; }
            public string Nif { get; set; }
            public string Departamento { get; set; } //ECI

            public static Interlocutor Factory(string ean, string nom = "", string address = "", string location = "", string zip = "", string nif = "", string registroMercantil = "", string departamento = "")
            {
                Interlocutor retval = new Interlocutor();
                retval.Ean = ean;
                retval.Nom = nom;
                retval.Address = address;
                retval.Location = location;
                retval.Zip = zip;
                retval.Nif = nif;
                retval.RegistroMercantil = registroMercantil;
                retval.Departamento = departamento;
                return retval;
            }
        }
    }
}
