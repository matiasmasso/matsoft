using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdiHelperStd
{
    public class Desadv : EdiFile
    {
        public string MessageId { get; set; }
        public string DocNumber { get; set; }
        public DateTime? DocDate { get; set; }
        public DateTime? DespatchDate { get; set; }
        public string Supplier { get; set; }
        public string Buyer { get; set; }
        public string DeliverTo { get; set; }
        public List<ShippingContainer> ShippingContainers { get; set; }
        public List<Item> Items { get; set; }

        private int hierarchicalNumber;
        private int _itemsCount;

        public Desadv()
        {
            base.Schema = Schemas.DESADV;
            this.ShippingContainers = new List<ShippingContainer>();
            this.Items = new List<Item>();
        }

        public static Desadv Factory(List<Exception> exs, string src)
        {
            Desadv retval = new Desadv();
            retval.Load(exs, src);
            return retval;
        }

        public static Desadv Factory(string supplier, string buyer, string deliverTo, string docNumber, DateTime docDate, DateTime despatchDate)
        {
            Desadv retval = new Desadv();
            retval.MessageId = string.Format("{0}.{1:yyyyMMddHHmmssfff}", docNumber, DateTime.Now);
            retval.Supplier = supplier;
            retval.Buyer = buyer;
            retval.DeliverTo = deliverTo;
            retval.DocNumber = docNumber;
            retval.DocDate = docDate;
            retval.DespatchDate = despatchDate;
            retval.Items = new List<Item>();
            return retval;
        }


        public bool Load(List<Exception> exs, string src)
        {
            base.Load(src);
            switch (base.Format)
            {
                case Formats.Native:
                    LoadFromNative(exs, src);
                    break;
                case Formats.Ediversa:
                    break;
            }
            return exs.Count == 0;
        }

        public bool LoadFromNative(List<Exception> exs, string src)
        {
            ShippingContainer shippingContainer = null;
            Item item = null;
            List<ShippingContainer> flatShippingContainerList = new List<ShippingContainer>();

            foreach (EdiFile.Segment segment in base.Segments)
            {
                switch (segment.Tag)
                {
                    case Segment.Tags.BGM:
                        this.DocNumber = base.FieldValue(exs, segment, 2);
                        break;
                    case Segment.Tags.DTM:
                        switch (base.FieldValue(exs, segment, 1, 0))
                        {
                            case "137":
                                this.DocDate = base.FieldDate(exs, segment, 1, 1);
                                break;
                            case "11":
                                this.DespatchDate = base.FieldDate(exs, segment, 1, 1);
                                break;
                        }
                        break;
                    case Segment.Tags.NAD:
                        switch (base.FieldValue(exs, segment, 1))
                        {
                            case "BY":
                                this.Buyer = base.FieldValue(exs, segment, 2, 0);
                                break;
                            case "DP":
                                this.DeliverTo = base.FieldValue(exs, segment, 2, 0);
                                break;
                            case "SU":
                                this.Supplier = base.FieldValue(exs, segment, 2, 0);
                                break;
                        }
                        break;
                    case Segment.Tags.CPS:
                        int hierarchicalNumber = base.FieldInt(exs, segment, 1);
                        if (hierarchicalNumber > 1)
                        {
                            shippingContainer = new ShippingContainer(hierarchicalNumber);

                            int parentHierarchicalNumber = base.FieldInt(exs, segment, 2);
                            if (parentHierarchicalNumber == 1)
                                this.ShippingContainers.Add(shippingContainer);
                            else
                            {
                                ShippingContainer parent = flatShippingContainerList.FirstOrDefault(x => x.HierarchicalNumber == parentHierarchicalNumber);
                                if (parent == null)
                                    exs.Add(new Exception());
                                else
                                    parent.ShippingContainers.Add(shippingContainer);
                            }

                            flatShippingContainerList.Add(shippingContainer);
                        }
                        break;
                    case Segment.Tags.PAC:
                        if (shippingContainer != null) //skip CPS+1'
                        {
                            string value = base.FieldValue(exs, segment, 3);
                            switch (value)
                            {
                                case "09":
                                    shippingContainer.Packaging = ShippingContainer.Packagings.ReturnablePallet;
                                    break;
                                case "201":
                                    shippingContainer.Packaging = ShippingContainer.Packagings.EuroPallet;
                                    break;
                                case "PK":
                                    shippingContainer.Packaging = ShippingContainer.Packagings.Package;
                                    break;
                                case "CT":
                                    shippingContainer.Packaging = ShippingContainer.Packagings.Carton;
                                    break;
                                case "BX":
                                    shippingContainer.Packaging = ShippingContainer.Packagings.Box;
                                    break;
                            }
                        }
                        break;
                    case Segment.Tags.GIN:
                        shippingContainer.SSCC = base.FieldValue(exs, segment, 2);
                        break;
                    case Segment.Tags.LIN:
                        item = new Item();
                        item.LineNumber = base.FieldInt(exs, segment, 1);
                        item.Sku = base.FieldValue(exs, segment, 3);
                        shippingContainer.Items.Add(item);
                        break;
                    case Segment.Tags.QTY:
                        string typeOfQty = base.FieldValue(exs, segment, 1, 0);
                        if (typeOfQty == "12") //despatch Qty
                            item.Qty = base.FieldInt(exs, segment, 1, 1);
                        break;
                    case Segment.Tags.IMD:
                        item.Description = base.FieldValue(exs, segment, 3, 3);
                        break;
                    case Segment.Tags.RFF:
                        string qualifier = base.FieldValue(exs, segment, 1, 0);
                        if (qualifier == "ON")
                        {
                            item.CustomerOrder = base.FieldValue(exs, segment, 1, 1);
                            if (base.Components(exs, segment, 1).Count > 2)
                                item.CustomerOrderLine = base.FieldInt(exs, segment, 1, 2);
                        }
                        break;
                }
            }
            return exs.Count == 0;
        }

        public string Message(List<Exception> exs, Formats format)
        {
            string retval = "";
            switch (format)
            {
                case Formats.Native:
                    retval = NativeMessage(exs);
                    break;
                case Formats.Ediversa:
                    retval = EdiversaMessage();
                    break;
            }
            return retval;

        }
        private string NativeMessage(List<Exception> exs)
        {
            _itemsCount = 0;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format("UNH+{0}+DESADV:D:96A:UN'", this.MessageId));
            sb.AppendLine(string.Format("BGM+351+{0}+9'", this.DocNumber));
            if (this.DocDate.HasValue)
                sb.AppendLine(string.Format("DTM+137:{0}:102'", this.DocDate.Value.ToString("yyyyMMdd")));
            else
                exs.Add(new Exception("missing document date"));

            if (this.DespatchDate.HasValue)
                sb.AppendLine(string.Format("DTM+11:{0}:102'", this.DocDate.Value.ToString("yyyyMMdd")));
            else
                exs.Add(new Exception("missing despatch date"));

            if (this.Supplier == "")
                exs.Add(new Exception("missing supplier"));
            else
                sb.AppendLine(string.Format("NAD+SU+{0}::9'", this.Supplier));

            if (this.Buyer == "")
                exs.Add(new Exception("missing buyer"));
            else
                sb.AppendLine(string.Format("NAD+BY+{0}::9'", this.Buyer));

            if (this.DeliverTo == "")
                exs.Add(new Exception("missing delivery platform"));
            else
                sb.AppendLine(string.Format("NAD+DP+{0}::9'", this.DeliverTo));

            hierarchicalNumber = 1;
            sb.AppendLine(string.Format("CPS+{0}'", hierarchicalNumber));
            if (this.ShippingContainers.Count > 0)
            {
                sb.AppendLine(string.Format("PAC+{0}+52'", this.ShippingContainers.Count));
                foreach (ShippingContainer shippingContainer in this.ShippingContainers)
                {
                    AddShippingContainer(exs, shippingContainer, 1, sb);
                }

            }

            sb.AppendLine(String.Format("CNT+2:{0}'", _itemsCount)); //Total value of the quantity segments at line level in a message
            var lines = sb.ToString().Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None).Length;
            sb.AppendLine(String.Format("UNT+{0}+{1}'", lines + 1, MessageId)); // total number of segments in the message + the message reference numbered detailed here should equal the one specified in the UNH segment.


            string retval = sb.ToString();
            return retval;
        }

        private void AddShippingContainer(List<Exception> exs, ShippingContainer shippingContainer, int parentHierarchicalNumber, StringBuilder sb)
        {
            hierarchicalNumber += 1;
            sb.AppendLine(string.Format("CPS+{0}+{1}'", hierarchicalNumber, parentHierarchicalNumber, sb));
            sb.AppendLine(string.Format("PAC+{0}+52'", shippingContainer.ShippingContainers.Count));
            parentHierarchicalNumber = hierarchicalNumber;
            foreach (ShippingContainer children in shippingContainer.ShippingContainers)
            {
                AddShippingContainer(exs, shippingContainer, parentHierarchicalNumber, sb);
            }
            foreach (Item item in shippingContainer.Items)
            {
                AddItem(exs, item, sb);
            }
        }
        private void AddItem(List<Exception> exs, Item item, StringBuilder sb)
        {
            sb.AppendLine(String.Format("LIN+{0}++{1}:EN'", item.Sku, item.LineNumber));
            sb.AppendLine(String.Format("RFF+ON:{0}'", item.CustomerOrder));
            sb.AppendLine(String.Format("IMD+F+M+:::{0}'", item.Description));
            sb.AppendLine(String.Format("QTY+12:{0}:PCE'", item.Qty));
            _itemsCount += 1;
        }
        private string EdiversaMessage()
        {
            string retval = "";
            return retval;
        }

        public class ShippingContainer
        {
            public int HierarchicalNumber { get; set; }
            public List<ShippingContainer> ShippingContainers { get; set; }
            public List<Item> Items { get; set; }
            public string SSCC { get; set; }
            public Packagings Packaging { get; set; }
            public enum Packagings
            {
                Unknown,
                ReturnablePallet,
                EuroPallet,
                Package,
                Carton,
                Box
            }

            public ShippingContainer(int hierarchicalNumber)
            {
                this.HierarchicalNumber = hierarchicalNumber;
                this.ShippingContainers = new List<ShippingContainer>();
                this.Items = new List<Item>();
            }

        }

        public class Item
        {
            public int LineNumber { get; set; }
            public string Sku { get; set; }
            public int Qty { get; set; }
            public string Description { get; set; }
            public string CustomerOrder { get; set; }
            public int CustomerOrderLine { get; set; }

            public static Item Factory(string customerOrder, string sku, string description, int qty)
            {
                Item retval = new Item();
                retval.CustomerOrder = customerOrder;
                retval.Sku = sku;
                retval.Description = description;
                retval.Qty = qty;
                return retval;
            }
        }

    }
}
