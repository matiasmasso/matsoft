using Api.Entities;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;

namespace Api.Services
{
    public class PurchaseOrderService
    {

        public static PurchaseOrderModel? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.Pdcs
                    .Where(x => x.Guid == guid)
                    .Select(x => new PurchaseOrderModel(x.Guid)
                    {
                        Contact = new GuidNom(x.CliGuid, x.CliGu.FullNom),
                        Id = x.Pdc1,
                        Fch = x.Fch,
                        Concept = x.Pdd,
                        Amt = new Amt(x.PdcPts, x.Cur, x.Eur),
                        Hash = x.Hash,
                        UsrLog = new UsrLogModel
                        {
                            UsrCreated = x.UsrCreatedGuid == null ? null : new GuidNom((Guid)x.UsrCreatedGuid, x.UsrCreatedGu!.Nickname ?? x.UsrCreatedGu.Nom ?? x.UsrCreatedGu!.Adr ?? ""),
                            FchCreated = x.FchCreated,
                            UsrLastEdited = x.UsrLastEditedGuid == null ? null : new GuidNom((Guid)x.UsrLastEditedGuid, x.UsrLastEditedGu!.Nickname ?? x.UsrLastEditedGu.Nom ?? x.UsrLastEditedGu!.Adr ?? ""),
                            FchLastEdited = x.FchLastEdited
                        }
                    })
                    .FirstOrDefault();

