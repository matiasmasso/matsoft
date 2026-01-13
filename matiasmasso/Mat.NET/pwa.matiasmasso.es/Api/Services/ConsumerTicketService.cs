using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using DTO;

namespace Api.Services
{
    public class ConsumerTicketService
    {
        public static bool Update(DTO.ConsumerTicketModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                return Update(db, value);
            }
        }
        public static bool Update(Entities.MaxiContext db, ConsumerTicketModel value)
        {
            var guid = value.Guid;
            Entities.ConsumerTicket? entity;
            if (value.IsNew)
            {
                var lastId = db.ConsumerTickets
                    .Where(x => x.Emp == value.Emp && ((DateTime)x.Fch!).Year == ((DateTime)value.Fch!).Year)
                    .Max(x => x.Id);

                entity = new Entities.ConsumerTicket();
                db.ConsumerTickets.Add(entity);
                entity.Guid = guid;
                entity.Emp = value.Emp;
                entity.UsrCreated = (Guid)value.UsrLog!.UsrCreated!.Guid!;
                entity.Id = lastId + 1;
            }
            else
                entity = db.ConsumerTickets.Find(guid);

            if (entity == null) throw new System.Exception("ConsumerTicket not found");

            entity.Fch = value.Fch;
            entity.Lang = value.Lang.ToString();
            entity.MarketPlace = value.MarketPlace!.Guid;
            entity.OrderNum = value.OrderNum;
            entity.Nom = value.Nom;
            entity.Cognom1 = value.Cognom1;
            entity.Cognom2 = value.Cognom2;
            entity.Address = value.Address;
            entity.ConsumerZip = value.ConsumerZip;
            entity.ConsumerLocation = value.ConsumerLocation;
            entity.ConsumerProvincia = value.ConsumerProvincia;
            entity.Zip = value.Zip;
            entity.BuyerNom = value.BuyerNom;
            entity.BuyerEmail = value.BuyerEmail;
            entity.Tel = value.Tel;
            entity.Portes = value.Portes;
            entity.Goods = value.Goods;
            entity.Comision = value.Comision;
            entity.PurchaseOrder = value.PurchaseOrder?.Guid;

            db.SaveChanges();
            return true;
        }

    }
    public class ConsumerTicketsService
    {
        public static List<ConsumerTicketModel> GetValues(Guid? marketplace)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.ConsumerTickets
                    .Where(x => x.MarketPlace == marketplace)
                    .Select(x => new ConsumerTicketModel(x.Guid)
                    {
                        Id = x.Id,
                        Fch = x.Fch,
                        Nom = x.Nom,
                        Cognom1 = x.Cognom1,
                        Cognom2 = x.Cognom2,
                        PurchaseOrder = x.PurchaseOrder == null ? null : new GuidNom((Guid)x.PurchaseOrder, ""),
                        Delivery = x.Delivery == null ? null : new GuidNom((Guid)x.Delivery, "")

                    }).ToList();
            }
        }
    }
}
