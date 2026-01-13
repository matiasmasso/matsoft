using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOEdiversaSalesReport : DTOBaseGuid
    {
        public string Id { get; set; }
        public DateTime Fch { get; set; }
        public DTOCustomer Customer { get; set; }
        public DTOCur Cur { get; set; }
        public List<Item> Items { get; set; }
        public List<DTOEdiversaException> Exceptions { get; set; }


        public DTOEdiversaSalesReport() : base()
        {
            Items = new List<Item>();
            Exceptions = new List<DTOEdiversaException>();
        }

        public DTOEdiversaSalesReport(Guid oGuid) : base(oGuid)
        {
            Items = new List<Item>();
            Exceptions = new List<DTOEdiversaException>();
        }

        public void AddException(DTOEdiversaException.Cods oCod, string sMsg, DTOEdiversaException.TagCods oTagCod = DTOEdiversaException.TagCods.NotSet, DTOBaseGuid oTag = null/* TODO Change to default(_) if this is not a reference type */)
        {
            var oException = DTOEdiversaException.Factory(oCod, oTag, sMsg);
            oException.TagCod = oTagCod;
            Exceptions.Add(oException);
        }

        public DTOAmt amount()
        {
            var eur = Items.Sum(x => x.Eur);
            return DTOAmt.Factory(eur);
        }

        public class Item
        {
            public DTOCustomer Customer { get; set; }
            public DTOProductSku Sku { get; set; }

            public DateTime Fch { get; set; }
            public string Dept { get; set; }
            public string Centro { get; set; }
            public int Qty { get; set; }
            public int QtyBack { get; set; }

            public decimal Eur { get; set; }
            public decimal Retail { get; set; }
        }
    }
}
