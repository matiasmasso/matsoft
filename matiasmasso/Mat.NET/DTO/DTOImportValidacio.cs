using System;

namespace DTO
{
    public class DTOImportValidacio : DTOBaseGuid
    {
        public int Lin { get; set; }
        public DTOProductSku Sku { get; set; }
        public int Qty { get; set; }
        public int Cfm { get; set; }

        public DTOImportValidacio() : base()
        {
        }

        public DTOImportValidacio(Guid oGuid) : base(oGuid)
        {
        }
    }
}
