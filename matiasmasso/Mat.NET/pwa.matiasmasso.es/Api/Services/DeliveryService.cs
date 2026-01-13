using Api.Entities;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class DeliveryService
    {
        public static DeliveryModel? Fetch(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.Albs
                    .Where(x => x.Guid.Equals(guid))
                    .Select(x => new DeliveryModel(x.Guid)
                    {
                        Id = x.Alb1,
                        Fch = x.Fch,
                        Cod = x.Cod,
                        Contact = x.CliGuid == null ? null : new GuidNom((Guid)x.CliGuid, x.Nom),
                        Invoice = x.FraGu == null ? null : new GuidNom((Guid)x.FraGuid!, string.Format("{0} del {1:dd/MM/yy}", x.FraGu.Fra1, x.FraGu.Fch)),
                        Transmission = x.TransmGu == null ? null : new GuidNom((Guid)x.TransmGuid!, string.Format("{0} del {1:dd/MM/yy HH:mm}", x.TransmGu.Transm1, x.TransmGu.Fch)),
                        Items = x.Arcs.OrderBy(y => y.Lin)
                            .Select(y => new DeliveryModel.Item(y.Guid)
                            {
                                Lin = y.Lin,
                                Qty = y.Qty,
                                Price = y.Eur,
                                Dto = (decimal?)y.Dto,
                                Sku = new GuidNom(y.ArtGuid, null)
                            }).ToList(),
                        UsrLog = new UsrLogModel
                        {
                            UsrCreated = x.UsrCreatedGuid == null ? null : new GuidNom((Guid)x.UsrCreatedGuid, x.UsrCreatedGu!.Nickname ?? x.UsrCreatedGu.Nom ?? x.UsrCreatedGu!.Adr ?? ""),
                            FchCreated = x.FchCreated,
                            UsrLastEdited = x.UsrLastEditedGuid == null ? null : new GuidNom((Guid)x.UsrLastEditedGuid, x.UsrLastEditedGu!.Nickname ?? x.UsrLastEditedGu.Nom ?? x.UsrLastEditedGu!.Adr ?? ""),
                            FchLastEdited = x.FchLastEdited
                        }

                    })
                    .FirstOrDefault();
                return retval;
            }
        }

        public static void Update(Entities.MaxiContext db, DeliveryModel value)
        {
            UpdateHeader(db, value);
            UpdateItems(db, value);
        }
        public static void UpdateHeader(Entities.MaxiContext db, DeliveryModel value)
        {
            var guid = value.Guid;
            Entities.Alb? entity;
            if (value.IsNew)
            {
                var year = ((DateTime)value.Fch!).Year;
                var lastId = db.Albs
                    .Where(x => x.Emp == value.Emp && ((DateTime)x.Fch!).Year == year)
                    .Max(x => x.Alb1);

                entity = new Entities.Alb();
                db.Albs.Add(entity);
                entity.Guid = guid;
                entity.Emp = value.Emp;
                entity.Yea = (short)year;
                entity.Alb1 = lastId + 1;

            }
            else
                entity = db.Albs.Find(guid);

            if (entity == null) throw new System.Exception("Delivery not found");

            entity.Fch = (DateTime?)value.Fch ?? DateTime.Today;
            entity.Cod = (short?)value.Cod ?? (int)PurchaseOrderModel.Cods.Customer;
            entity.CliGuid = value.Contact!.Guid;
            entity.Eur = value.Amt?.Value ?? 0;
            entity.UsrCreatedGuid = value.UsrLog?.UsrCreated?.Guid;
            entity.UsrLastEditedGuid = value.UsrLog?.UsrLastEdited?.Guid;
            entity.FchCreated = value.UsrLog?.FchCreated ?? DateTime.Now;
            entity.FchLastEdited = value.UsrLog?.FchLastEdited ?? DateTime.Now;
        }
        public static void UpdateItems(Entities.MaxiContext db, DeliveryModel value)
        {
            var lin = 1;
            foreach (var item in value.Items)
            {
                var entity = new Entities.Arc();
                entity.Guid = item.Guid;
                entity.Lin = lin;
                entity.Cod = (int)DeliveryModel.Item.Cods.Sale;
                entity.AlbGuid = value.Guid;
                entity.Qty = item.Qty ?? 0;
                entity.ArtGuid = (Guid)item.Sku!.Guid;
                entity.PncGuid = item.PncGuid;
                entity.PdcGuid = item.PdcGuid;
                entity.MgzGuid = item.MgzGuid;
                entity.Eur = item.Price ?? 0;
                entity.Pts = item.Price ?? 0;
                entity.Cur = "EUR";
                db.Arcs.Add(entity);
                lin += 1;
            }
        }


    }
    public class DeliveriesService
    {

        public static DeliveryModel.List FromUser(UserModel user)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new DeliveryModel.List();
                if (user.isCustomer())
                {
                    retval.Items = db.Albs
                        .Join(db.EmailClis, a => a.CliGuid, e => e.ContactGuid, (a, e) => new { a, e })
                        .Where(x => x.e.EmailGuid == user.Guid)
                        .Select(x => new DeliveryModel(x.a.Guid)
                        {
                            Id = x.a.Alb1,
                            Fch = x.a.Fch,
                            Cod = x.a.Cod,
                            Contact = x.a.CliGuid == null ? null : new GuidNom((Guid)x.a.CliGuid, x.a.Nom),
                            Amt = new Amt(x.a.Pts, x.a.Cur, x.a.Eur),
                        })
                        .OrderByDescending(x => x.Fch.Year)
                        .ThenByDescending(x => x.Id)
                        .ToList();
                }
                return retval;
            }
        }


        public static DeliveryModel.List Fetch(Guid contactGuid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new DeliveryModel.List();

                retval.Items = db.Albs
                .Where(x => x.CliGuid == contactGuid)
                .Select(x => new DeliveryModel(x.Guid)
                {
                    Id = x.Alb1,
                    Fch = x.Fch,
                    Cod = x.Cod,
                    Contact = x.CliGuid == null ? null : new GuidNom((Guid)x.CliGuid, x.Nom),
                    Amt = new Amt(x.Pts, x.Cur, x.Eur)
                })
                .OrderByDescending(x => x.Fch.Year)
                .ThenByDescending(x => x.Id)
                .ToList();
                return retval;
            }
        }

        public static DeliveryModel.List FromYear(int emp, int year)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new DeliveryModel.List();

                retval.Years = db.Albs
                    .Where(x => x.Emp == emp)
                    .GroupBy(x => x.Yea)
                    .Select(x => (int)x.Key)
                    .OrderByDescending(x => x)
                    .ToList();

                if (year == 0) year = retval.Years.FirstOrDefault();


                retval.Items = db.Albs
                    .Where(x => x.Emp == emp && x.Yea == year)
                    .Select(x => new DeliveryModel(x.Guid)
                    {
                        Id = x.Alb1,
                        Fch = x.Fch,
                        Cod = x.Cod,
                        Contact = new GuidNom((Guid)x.CliGuid!, x.Nom),
                        Amt = new Amt(x.Pts, x.Cur, x.Eur)
                    })
                    .OrderByDescending(x => x.Id)
                    .ToList();
                return retval;
            }
        }

    }
}
