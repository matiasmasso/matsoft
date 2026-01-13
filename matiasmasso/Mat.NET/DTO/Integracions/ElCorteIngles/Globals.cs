using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO.Integracions.ElCorteIngles
{
    class Globals
    {
        public static Boolean matchesGln(string gln)
        {
            Boolean retval = DTOEdiFile.Interlocutor(gln) == DTOEdiversaFile.Interlocutors.ElCorteIngles;
            return retval;
        }

        public static void splitOrderConcept(string concept, ref string orderNumber, ref string centro, ref string depto)
        {
            List<string> segments = concept.Split('/').ToList();
            if (segments.Count >= 3)
            {
                orderNumber = segments.First().Trim();
                if (segments[1].Length >= 5)
                    centro = segments[1].Substring(5).Trim();
                if (segments[2].Length >= 4)
                    depto = segments[2].Substring(4).Trim(); //net del prefixe "dep."
            }
        }

        public static string ComandaNum(DTOPurchaseOrder order)
        {
            string retval = "";
            List<string> segments = order.Concept.Split('/').ToList();
            if (segments.Count == 4)
                retval = segments.First().Trim();
            return retval;
        }
        public static string Centro(DTOPurchaseOrder order)
        {
            string retval = "";
            List<string> segments = order.Concept.Split('/').ToList();
            if (segments.Count == 4 && segments[1].Length >= 5)
                retval = segments[1].Substring(5).Trim();
            return retval;
        }
        public static string Depto(DTOPurchaseOrder order)
        {
            string retval = "";
            List<string> segments = order.Concept.Split('/').ToList();
            if (segments.Count == 4 && segments[2].Length >= 4)
                retval = segments[2].Substring(4).Trim(); //net del prefixe "dep."
            return retval;
        }
        public static string NumProveidor(DTOPurchaseOrder order)
        {
            string retval = "";
            List<string> segments = order.Concept.Split('/').ToList();
            if (segments.Count == 4 && segments[2].Length >= 5)
                retval = segments[3].Substring(5).Trim(); //net del prefixe "prov."
            return retval;
        }

        public static string ProductFamilia(DTOCustomerProduct customerProduct)
        {
            string retval = "";
            if (customerProduct != null && customerProduct.Ref.Length > 3)
            {
                string src = MatHelperStd.TextHelper.RegexSuppress(customerProduct.Ref, "[^0-9]");
                retval = src.Substring(0, 3);
            }
            return retval;
        }

        public static string ProductBarra(DTOCustomerProduct customerProduct)
        {
            string retval = "";
            if (customerProduct != null && customerProduct.Ref.Length > 6)
            {
                string src = MatHelperStd.TextHelper.RegexSuppress(customerProduct.Ref, "[^0-9]");
                retval = src.Substring(3, src.Length - 3);
            }
            return retval;
        }

    }
}
