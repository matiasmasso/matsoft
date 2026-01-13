using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOECITransmGroup : DTOBaseGuid
    {
        public int Ord { get; set; }
        public string Nom { get; set; }
        public DTOContact Platform { get; set; }

        public List<DTOECITransmCentre> Items { get; set; }

        public DTOECITransmGroup() : base()
        {
            Items = new List<DTOECITransmCentre>();
        }

        public DTOECITransmGroup(Guid oGuid) : base(oGuid)
        {
            Items = new List<DTOECITransmCentre>();
        }

        public static List<DTODelivery> SelectedDeliveries(DTOECITransmGroup oGroup, List<DTODelivery> oSrc, ref List<DTODelivery> rest)
        {
            List<DTODelivery> retval = new List<DTODelivery>();
            rest = new List<DTODelivery>();
            foreach (DTODelivery oDelivery in oSrc)
            {
                if (DTOECITransmGroup.Belongs(oGroup, oDelivery))
                    retval.Add(oDelivery);
                else
                    rest.Add(oDelivery);
            }
            return retval;
        }


        public static List<DTODelivery> SortDeliveries(List<DTODelivery> oDeliveries)
        {
            List<DTODelivery> retval = oDeliveries.OrderBy(x => x.Platform.Guid.ToString()).OrderBy(x => x.Customer.Ref).OrderBy(x => DTOEci.NumeroDeComanda(x)).ToList();
            return retval;
        }

        public static bool Belongs(DTOECITransmGroup oGroup, DTODelivery oDelivery)
        {
            bool retval = oGroup.MatchesPlatform(oDelivery) & oGroup.MatchesCenter(oDelivery);
            return retval;
        }
        public static bool Belongs(DTOECITransmGroup oGroup, DTOPurchaseOrder oOrder)
        {
            bool retval = oGroup.MatchesPlatform(oOrder) & oGroup.MatchesCenter(oOrder);
            return retval;
        }

        public bool MatchesPlatform(DTODelivery oDelivery)
        {
            var retval = Platform.Equals(oDelivery.Platform);
            return retval;
        }

        public bool MatchesPlatform(DTOPurchaseOrder oOrder)
        {
            var retval = Platform.Equals(oOrder.Platform);
            return retval;
        }

        public bool MatchesCenter(DTODelivery oDelivery)
        {
            bool retval;
            if (Items.Count == 0)
                retval = true; // si no hi ha centres, s'admet qualsevol centre
            else
                retval = Items.Any(x => x.Centre.Equals(oDelivery.Customer));
            return retval;
        }

        public bool MatchesCenter(DTOPurchaseOrder oOrder)
        {
            bool retval;
            if (Items.Count == 0)
                retval = true; // si no hi ha centres, s'admet qualsevol centre
            else
                retval = Items.Any(x => x.Centre.Equals(oOrder.Customer));
            return retval;
        }

        public static List<DTOContact> Contacts(DTOECITransmGroup oECITransmGroup)
        {
            List<DTOContact> retval = new List<DTOContact>();
            foreach (var item in oECITransmGroup.Items)
                retval.Add(item.Centre);
            return retval;
        }

        public static List<DTODelivery> Sort(List<DTODelivery> oDeliveries)
        {
            List<DTODelivery> retval = oDeliveries.OrderBy(x => x.Platform.Guid.ToString()).OrderBy(x => x.Customer.Ref).OrderBy(x => DTOEci.NumeroDeComanda(x)).ToList();
            return retval;
        }

        public static List<DTODelivery> SortedDeliveries(List<DTOECITransmGroup> oGroups, List<DTODelivery> oDeliveries, List<Exception> exs)
        {
            List<DTODelivery> retval = new List<DTODelivery>();
            List<DTODelivery> rest = oDeliveries;
            foreach (DTOECITransmGroup oGroup in oGroups)
            {
                if (rest.Count == 0)
                    break;

                List<DTODelivery> src = rest;
                List<DTODelivery> oGroupDeliveries = DTOECITransmGroup.SelectedDeliveries(oGroup, src, ref rest);
                var oSortedGroupDeliveries = oGroupDeliveries.OrderBy(x => x.Customer.Ref).OrderBy(x => DTOEci.NumeroDeComanda(x)).ToList();
                retval.AddRange(oSortedGroupDeliveries);
            }

            if (rest.Count > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.AppendLine(string.Format("{0} albarans no s'han pogut assignar a cap grup de transmisió:", rest.Count));
                foreach (var oDelivery in rest)
                    sb.AppendLine(string.Format("centre {0} comanda {1}", oDelivery.Customer.Ref, DTOEci.NumeroDeComanda(oDelivery)));
            }
            return retval;
        }

        public override string ToString()
        {
            return Nom;
        }
    }
}
