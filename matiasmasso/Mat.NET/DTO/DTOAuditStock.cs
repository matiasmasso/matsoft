using System;

namespace DTO
{
    public class DTOAuditStock : DTOBaseGuid
    {
        public int Year { get; set; }
        public DTOExercici Exercici { get; set; }
        public string Ref { get; set; }
        public DTOProductSku Sku { get; set; }
        public string Dsc { get; set; }
        public int Qty { get; set; }
        public string Palet { get; set; }
        public DateTime FchEntrada { get; set; }
        public int Dias { get; set; }
        public string Entrada { get; set; }
        public string Procedencia { get; set; }
        public decimal Cost { get; set; }

        public DTOAuditStock() : base()
        {
        }

        public DTOAuditStock(Guid oGuid) : base(oGuid)
        {
        }
    }
}