                if (retval != null)
                {
                    retval.Items = db.Pncs
                        .Where(x => x.PdcGuid == retval.Guid)
                        .OrderBy(x => x.Lin)
                        .Select(x => new PurchaseOrderModel.Item(x.Guid)
                        {
                            Qty = x.Qty,
                            Price = x.Eur,
                            Dto = x.Dto,
                            Sku = x.ArtGuid
                        })
                        .ToList();
                }
                return retval;
            }



        }

        public static void Update( PurchaseOrderModel value)
        {
            using(var db = new Entities.MaxiContext())
            {
                Update(db, value);
            }
        }
        public static void Update(Entities.MaxiContext db, PurchaseOrderModel value)
        {
            UpdateHeader(db, value);
            UpdateItems(db, value);
        }
        public static void UpdateHeader(Entities.MaxiContext db, PurchaseOrderModel value)
        {
            var guid = value.Guid;
            Entities.Pdc? entity;
            if (value.IsNew)
            {
                var year = ((DateTime)value.Fch!).Year;
                var lastId = db.Pdcs
                    .Where(x => x.Emp == value.Emp && ((DateTime)x.Fch!).Year == year)
                    .Max(x => x.Pdc1);

                entity = new Entities.Pdc();
                db.Pdcs.Add(entity);
                entity.Guid = guid;
                entity.Emp = value.Emp ?? (int)EmpModel.EmpIds.MatiasMasso;
                entity.Yea = (short)year;
                entity.Pdc1 = lastId + 1;
                entity.NoRep = value.Norep;
                entity.Eur = value.Sum();
                entity.PdcPts = value.Sum();

            }
            else
                entity = db.Pdcs.Find(guid);

            if (entity == null) throw new System.Exception("Purchase Order not found");

            entity.Fch = (DateTime?)value.Fch ?? DateTime.Today;
            entity.Cod = (short?)value.Cod ?? (int)PurchaseOrderModel.Cods.Customer;
            entity.Src = (short?)value.Src ?? (int)PurchaseOrderModel.Sources.no_Especificado; 
            entity.CliGuid = value.Contact!.Guid;
            entity.Pdd = value.Concept ?? "";
            entity.Eur = value.Amt?.Value ?? 0;
            entity.UsrCreatedGuid = value.UsrLog?.UsrCreated?.Guid;
            entity.UsrLastEditedGuid = value.UsrLog?.UsrLastEdited?.Guid;
            entity.FchCreated = value.UsrLog?.FchCreated;
            entity.FchLastEdited = value.UsrLog?.FchLastEdited;
        }
        public static void UpdateItems(Entities.MaxiContext db, PurchaseOrderModel value)
        {
            var lin = 1;
            foreach(var item in value.Items)
            {
                var entity = new Entities.Pnc();
                entity.Guid = item.Guid;
                entity.Lin= lin;
                entity.PdcGuid = value.Guid;
                entity.Qty = item.Qty;
                entity.Pn2 = item.Pending;
                entity.Carrec = item.ChargeCod == PurchaseOrderModel.Item.ChargeCods.chargeable;
                entity.ArtGuid = (Guid)item.Sku!;
                entity.Eur = item.Price ?? 0;
                entity.Pts = item.Price ?? 0;
                db.Pncs.Add(entity);
                lin += 1;
            }
        }
    }
    public class PurchaseOrderListService
    {

        public static PurchaseOrderListDTO FromUser(UserModel user)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new PurchaseOrderListDTO();
                if (user.isCustomer())
                {
                    retval.Items = db.Pdcs
                        .Join(db.EmailClis, p => p.CliGuid, e => e.ContactGuid, (p, e) => new { p, e })
                        .Where(x => x.e.EmailGuid == user.Guid)
                        .Select(x => new PurchaseOrderListDTO.Item
                        {
                            Guid = x.p.Guid,
                            Contact = new GuidNom(x.p.CliGuid, x.p.CliGu.FullNom),
                            Id = x.p.Pdc1,
                            Fch = x.p.Fch,
                            Concept = x.p.Pdd,
                            Amt = new Amt(x.p.PdcPts, x.p.Cur, x.p.Eur),
                            Hash = x.p.Hash,
                            User = x.p.UsrCreatedGuid == null ? null : x.p.UsrCreatedGu!.Nickname ?? x.p.UsrCreatedGu.Nom ?? x.p.UsrCreatedGu!.Adr ?? "",
                        })
                        .ToList();
                }
                return retval;
            }
        }

        public static PurchaseOrderListDTO FromContact(Guid contact)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new PurchaseOrderListDTO();
                retval.Items = db.Pdcs
                .Where(x => x.CliGuid == contact)
                    .Select(x => new PurchaseOrderListDTO.Item
                    {
                        Guid = x.Guid,
                        Contact = new GuidNom(x.CliGuid, x.CliGu.FullNom),
                        Id = x.Pdc1,
                        Fch = x.Fch,
                        Concept = x.Pdd,
                        Amt = new Amt(x.PdcPts, x.Cur, x.Eur),
                        Hash = x.Hash,
                        User = x.UsrCreatedGuid == null ? null : x.UsrCreatedGu!.Nickname ?? x.UsrCreatedGu.Nom ?? x.UsrCreatedGu!.Adr ?? ""
                    })
                    .ToList();
                return retval;
            }

        }
        public static PurchaseOrderListDTO FromYear(int emp, int year)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new PurchaseOrderListDTO();

                retval.Years = db.Pdcs
                    .Where(x => x.Emp == emp && x.Cod == (int)PurchaseOrderModel.Cods.Customer)
                    .GroupBy(x => x.Yea)
                    .Select(x => (int)x.Key)
                    .OrderByDescending(x => x)
                    .ToList();

                if (year == 0) year = retval.Years.FirstOrDefault();

                retval.Items = db.Pdcs.Where(x => x.Emp == emp & x.Cod == (int)PurchaseOrderModel.Cods.Customer & x.Yea == year)
                    .Select(x => new PurchaseOrderListDTO.Item
                    {
                        Guid = x.Guid,
                        Contact = new GuidNom(x.CliGuid, x.CliGu.FullNom),
                        Id = x.Pdc1,
                        Fch = x.Fch,
                        Concept = x.Pdd,
                        Amt = new Amt(x.PdcPts, x.Cur, x.Eur),
                        Hash = x.Hash,
                        User = x.UsrCreatedGuid == null ? null : x.UsrCreatedGu!.Nickname ?? x.UsrCreatedGu.Nom ?? x.UsrCreatedGu!.Adr ?? ""
                    })
                    .ToList();
                return retval;
            }

        }

    }
}
