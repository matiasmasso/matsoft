using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOEdiOrder : DTOBaseGuid
    {
        public string DocNum { get; set; }
        public DateTime FchDoc { get; set; }
        public DateTime FchDeliveryMin { get; set; }
        public DateTime FchDeliveryMax { get; set; }
        public string Tipo { get; set; }
        public DTOCur Cur { get; set; }
        public string Obs { get; set; }

        public DTOCustomer Comprador { get; set; }
        public DTOEan NADMS { get; set; }
        public DTOEan ProveedorEAN { get; set; }
        public DTOEan CompradorEAN { get; set; }
        public DTOEan FacturarAEAN { get; set; }
        public DTOEan ReceptorMercanciaEAN { get; set; }
        public DTOContact Proveedor { get; set; }
        public DTOCustomer Customer { get; set; }
        public DTOCustomer FacturarA { get; set; } // a qui es factura
        public DTOContact ReceptorMercancia { get; set; }

        public string Centro { get; set; }
        public string Departamento { get; set; }
        public string NumProveidor { get; set; }
        public DTOAmt Amt { get; set; }
        public List<Item> Items { get; set; }


        public DTOEdiOrder() : base() { }
        public DTOEdiOrder(Guid guid) : base(guid) { }

        public static DTOEdiOrder Factory()
        {
            DTOEdiOrder retval = new DTOEdiOrder();
            return retval;
        }
        public class Collection : List<DTOEdiOrder>
        {

        }

        public class Item : DTOBaseGuid
        {
            public int Lin { get; set; }
            public DTOEan Ean { get; set; }
            public string RefProveidor { get; set; }
            public string RefClient { get; set; }
            public string Dsc { get; set; }
            public DTOProductSku Sku { get; set; }
            public int Qty { get; set; }
            public DTOAmt Preu { get; set; }

            public DTOAmt PreuNet { get; set; }
            public decimal Dto { get; set; }
            public DTOUser SkipPreuValidationUser { get; set; }
            public DateTime SkipPreuValidationFch { get; set; }
            public DTOUser SkipDtoValidationUser { get; set; }
            public DateTime SkipDtoValidationFch { get; set; }
            public DTOUser SkipItemUser { get; set; }
            public DateTime SkipItemFch { get; set; }
            public List<DTOEdiversaException> Exceptions { get; set; }

            public Item() : base()
            {
                Exceptions = new List<DTOEdiversaException>();
            }

            public Item(Guid guid) : base(guid)
            {
                Exceptions = new List<DTOEdiversaException>();
            }

        }
    }


}
