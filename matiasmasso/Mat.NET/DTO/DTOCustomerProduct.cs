using System;

namespace DTO
{
    public class DTOCustomerProduct : DTOBaseGuid
    {
        public DTOProductSku Sku { get; set; }
        public DTOCustomer Customer { get; set; }
        public string @Ref { get; set; }
        public string DUN14 { get; set; }
        public string Dsc { get; set; }
        public string Color { get; set; }
        public DateTime FchFrom { get; set; }
        public DateTime FchTo { get; set; }
        public DTOYearMonth YearMonth { get; set; }
        public int Qty { get; set; }
        public DTO.Integracions.ElCorteIngles.Dept EciDept { get; set; }


        public DTOCustomerProduct() : base()
        {
        }

        public DTOCustomerProduct(Guid oGuid) : base(oGuid)
        {
        }

        public static string Dun14OrDefault(DTOCustomerProduct oCustomerProduct)
        {
            string retval = "";
            if (oCustomerProduct != null)
                retval = oCustomerProduct.DUN14;
            return retval;
        }

        public class Compact : DTOBaseGuid
        {
            public string Ref { get; set; }

            public Compact() : base()
            {
            }

            public Compact(Guid oGuid, string sRef) : base(oGuid)
            {
                this.Ref = sRef;
            }

        }
    }
}
