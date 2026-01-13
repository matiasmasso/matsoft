using System;

namespace DTO
{
    public class DTOPmc : DTOBaseGuid
    {
        public DateTime Fch { get; set; }
        public Guid DeliveryGuid { get; set; }
        public int DeliveryNum { get; set; }
        public DTOGuidNom Brand { get; set; }
        public DTOGuidNom Category { get; set; }
        public DTOGuidNom Sku { get; set; }
        public DTOGuidNom Customer { get; set; }
        public int Qty { get; set; }
        public decimal Eur { get; set; }
        public decimal Dto { get; set; }
        public decimal Pmc { get; set; }
    }
}
