using Api.Entities;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class ConsumerTicketService
    {
        public static ConsumerTicketModel? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.ConsumerTickets
                    .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new ConsumerTicketModel(x.Guid)
                    {
                        Id = x.Id,
                        Fch = x.Fch,
                        Lang = new LangDTO(x.Lang),
                        MarketPlace = x.MarketPlace == null ? null : new GuidNom((Guid)x.MarketPlace!, null),
                        Nom = x.Nom,
                        Cognom1 = x.Cognom1,
                        Cognom2 = x.Cognom2,
                        Address = x.Address,
                        ConsumerZip = x.ConsumerZip,
                        ConsumerLocation = x.ConsumerLocation,
                        ConsumerProvincia = x.ConsumerProvincia,
                        Zip = x.Zip,
                        BuyerNom = x.BuyerNom,
                        BuyerEmail = x.BuyerEmail,
                        Tel = x.Tel,
                        Portes = x.Portes,
                        Comision = x.Comision,
                        PurchaseOrder = x.PurchaseOrder == null ? null : new GuidNom((Guid)x.PurchaseOrder, ""),
                        Delivery = x.Delivery == null ? null : new GuidNom((Guid)x.Delivery, ""),
                        OrderNum = x.OrderNum,
                        UsrLog = new UsrLogModel
                        {
                            FchCreated = x.FchCreated,
                            UsrCreated = new GuidNom(x.UsrCreated, null)
                        }
                    }).FirstOrDefault();
            }
        }

        public static ConsumerTicketModel? FromOrderNum(string orderNum, Guid? marketplace = null)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.ConsumerTickets
                    .AsNoTracking()
                    .Include(x=>x.UsrCreatedNavigation)
                    .Where(x => x.OrderNum == orderNum && x.MarketPlace == (marketplace ?? x.MarketPlace))
                    .Select(x => new ConsumerTicketModel(x.Guid)
                    {
                        Id = x.Id,
                        Fch = x.Fch,
                        Lang = new LangDTO(x.Lang),
                        MarketPlace = x.MarketPlace == null ? null : new GuidNom((Guid)x.MarketPlace!, null),
                        Nom = x.Nom,
                        Cognom1 = x.Cognom1,
                        Cognom2 = x.Cognom2,
                        Address = x.Address,
                        ConsumerZip = x.ConsumerZip,
                        ConsumerLocation = x.ConsumerLocation,
                        ConsumerProvincia = x.ConsumerProvincia,
                        Zip = x.Zip,
                        BuyerNom = x.BuyerNom,
                        BuyerEmail = x.BuyerEmail,
                        Tel = x.Tel,
                        Portes = x.Portes,
                        Comision = x.Comision,
                        PurchaseOrder = x.PurchaseOrder == null ? null : new GuidNom((Guid)x.PurchaseOrder, ""),
                        Delivery = x.Delivery == null ? null : new GuidNom((Guid)x.Delivery, ""),
                        OrderNum = x.OrderNum,
                        UsrLog = new UsrLogModel
                        {
                            FchCreated = x.FchCreated,
                            UsrCreated = new GuidNom(x.UsrCreated, x.UsrCreatedNavigation.Nickname ?? x.UsrCreatedNavigation.Adr)
                        }
                    }).FirstOrDefault();
                return retval;
            }
        }
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
            var fch = value.Fch!;

            Entities.ConsumerTicket? entity;
            if (value.IsNew)
            {
                entity = new Entities.ConsumerTicket();
                value.Id = SetNextId(db, value, ref entity);
                db.ConsumerTickets.Add(entity);
                entity.Guid = value.Guid;
                entity.UsrCreated = (Guid)value.UsrLog!.UsrCreated!.Guid!;
            }
            else
            {
                entity = db.ConsumerTickets.Find(value.Guid);
                if (entity == null) throw new System.Exception("Consumer ticket not found");
                if (((DateOnly)fch).Year != ((DateOnly)entity.Fch!).Year)
                    value.Id = SetNextId(db, value, ref entity);
            }

            entity.Fch = value.Fch;
            entity.Lang = value.Lang!.ToString();
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
            entity.Delivery = value.Delivery?.Guid;
            entity.Cca = value.Cca?.Guid;
            return true;
        }

        public static int SetNextId(MaxiContext db, ConsumerTicketModel value, ref Entities.ConsumerTicket entity)
        {
            var emp = (int?)value.Emp ?? (int)EmpModel.EmpIds.MatiasMasso;
            var fch = value.Fch!;
            var yea = (short)((DateOnly)fch).Year;

            var lastId = db.ConsumerTickets
                .AsNoTracking()
                .Where(x => x.Emp == emp & ((DateOnly)x.Fch!).Year == yea)
                .Max(x => x.Id);

            entity.Emp = emp;
            entity.Fch = fch;
            entity.Id = lastId + 1;
            return entity.Id;
        }

    }
    public class ConsumerTicketsService
    {
        public static List<ConsumerTicketModel> FromUser(Guid? marketplace, Guid? user)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.ConsumerTickets
                    .AsNoTracking()
                    .Where(x => x.MarketPlace == marketplace && x.UsrCreated == user)
                    .Select(x => new ConsumerTicketModel(x.Guid)
                    {
                        Id = x.Id,
                        Fch = x.Fch,
                        OrderNum = x.OrderNum,
                        Goods = x.Goods,
                        PurchaseOrder = x.PurchaseOrder == null ? null : new GuidNom((Guid)x.PurchaseOrder, ""),
                        Delivery = x.Delivery == null ? null : new GuidNom((Guid)x.Delivery, "")

                    }).ToList();
            }
        }
        public static List<ConsumerTicketModel> GetValues(Guid? marketplace)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.ConsumerTickets
                    .AsNoTracking()
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
        public static List<ConsumerTicketModel> FromOrderNumbers(Guid? marketplace, List<string> ordernumbers)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.ConsumerTickets
                    .AsNoTracking()
                    .Include(x=>x.UsrCreatedNavigation)
                    .Where(x => x.MarketPlace == marketplace && ordernumbers.Any(y=>x.OrderNum == y))
                    .Select(x => new ConsumerTicketModel(x.Guid)
                    {
                        Id = x.Id,
                        Fch = x.Fch,
                        Nom = x.Nom,
                        Cognom1 = x.Cognom1,
                        Cognom2 = x.Cognom2,
                        OrderNum = x.OrderNum,
                        PurchaseOrder = x.PurchaseOrder == null ? null : new GuidNom((Guid)x.PurchaseOrder, ""),
                        Delivery = x.Delivery == null ? null : new GuidNom((Guid)x.Delivery, ""),
                        UsrLog = new UsrLogModel
                        {
                            FchCreated = x.FchCreated,
                            UsrCreated = new GuidNom(x.UsrCreated, UserModel.DisplayNom(x.UsrCreatedNavigation.Adr, x.UsrCreatedNavigation.Nickname))
                        }
                    }).ToList();
            }
        }
    }
}
