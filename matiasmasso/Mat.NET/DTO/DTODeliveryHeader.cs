using System;

namespace DTO
{
    public class DTODeliveryHeader
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }
        public DateTime Fch { get; set; }
        public DTOCustomer Customer { get; set; }
        public DTOAmt ImportAdicional { get; set; }
        public DTOAmt Import { get; set; }
        public DTOTransmisio Transmisio { get; set; }
        public DTOInvoice Invoice { get; set; }
        public DTOPurchaseOrder.Codis Cod { get; set; }
        public DTO.DTOCustomer.CashCodes CashCod { get; set; }
        public DTO.DTOCustomer.PortsCodes PortsCod { get; set; }
        public bool Facturable { get; set; }
        public DTOTransportista Transportista { get; set; }
        public string Tracking { get; set; }
        public DTODocFile EtiquetesTransport { get; set; }
        public DTOUsrLog2 UsrLog { get; set; }


        public class DTOCustomer
        {
            public Guid Guid { get; set; }
            public string FullNom { get; set; }
        }

        public class DTOAmt
        {
            public decimal eur { get; set; }
        }

        public class DTOTransmisio
        {
            public Guid Guid { get; set; }
            public int id { get; set; }
        }

        public class DTOInvoice
        {
            public Guid Guid { get; set; }
            public int num { get; set; }
        }

        public class DTOUsrLog
        {
            public DTOUser usrCreated { get; set; }
        }
        public class DTOUser
        {
            public Guid Guid { get; set; }
            public string emailAddress { get; set; }
            public string nickname { get; set; }
        }

        public class DTOTransportista
        {
            public Guid Guid { get; set; }
            public string abr { get; set; }
        }

        public class DTODocFile
        {
            public string hash { get; set; }
        }
    }
}
