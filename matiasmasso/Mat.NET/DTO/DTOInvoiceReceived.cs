using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOInvoiceReceived : DTOBaseGuid
    {
        public DTOGuidNom Proveidor { get; set; }
        public DTOEan ProveidorEan { get; set; }
        public string DocNum { get; set; }
        public DateTime Fch { get; set; }
        public List<Item> Items { get; set; }
        public string ShipmentId { get; set; }

        public DTOGuidNom Importacio { get; set; }
        public DTOCur Cur { get; set; }
        public DTOAmt TaxBase { get; set; }
        public DTOAmt NetTotal { get; set; }
        public List<DTOException> Exceptions { get; set; }

        public enum ExCods
        {
            NotSet,
            ProveidorNotFound
        }

        public DTOInvoiceReceived() : base()
        {
            //Cur = DTOCur.Eur(); json serialize matxaca Eur amb tag
            Items = new List<Item>();
            Exceptions = new List<DTOException>();
        }

        public DTOInvoiceReceived(Guid oGuid) : base(oGuid)
        {
            Cur = DTOCur.Eur();
            Items = new List<Item>();
            Exceptions = new List<DTOException>();
        }


        public Item AddItem(Guid guid)
        {
            Item retval = new Item(guid);
            retval.Lin = Items.Count + 1;
            Items.Add(retval);
            return retval;
        }

        public DTOException AddException(string msg = "", ExCods cod = ExCods.NotSet, DTOBaseGuid tag = null)
        {
            DTOException retval = new DTOException();
            retval.Msg = msg;
            retval.Cod = (int)cod;
            retval.Tag = tag;
            Exceptions.Add(retval);
            return retval;
        }

        public DTOAmt SumaDeImports()
        {
            DTOAmt retval = DTOAmt.Factory();
            foreach (Item item in Items)
            {
                retval.Add(DTOAmt.import(item.Qty, DTOAmt.Factory(item.Price), item.DtoOrDefault()));
            }
            return retval;
        }

        public Boolean HasExceptions()
        {
            int itemsExsCount = Items.SelectMany(x => x.Exceptions).Count();
            return (Exceptions.Count + itemsExsCount) > 0;
        }

        public void ClearExceptions()
        {
            Exceptions = new List<DTOException>();
            foreach (Item item in Items)
                item.Exceptions = new List<DTOException>();
        }

        public List<String> SkuEans()
        {
            List<String> retval = Items.
                Where(a => a.SkuEan != null).
                Select(b => b.SkuEan.Value).Distinct().ToList();
            return retval;
        }

        public List<String> PurchaseOrderIds()
        {
            List<String> retval = Items.
                Where(a => !String.IsNullOrEmpty(a.PurchaseOrderId) && int.TryParse(a.PurchaseOrderId, out _)).
                Select(b => b.PurchaseOrderId).ToList();

            retval = retval.Distinct().ToList();
            return retval;
        }

        public class Item : DTOBaseGuid
        {
            public int Lin { get; set; }
            public DTOGuidNom PurchaseOrder { get; set; }
            public DTOPurchaseOrderItem PurchaseOrderItem { get; set; }
            public string PurchaseOrderId { get; set; }
            public string OrderConfirmation { get; set; }
            public string DeliveryNote { get; set; }
            public DTOGuidNom Sku { get; set; }
            public DTOEan SkuEan { get; set; }
            public string SkuRef { get; set; }
            public string SkuNom { get; set; }
            public int Qty { get; set; }
            public int QtyConfirmed { get; set; }
            public decimal Price { get; set; }
            public decimal Dto { get; set; }
            public DTOAmt Amount { get; set; }
            public List<DTOException> Exceptions { get; set; }

            public enum ExCods
            {
                NotSet,
                SkuNotFound,
                MissingPurchaseOrder,
                MissingItemInPurchaseOrder,
                PurchaseOrderNotFound,
                PriceGap,
                QtyGap
            }


            public Item() : base()
            {
                Exceptions = new List<DTOException>();
            }

            public Item(Guid oGuid) : base(oGuid)
            {
                Exceptions = new List<DTOException>();
            }

            public DTOException AddException(string msg = "", ExCods cod = ExCods.NotSet, DTOBaseGuid tag = null)
            {
                DTOException retval = new DTOException();
                retval.Msg = msg;
                retval.Cod = (int)cod;
                retval.Tag = tag;
                Exceptions.Add(retval);
                return retval;
            }

            public decimal DtoOrDefault()
            {
                decimal retval = Dto;
                if (Dto == 0 && Amount != null)
                {
                    if (Amount.Val != 0)
                        retval = 100 * (1 - Amount.Val / (Qty * Price));
                    else if (Amount.Eur != 0)
                        retval = 100 * (1 - Amount.Eur / (Qty * Price));
                    retval = Math.Round(retval, 2);
                }
                return retval;
            }

        }

    }
}
