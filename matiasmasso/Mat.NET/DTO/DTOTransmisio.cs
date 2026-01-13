using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOTransmisio : DTOBaseGuid
    {
        public class Compact
        {
            public Guid Guid { get; set; }
            public int Id { get; set; }
        }
        public DTOEmp Emp { get; set; }
        public int Id { get; set; }
        public DateTimeOffset Fch { get; set; }
        public DTOMgz Mgz { get; set; }
        public DTOAmt Amt { get; set; }
        public List<DTODelivery> Deliveries { get; set; }

        public int DeliveriesCount { get; set; }
        public int InvoicedDeliveriesCount { get; set; }
        public int NoFacturablesCount { get; set; }
        public int LinesCount { get; set; }
        public int UnitsCount { get; set; }

        public DTOTransmisio() : base()
        {
        }

        public DTOTransmisio(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOTransmisio Factory(DTOEmp oEmp, DTOMgz oMgz, List<DTODelivery> oDeliveries)
        {
            DTOTransmisio retval = new DTOTransmisio();
            {
                var withBlock = retval;
                withBlock.Emp = oEmp;
                withBlock.Fch = DateTimeOffset.Now;
                withBlock.Mgz = oEmp.Mgz;
                withBlock.Deliveries = oDeliveries;
            }
            return retval;
        }

        public string FileNameDades()
        {
            string s = FilePrefix() + "dades." + base.Guid.ToString() + ".xml";
            return s;
        }

        public string FileNameDocs()
        {
            return FilePrefix() + "documentacio." + base.Guid.ToString() + ".pdf";
        }

        public string FilePrefix()
        {
            return "M+O." + Fch.Year.ToString() + "." + String.Format("{0:0000}", Id) + ".";
        }
    }
}
