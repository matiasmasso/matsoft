using Api.Entities;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
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
                        .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new PurchaseOrderModel(x.Guid)
                    {
                        Contact = new GuidNom(x.CliGuid, x.Cli.FullNom),
                        Id = x.Pdc1,
                        Fch = x.Fch,
                        Concept = x.Pdd,
                        Amt = new Amt(x.PdcPts, x.Cur, x.Eur),
                        Hash = x.Hash,
                        UsrLog = new UsrLogModel
                        {
                            UsrCreated = x.UsrCreatedGuid == null ? null : new GuidNom((Guid)x.UsrCreatedGuid, x.UsrCreated == null ? "" : x.UsrCreated.Nickname ?? x.UsrCreated.Nom ?? x.UsrCreated.Adr ?? ""),
                            FchCreated = x.FchCreated,
                            UsrLastEdited = x.UsrLastEditedGuid == null ? null : new GuidNom((Guid)x.UsrLastEditedGuid, x.UsrLastEdited == null ? "" : x.UsrLastEdited.Nickname ?? x.UsrLastEdited.Nom ?? x.UsrLastEdited.Adr ?? ""),
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

        public static void Update(PurchaseOrderModel value)
        {
            using (var db = new Entities.MaxiContext())
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
            var fch = value.Fch!;

            Entities.Pdc? entity;
            if (value.IsNew)
            {
                entity = new Entities.Pdc();
                SetNextId(db, value, ref entity);
                db.Pdcs.Add(entity);
                entity.Guid = value.Guid;
            }
            else
            {
                entity = db.Pdcs.Find(value.Guid);
                if (entity == null) throw new System.Exception("Pdc not found");
                if (((DateOnly)fch).Year != entity.Yea)
                    SetNextId(db, value, ref entity);
            }

            if (entity == null) throw new System.Exception("Purchase Order not found");

            entity.Fch = value.Fch ?? DateOnly.FromDateTime( DateTime.Today);
            entity.Cod = (short?)value.Cod ?? (int)PurchaseOrderModel.Cods.Customer;
            entity.Src = (short?)value.Src ?? (int)PurchaseOrderModel.Sources.no_Especificado;
            entity.CliGuid = value.Contact!.Guid;
            entity.NoRep = value.Norep;
            entity.Pdd = value.Concept ?? "";
            entity.Eur = value.Sum();
            entity.PdcPts = value.Sum();
            entity.UsrCreatedGuid = value.UsrLog?.UsrCreated?.Guid;
            entity.UsrLastEditedGuid = value.UsrLog?.UsrLastEdited?.Guid;
            entity.FchCreated = value.UsrLog?.FchCreated;
            entity.FchLastEdited = value.UsrLog?.FchLastEdited;
        }
        public static void UpdateItems(Entities.MaxiContext db, PurchaseOrderModel value)
        {
            var lin = 1;
            foreach (var item in value.Items)
            {
                var entity = new Entities.Pnc();
                entity.Guid = item.Guid;
                entity.Lin = lin;
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

        public static void SetNextId(MaxiContext db, PurchaseOrderModel value, ref Entities.Pdc entity)
        {
            var emp = (int?)value.Emp ?? (int)EmpModel.EmpIds.MatiasMasso;
            var fch = value.Fch!;
            var yea = (short)((DateOnly)fch).Year;

            var lastId = db.Pdcs
                        .AsNoTracking()
                .Where(x => x.Emp == emp & x.Yea == yea)
                .Max(x => x.Pdc1);

            value.Id = lastId + 1;
            entity.Emp = emp;
            entity.Yea = yea;
            entity.Pdc1 = (int)value.Id;
        }

        public static PurchaseOrderModel.Resources Resources(Guid userGuid)
        {
            var retval = new PurchaseOrderModel.Resources();
            var user = new UserModel(userGuid);
            using (var db = new MaxiContext())
            {
                retval.AvailableDestinations = Destinations(db, userGuid);
                retval.CustomerPortfolio = CustomerPortfolioService.FromUser(db, user);
                retval.CustomerDtosOnRrpp = CustomerDtosOnRrppService.GetValues(db, user);
                //passar a user : retval.CustomPricelist = PriceListCustomerService.CurrentFromCustomer(db, customerGuid);
            }
            return retval;
        }

        public static PurchaseOrderModel.Resources CustomerResources(Guid customerGuid)
        {
            var retval = new PurchaseOrderModel.Resources();
            using (var db = new MaxiContext())
            {
                retval.CustomerPortfolio = CustomerPortfolioService.FromCustomer(db, customerGuid);
                retval.CustomerDtosOnRrpp = CustomerDtosOnRrppService.FromCustomer(db, customerGuid);
                retval.CustomPricelist = PriceListCustomerService.CurrentFromCustomer(db, customerGuid);
            }
            return retval;
        }

        public static List<Guid> Destinations(MaxiContext db, Guid userGuid)
        {
            return db.EmailClis
            .AsNoTracking()
            .Include(x => x.Contact)
            .Where(x => x.EmailGuid == userGuid && !x.Contact.Obsoleto)
            .Select(x => x.ContactGuid)
            .ToList();
        }


    }
    public class PurchaseOrdersService
    {

        public static PurchaseOrderListDTO FromContact(Guid contact)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new PurchaseOrderListDTO();
                retval.Items = db.Pdcs
                        .AsNoTracking()
                .Where(x => x.CliGuid == contact)
                    .Select(x => new PurchaseOrderListDTO.Item
                    {
                        Guid = x.Guid,
                        Contact = new GuidNom(x.CliGuid, x.Cli.FullNom),
                        Id = x.Pdc1,
                        Fch = x.Fch,
                        Concept = x.Pdd,
                        Amt = new Amt(x.PdcPts, x.Cur, x.Eur),
                        Hash = x.Hash,
                        User = x.UsrCreatedGuid == null ? null : x.UsrCreated == null ? null : x.UsrCreated.Nickname ?? x.UsrCreated.Nom ?? x.UsrCreated.Adr ?? ""
                    })
                    .ToList();
                return retval;
            }

        }
        public static List<PurchaseOrderModel> GetValues(int emp, int year)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Pdcs
                    .Include(x => x.UsrCreated)
                    .AsNoTracking()
                    .Where(x => x.Emp == emp && x.Yea == year)
                    .Select(x => new PurchaseOrderModel(x.Guid)
                    {
                        Contact = new GuidNom(x.CliGuid, x.Cli.FullNom),
                        Id = x.Pdc1,
                        Fch = x.Fch,
                        Concept = x.Pdd,
                        Amt = new Amt(x.PdcPts, x.Cur, x.Eur),
                        Hash = x.Hash,
                        FchCreated = x.FchCreated,
                        UsrCreated = x.UsrCreated == null ? null : new GuidNom((Guid)x.UsrCreatedGuid!, x.UsrCreated.Nickname ?? x.UsrCreated.Nom ?? x.UsrCreated.Adr ?? "")
                    })
                    .ToList();
            }

        }

        public static void Update(List<PurchaseOrderModel> values)
        {
            using (var db = new Entities.MaxiContext())
            {
                Update(db, values);
            }

        }
        public static void Update(Entities.MaxiContext db, List<PurchaseOrderModel> values)
        {
            foreach (var po in values)
            {
                PurchaseOrderService.UpdateHeader(db, po);
                PurchaseOrderService.UpdateItems(db, po);
                db.SaveChanges();
            }
        }


    }
}
