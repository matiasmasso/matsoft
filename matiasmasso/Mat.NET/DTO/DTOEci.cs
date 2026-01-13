using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOEci
    {
        public static string NumeroDeComanda(DTODelivery oDelivery)
        {
            string retval = "";
            switch (oDelivery.Cod)
            {
                case DTOPurchaseOrder.Codis.client:
                    {
                        DTOPurchaseOrder oOrder = oDelivery.Items.First().PurchaseOrderItem.PurchaseOrder;
                        retval = NumeroDeComanda(oOrder);
                        break;
                    }
            }
            return retval;
        }

        public static string NumeroDeComanda(DTOPurchaseOrder oOrder)
        {
            string retval = "";
            if (!string.IsNullOrEmpty(oOrder.Concept))
            {
                string sPdd = oOrder.Concept;
                int i;
                for (i = 0; i <= sPdd.Length - 1; i++)
                {
                    if (!"0123456789".Contains(sPdd.Substring(i, 1)))
                        break;
                }
                if (i > 0)
                    retval = sPdd.Substring(0, i);
            }
            return retval;
        }

        public static string GetDepartamentoFromFirstAlbPdd(List<DTODelivery> oDeliveries)
        {
            string retval = "";
            // /dep.053/
            foreach (DTODelivery oDelivery in oDeliveries)
            {
                foreach (DTODeliveryItem oItem in oDelivery.Items)
                {
                    if (oItem.PurchaseOrderItem != null)
                    {
                        DTOPurchaseOrder oOrder = oItem.PurchaseOrderItem.PurchaseOrder;
                        string src = oOrder.Concept;
                        int iStart = src.IndexOf("/dep.");
                        if (iStart >= 0)
                        {
                            int iEnd = src.IndexOf("/", iStart + 5);
                            if (iEnd > iStart)
                            {
                                string sSegment = src.Substring(iStart, iEnd - iStart);
                                retval = sSegment.Replace("dep.", "").Replace("/", "");
                                break;
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(retval))
                    break;
            }
            return retval;
        }

        public static void GetDetailsFromPdc(DTOPurchaseOrder oPurchaseOrder, ref string sPedido, ref string sCentro, ref string sDepartamento, ref string sNumProveedor)
        {
            // 30560101/ctro.0050/dep.053/prov.01-030825
            string[] sPdd = oPurchaseOrder.Concept.Split('/');
            if (sPdd.Length == 4)
            {
                sPedido = sPdd[0];
                if (sPdd[1].Length >= 5)
                    sCentro = sPdd[1].Substring(5);
                sDepartamento = sPdd[2].Replace("dep.", "");
                sNumProveedor = sPdd[3].Replace("prov.", "");
            }
        }
    }
}
