using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTORemittanceAdvice
    {
        public DTOProveidor Proveidor { get; set; }
        public DTODocFile DocFile { get; set; }
        public DTOIban Iban { get; set; }
        public List<DTORemittanceAdviceItem> Items { get; set; }

        public static DTOAmt Total(DTORemittanceAdvice value)
        {
            var retval = DTOAmt.Empty();
            foreach (DTORemittanceAdviceItem oItem in value.Items)
                retval.Add(oItem.Amt);
            return retval;
        }
    }

    public class DTORemittanceAdviceItem
    {
        public DateTime Fch { get; set; }
        public string Fra { get; set; }
        public DTOAmt Amt { get; set; }
        public string Url { get; set; }

        public DTODocFile DocFile { get; set; }

        public string Text(DTOLang oLang)
        {
            string retval = oLang.Tradueix("factura", "factura", "invoice") + " " + Fra + " " + oLang.Tradueix("del", "del", "from") + " " + Fch.ToShortDateString();
            return retval;
        }
    }
}
