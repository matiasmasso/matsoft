using DocumentFormat.OpenXml.Presentation;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class ShoppingBasketService
    {
        public static ShoppingBasketModel? Find(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return Find(db, guid);
            }
        }
        public static ShoppingBasketModel? FromTpvOrder(string tpvOrder)
        {
            using (var db = new Entities.MaxiContext())
            {
                return FromTpvOrder(db, tpvOrder);
            }
        }
        public static ShoppingBasketModel? FromTpvOrder(Entities.MaxiContext db, string? tpvOrder)
        {
            ShoppingBasketModel? retval = null;
            if (!string.IsNullOrEmpty(tpvOrder))
            {
                var guid = db.TpvLogs
                        .AsNoTracking()
                 .Where(x => x.DsOrder == tpvOrder)
                 .Select(x => x.Request)
                 .FirstOrDefault();

                if (guid != null) retval = Find(db, (Guid)guid);
            }
            return retval;
        }

        public static ShoppingBasketModel? Find(Entities.MaxiContext db, Guid guid)
        {
            var retval = db.ShoppingBaskets
                        .AsNoTracking()
                .Where(x => x.Guid == guid)
                .Select(x => new ShoppingBasketModel(x.Guid)
                {
                    Fch = x.Fch,
                    Lang = new LangDTO(x.Lang),
                    User = new UserModel((Guid)x.UserAccount!) { EmailAddress = x.UserAccountNavigation.Adr},
                    MarketPlace = new MarketPlaceModel((Guid)x.MarketPlace!),
                    OrderNum = x.OrderNum,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    LastName2 = x.LastName2,
                    Country = x.Country,
                    ZipCod = x.ZipCod,
                    Location = x.Location,
                    Address = x.Address,
                    Tel = x.Tel,
                    TrpObs = x.TrpObs,
                    Ports = x.Ports,
                    PurchaseOrder = x.PurchaseOrder == null ? null : new PurchaseOrderModel((Guid)x.PurchaseOrder)
                    
                }).FirstOrDefault();

            if (retval != null)
            {
                retval.Items = db.ShoppingBasketItems
                        .AsNoTracking()
                    .Where(x => x.ParentNavigation.Guid == guid)
                    .OrderBy(x => x.Lin)
                    .Select(x => new ShoppingBasketModel.Item
                    {
                        Qty = (int)x.Qty!,
                        Price = x.Price,
                        Sku = x.Sku
                    }).ToList();
            }
            return retval;
        }

        public static bool Update(ShoppingBasketModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                UpdateHeader(db, value);
                UpdateItems(db, value);
                db.SaveChanges();
                return true;
            }
        }

        public static void UpdateHeader(Entities.MaxiContext db, ShoppingBasketModel value)
        {
            Entities.ShoppingBasket? entity = db.ShoppingBaskets.Find(value.Guid);
            if (entity == null)
            {
                entity = new Entities.ShoppingBasket();
                db.ShoppingBaskets.Add(entity);
                entity.Guid = value.Guid;
            }

            entity.Fch = (DateTime)value.Fch!;
            entity.Lang = value.Lang!.Tag();
            entity.UserAccount = value.User!.Guid;
            entity.MarketPlace = value.MarketPlace!.Guid;
            entity.OrderNum = value.OrderNum;
            entity.FirstName = value.FirstName;
            entity.LastName = value.LastName;
            entity.LastName2 = value.LastName2;
            entity.Country = value.Country;
            entity.ZipCod = value.ZipCod;
            entity.Location = value.Location;
            entity.Address = value.Address;
            entity.Tel = value.Tel;
            entity.TrpObs = value.TrpObs;
            entity.Ports = value.Ports;
            entity.Amount = value.Items!.Sum(x => x.Amount());
            entity.PurchaseOrder = value.PurchaseOrder == null ? null : value.PurchaseOrder.Guid;

        }

        public static void UpdateItems(Entities.MaxiContext db, ShoppingBasketModel value)
        {
            db.ShoppingBasketItems.RemoveRange(db.ShoppingBasketItems.Where(x => x.Parent == value.Guid));

            var lin = 1;
            foreach (var item in value.Items!)
            {
                var entity = new Entities.ShoppingBasketItem();
                entity.Parent = value.Guid;
                entity.Lin = lin;
                entity.Qty = item.Qty;
                entity.Sku = (Guid)item.Sku!;
                entity.Price = item.Price ?? 0;
                db.ShoppingBasketItems.Add(entity);
                lin += 1;
            }
        }


        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                db.ShoppingBasketItems.RemoveRange(db.ShoppingBasketItems.Where(x => x.Parent == guid));
                db.ShoppingBaskets.Remove(db.ShoppingBaskets.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;
        }

        public static ZipDTO? FindOrCreateZip(ShoppingBasketModel basket)
        {
            ZipDTO? retval = null;
            if (basket.Country != null && basket.Location != null && basket.ZipCod != null)
            {
                var country = (Guid)basket.Country;
                var cache = CacheService.Request(CacheDTO.Table.TableIds.Country, CacheDTO.Table.TableIds.Zona, CacheDTO.Table.TableIds.Provincia, CacheDTO.Table.TableIds.Location, CacheDTO.Table.TableIds.Zip);
                var oCountry = cache.Country(country);
                var zip = cache.ZipFromLocation(oCountry, basket.Location, basket.ZipCod);
                retval = zip == null ? null : new ZipDTO(zip.Guid)
                {
                    ZipCod = basket.ZipCod,
                    //Location = cache.Location(zip.Location)
                    //{
                    //    Zona = new ZonaDTO()
                    //}
                };
            }
            return retval;
        }
    }

    public class ShoppingBasketsService
    {
        public static List<ShoppingBasketModel> GetValues(MarketPlaceModel marketplace)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.ShoppingBaskets
                        .AsNoTracking()
                    .Where(x => x.MarketPlace == marketplace.Guid)
                    .OrderByDescending(x => x.Fch)
                    .Select(x => new ShoppingBasketModel(x.Guid)
                    {
                        Fch = x.Fch,
                        User = new UserModel((Guid)x.UserAccount!) ,
                        MarketPlace = new MarketPlaceModel((Guid)x.MarketPlace!),
                        OrderNum = x.OrderNum,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        LastName2 = x.LastName2,
                        Country = x.Country,
                        ZipCod = x.ZipCod,
                        Location = x.Location,
                        Amount = x.Amount,
                        Ports = x.Ports,
                        PurchaseOrder = x.PurchaseOrder == null ? null : new PurchaseOrderModel((Guid)x.PurchaseOrder)
                    }).ToList();
                return retval;
            }
        }
    }

}
