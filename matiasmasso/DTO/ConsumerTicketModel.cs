using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ConsumerTicketModel:BaseGuid, IModel
    {
        public int Id { get; set; }
        public EmpModel.EmpIds Emp { get; set; }
        public LangDTO? Lang { get; set; }
        public DateOnly? Fch { get; set; }
        public GuidNom? MarketPlace { get; set; }
        public string? OrderNum { get; set; }
        public string? Nom { get; set; }
        public string? Cognom1 { get; set; }
        public string? Cognom2 { get; set; }
        public string? Address { get; set; }
        public string? ConsumerZip { get; set; }
        public string? ConsumerLocation { get; set; }
        public string? ConsumerProvincia { get; set; }
        public Guid? Zip { get; set; }
        public string? BuyerNom { get; set; }
        public string? BuyerEmail { get; set; }
        public string? Tel { get; set; }
        public decimal? Portes { get; set; }
        public decimal? Goods { get; set; }
        public decimal? Comision { get; set; }
        public GuidNom? Cca { get; set; }
        public GuidNom? PurchaseOrder { get; set; }
        public GuidNom? Delivery { get; set; }
        public GuidNom? Invoice { get; set; }
        public List<Item> Items { get; set; } = new();

        public UsrLogModel? UsrLog { get; set; }

        public ConsumerTicketModel() : base() { }
        public ConsumerTicketModel(Guid guid) : base(guid) { }

        public static ConsumerTicketModel Factory(UserModel user, LangDTO lang, MarketPlaceModel marketplace, string? orderNum)
        {
            return new ConsumerTicketModel
            {
                Emp = user.Emp,
                Lang = lang,
                MarketPlace = new GuidNom(marketplace.Guid, marketplace.Nom),
                Fch = DateOnly.FromDateTime(DateTime.Now),
                OrderNum = orderNum,
                UsrLog = UsrLogModel.Factory(user)
            };
        }

        public static ConsumerTicketModel Factory(EmpModel.EmpIds empId, Guid userGuid, LangDTO lang, MarketPlaceModel marketplace, string? orderNum)
        {
            return new ConsumerTicketModel
            {
                Emp = empId ,
                Lang = lang,
                MarketPlace = new GuidNom(marketplace.Guid, marketplace.Nom),
                Fch = DateOnly.FromDateTime(DateTime.Now),
                OrderNum = orderNum,
                UsrLog = UsrLogModel.Factory(new UserModel(userGuid))
            };
        }

        public decimal Sum() => Items.Sum(x => x.Amount());

        public string? FullNom()
        {
            string? retval = string.Empty;
            var hasNom = !string.IsNullOrEmpty(Nom);
            var hasCognom1 = !string.IsNullOrEmpty(Cognom1);
            var hasCognom2 = !string.IsNullOrEmpty(Cognom2);
            if (hasNom & hasCognom1 & hasCognom2)
                retval = string.Format("{0} {1}, {2}", Cognom1, Cognom2, Nom);
            else if (hasNom & hasCognom1)
                retval = string.Format("{0}, {1}", Cognom1, Nom);
            else if (hasNom & hasCognom2)
                retval = string.Format("{0}, {1}", Cognom2, Nom);
            else if (hasCognom1 & hasCognom2)
                retval = string.Format("{0} {1}", Cognom1, Cognom2);
            else if (hasNom)
                retval = string.Format("{0}", Nom);
            else if (hasCognom1)
                retval = string.Format("{0}", Cognom1);
            else if (hasCognom2)
                retval = string.Format("{0}", Cognom2);
            return retval;
        }

        public string PropertyPageUrl() => Globals.PageUrl("consumerTicket", Guid.ToString());
        public decimal Cash() => Items.Sum(x => x.Amount());


        public class Item
        {
            public int Qty { get; set; }
            public Guid Sku { get; set; }
            public decimal Price { get; set; }
            public decimal Dto { get; set; }
            public Guid PncGuid { get; set; }
            public decimal Amt() => Qty * Price;
            public decimal Amount() => Math.Round(Qty * (Price) * (100 - (Dto)) / 100, 2);

        }

        public DeliveryModel Deliver(MgzModel mgz)
        {
            var retval = new DeliveryModel
            {
                Fch = DateTime.Now,
                Cod = PurchaseOrderModel.Cods.Customer,
                Contact = new GuidNom(CustomerModel.Wellknown(CustomerModel.Wellknowns.consumidor)!.Guid),
                Amt = new Amt((decimal)Items.Sum(x => x.Amount())),
                UsrLog = UsrLog,
                Items = Items.Select(x => new DeliveryModel.Item
                {
                    Qty = x.Qty,
                    Sku = x.Sku,
                    Price = x.Price,
                    PncGuid = x.PncGuid,
                    PdcGuid = PurchaseOrder?.Guid,
                    MgzGuid = mgz.Guid
                }).ToList()
            };
            return retval;
        }

    }
}
