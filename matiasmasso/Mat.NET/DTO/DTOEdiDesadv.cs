using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class DTOEdiDesadv
    {
        public string Supplier { get; set; }
        public string CustomerSupplierCod { get; set; } //num.de proveidor assignat per el client
        public string Buyer { get; set; }
        public string DeliveryFrom { get; set; }
        public string DeliveryFromZip { get; set; }
        public string DeliveryFromCountryISO { get; set; }
        public string DeliveryTo { get; set; }
        public string DeliveryNum { get; set; }
        public DateTime DeliveryDate { get; set; }

        private string MsgId { get; set; } // Amazon max 14 digits

        private List<Pallet> Pallets { get; set; }
        private List<Package> Packages { get; set; }
        private List<Item> Items { get; set; }


        public DTOEdiDesadv()
        {
            Items = new List<Item>();
            Packages = new List<Package>();
            Pallets = new List<Pallet>();
        }
        public static DTOEdiDesadv Factory(string supplier, string buyer, string deliveryFrom, string deliveryTo, string deliveryNum, DateTime deliveryDate)
        {
            DTOEdiDesadv retval = new DTOEdiDesadv();
            retval.MsgId = string.Format("{0}.{1:fff}", deliveryNum, DTO.GlobalVariables.Now());
            retval.Supplier = supplier;
            retval.Buyer = buyer;
            retval.DeliveryFrom = deliveryFrom;
            retval.DeliveryTo = deliveryTo;
            retval.DeliveryNum = deliveryNum;
            retval.DeliveryDate = deliveryDate;
            retval.Items = new List<Item>();
            return retval;
        }

        public Package AddPackage(int cps, string sscc)
        {
            Package retval = new Package(cps, sscc);
            Packages.Add(retval);
            return retval;
        }
        public Pallet AddPallet(int cps, string sscc)
        {
            Pallet retval = new Pallet(cps, sscc);
            Pallets.Add(retval);
            return retval;
        }

        public int AddItem(string orderNum, string ean13, string description, int qty) //To Deprecate
        {
            Item item = new Item();
            item.OrderNum = orderNum;
            item.Ean13 = ean13;
            item.Description = description;
            item.Qty = qty;
            Items.Add(item);
            item.Lin = Items.Count;
            return Items.Count;
        }

        public string DefaultFilename()
        {
            string retval = string.Format("DESADV_{0}.txt", MsgId);
            return retval;
        }


        public string EdiversaMessage()
        {
            //todo: INCLUDE message receiver NADMR = order NADMS message sender
            List<string> segments = new List<string>();
            segments.Add("DESADV_D_96A_UN_EAN005");
            segments.Add(String.Format("BGM|{0}|{1}|{2}", DeliveryNum, 351, 9));
            //segments.Add(String.Format("DTM|{0:yyyyMMdd}|||{1:yyyyMMdd}||||||||||{2:yyyyMMdd}", DeliveryDate, DeliveryDate, TimeHelper.AddDiasHabils(DeliveryDate, 2))); //Document/message date/time
            segments.Add(String.Format("DTM|{0:yyyyMMdd}||||{1:yyyyMMdd}||||||||||{2:yyyyMMdd}", DeliveryDate, DeliveryDate, TimeHelper.AddDiasHabils(DeliveryDate, 2))); //Document/message date/time
            segments.Add(String.Format("RFF|DQ|{0}", DeliveryNum));
            segments.Add(String.Format("NADBY|{0}|9", Buyer.RemoveDiacritics()));
            segments.Add(String.Format("NADSU|{0}|9", Supplier));
            segments.Add(String.Format("NADDP|{0}|9", DeliveryTo));
            //segments.Add(String.Format("NADSF|{0}|9|||||{1}||{2}", DeliveryFrom, DeliveryFromZip, DeliveryFromCountryISO));
            segments.Add(String.Format("NADSF|{0}|9|||||{1}||{2}", DeliveryFrom, DeliveryFromZip, DeliveryFromCountryISO));
            segments.Add(String.Format("TDT|30||||20|31"));


            if (Pallets.Count > 0)
            {
                segments.Add("CPS|1"); //first level of grouping (shipment)
                segments.Add(String.Format("PAC|{0}|{1}", Pallets.Count, "09")); //Number of pallets | returnable pallets 
                EdiversaAddPalletsSegments(segments, Pallets);
            }

            if (Packages.Count == 0)
            {
                EdiversaAddItemsSegments(segments, Items);
            }
            else
            {
                segments.Add("CPS|1"); //first level of grouping (shipment)
                segments.Add(String.Format("PAC|{0}|{1}", Packages.Count, "PK")); //Number of packages | packages
                EdiversaAddPackagesSegments(segments, Packages, 1);
            }

            int linCount = segments.Where(x => x.StartsWith("LIN")).Count();
            segments.Add(String.Format("CNTRES|||{0}", linCount));  //Total value of the quantity segments at line level in a message

            StringBuilder sb = new StringBuilder();
            foreach (string segment in segments)
                sb.AppendLine(segment);

            string retval = sb.ToString();
            return retval;
        }

        private void EdiversaAddPalletsSegments(List<string> segments, List<Pallet> pallets)
        {
            foreach (Pallet pallet in pallets)
            {
                segments.Add(String.Format("CPS|{0}|1", pallet.Cps)); // hierarchical number for this pallet | parent CPS hierarchical number
                segments.Add(String.Format("PAC|{0}|{1}|{2}", pallet.Packages.Count, "CT", "52")); //number of packages in this pallet | type of package (CT=carton)
                segments.Add(String.Format("PCI|33E|BJ|{0}", pallet.SSCC)); //pallet SSCC number
                EdiversaAddPackagesSegments(segments, pallet.Packages, pallet.Cps);
            }
        }

        private void EdiversaAddPackagesSegments(List<string> segments, List<Package> packages, int parentCps)
        {
            foreach (Package package in packages)
            {
                segments.Add(String.Format("CPS|{0}|{1}", package.Cps, parentCps)); // hierarchical Cps number for this package | Parent hierarchical number
                segments.Add(String.Format("PAC|{0}|{1}", 1, "PK")); //Number of packages | packages
                segments.Add(String.Format("PCI|33E|BJ|{0}", package.SSCC)); //package SSCC number
                EdiversaAddItemsSegments(segments, package.Items);
            }
        }

        private void EdiversaAddItemsSegments(List<string> segments, List<Item> items)
        {
            foreach (Item item in items)
            {
                segments.Add(String.Format("LIN|{0}|EN|{1}", item.Ean13, item.Lin));
                //segments.Add(String.Format("IMDLIN|F|{0}", item.Description)); //retirat per Amazon
                segments.Add(String.Format("QTYLIN|12|{0}|PCE", item.Qty));
                segments.Add(String.Format("RFFLIN|ON|{0}", item.OrderNum));
            }
        }



        public string EdiMessage()
        {
            DateTime now = DTO.GlobalVariables.Now();
            string interchangeRef = now.ToString("FFFFFF");

            string sender = this.Supplier;
            string receiver = this.Buyer;
            switch (DTOEdiversaFile.ReadInterlocutor(DTOEan.Factory(receiver)))
            {
                case DTOEdiversaFile.Interlocutors.amazon:
                    receiver = "5450534000000";
                    break;
            }

            List<string> segments = new List<string>();
            segments.Add(String.Format("UNB+UNOC:3+{0}:14+{1}:14+{2}:{3}+{4}+++++EANCOM'", sender, receiver, DTO.GlobalVariables.Now().ToString("yyMMdd"), DateTime.Now.ToString("HHmm"), interchangeRef));
            segments.Add(String.Format("UNH+{0}+DESADV:D:96A:UN+EAN005'", MsgId));
            segments.Add(String.Format("BGM+351+{0}+9'", DeliveryNum));
            segments.Add(String.Format("DTM+137:{0:yyyyMMdd}:102'", DeliveryDate)); //Document/message date/time
            segments.Add(String.Format("DTM+132:{0:yyyyMMdd}:102'", TimeHelper.AddDiasHabils(DeliveryDate, 2))); //Arrival date/time
            segments.Add(String.Format("DTM+11:{0:yyyyMMdd}:102'", DeliveryDate)); //Despatch date and/or time
            segments.Add(String.Format("RFF+DQ:{0}'", DeliveryNum)); //Delivery note number
            //segments.Add(String.Format("RFF+PK:{0}'", DeliveryNum)); //PK set for TradeInn and moved to DQ for Amazon
            segments.Add(String.Format("NAD+SU+{0}::9'", Supplier));
            segments.Add(String.Format("NAD+BY+{0}::9'", Buyer));
            segments.Add(String.Format("NAD+SF+{0}::9++++++{1}+{2}'", DeliveryFrom, DeliveryFromZip, DeliveryFromCountryISO));
            segments.Add(String.Format("NAD+DP+{0}::9'", DeliveryTo));
            segments.Add("TDT+20++30+31'"); //transport per carretera



            if (Pallets.Count > 0)
            {
                segments.Add("CPS+1'"); //first level of grouping (shipment)
                segments.Add(String.Format("PAC+{0}+{1}+{2}'", Pallets.Count, "52", "09")); //Number of pallets | codes UCC/EAN128 | returnable pallets 
                EdiAddPalletsSegments(segments, Pallets);
            }

            if (Packages.Count == 0)
            {
                EdiAddItemsSegments(segments, Items);
            }
            else
            {
                segments.Add("CPS+1'"); //first level of grouping (shipment)
                segments.Add(String.Format("PAC+{0}++{1}'", Packages.Count, "PK")); //Number of packages | codes UCC/EAN128 | package (Amazon format)
                //segments.Add(String.Format("PAC+{0}+{1}+{2}'", Packages.Count, "52", "CT")); //Number of packages | codes UCC/EAN128 | carton boxes
                EdiAddPackagesSegments(segments, Packages, 1);
            }

            int linCount = segments.Where(x => x.StartsWith("LIN")).Count();
            segments.Add(String.Format("CNT+2:{0}'", linCount)); //Total value of the quantity segments at line level in a message
            segments.Add(String.Format("UNT+{0}+{1}'", segments.Count + 2, MsgId)); // total number of segments in the message + the message reference numbered detailed here should equal the one specified in the UNH segment.
            segments.Add(String.Format("UNZ+{0}+{1}'", 1, interchangeRef)); // total number of segments in the message + the message reference numbered detailed here should equal the one specified in the UNH segment.

            StringBuilder sb = new StringBuilder();
            foreach (string segment in segments)
                sb.AppendLine(segment);

            string retval = sb.ToString();
            return retval;
        }

        private void EdiAddPalletsSegments(List<string> segments, List<Pallet> pallets)
        {
            foreach (Pallet pallet in pallets)
            {
                segments.Add(String.Format("CPS+{0}+1'", pallet.Cps)); // hierarchical number for this pallet | parent CPS hierarchical number
                segments.Add(String.Format("PAC+{0}+{1}+{2}'", pallet.Packages.Count, "52", "CT")); //Number of packages | codes UCC/EAN128 | cartons
                segments.Add("PCI+33E'"); //pallet marked with serial shipping container code (SSCC)
                segments.Add(String.Format("GIN+BJ+{0}'", pallet.SSCC));

                EdiAddPackagesSegments(segments, pallet.Packages, pallet.Cps);
            }
        }

        private void EdiAddPackagesSegments(List<string> segments, List<Package> packages, int parentCps)
        {
            foreach (Package package in packages)
            {
                segments.Add(String.Format("CPS+{0}+{1}'", package.Cps, parentCps)); // hierarchical Cps number for this package | Parent hierarchical number
                segments.Add(String.Format("PAC+{0}+{1}+{2}'", 1, "52", "PK")); //Number of packages | codes UCC/EAN128 | packages. Included for Amazon 
                segments.Add("PCI+33E'"); //package marked with serial shipping container code (SSCC)
                segments.Add(String.Format("GIN+BJ+{0}'", package.SSCC));
                EdiAddItemsSegments(segments, package.Items);
            }
        }

        private void EdiAddItemsSegments(List<string> segments, List<Item> items)
        {
            foreach (Item item in items)
            {
                segments.Add(String.Format("LIN+{0}++{1}:EN'", item.Lin, item.Ean13));
                segments.Add(String.Format("QTY+12:{0}:PCE'", item.Qty));
                segments.Add(String.Format("RFF+ON:{0}'", item.OrderNum));
                //segments.Add(String.Format("IMD+F+M+:::{0}'", item.Description)); suprimit per Amazon
            }
        }


        public class Pallet
        {
            public int Cps { get; set; }
            public string SSCC { get; set; }
            public List<Package> Packages { get; set; }

            public Pallet(int cps, string sscc)
            {
                Cps = cps;
                SSCC = sscc;
                Packages = new List<Package>();
            }

            public Package AddPackage(int cps, string sscc)
            {
                Package package = new Package(cps, sscc);
                Packages.Add(package);
                return package;
            }

        }


        public class Package
        {
            public int Cps { get; set; }
            public string SSCC { get; set; }
            public List<Item> Items { get; set; }

            public Package(int cps, string sscc)
            {
                Cps = cps;
                SSCC = sscc;
                Items = new List<Item>();
            }

            public int AddItem(string orderNum, string ean13, string description, int qty)
            {
                Item item = new Item();
                item.OrderNum = orderNum;
                item.Ean13 = ean13;
                item.Description = description;
                item.Qty = qty;
                Items.Add(item);
                item.Lin = Items.Count;
                return Items.Count;
            }

        }

        public class Item
        {
            public int Lin { get; set; }
            public string Ean13 { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }

            public string OrderNum { get; set; }
        }
    }
}
