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
                var retval = db.ShoppingBaskets
                    .Where(x => x.Guid == guid)
                    .Select(x => new ShoppingBasketModel(x.Guid)
                    {
                        Fch = x.Fch,
                        Lang = new LangDTO(x.Lang),
                        User = new UserModel((Guid)x.UserAccount!),
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
                        TrpObs = x.TrpObs
                    }).FirstOrDefault();

                if (retval != null)
                {
                    retval.Items = db.ShoppingBasketItems
                        .Where(x => x.ParentNavigation.Guid == guid)
                        .OrderBy(x => x.Lin)
                        .Select(x => new ShoppingBasketModel.Item
                        {
                            Qty = (int)x.Qty!,
                            Price = x.Price,
                            Sku = new GuidNom { Guid = x.Sku }
                        }).ToList();
                }
                return retval;
            }
        }

        public static bool Update(ShoppingBasketModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                UpdateHeader(db, value);
                db.SaveChanges();
                return true;
            }
        }

        public static void UpdateHeader(Entities.MaxiContext db, ShoppingBasketModel value)
        {
            Entities.ShoppingBasket? entity;
            if (value.IsNew)
            {
                entity = new Entities.ShoppingBasket();
                db.ShoppingBaskets.Add(entity);
                entity.Guid = value.Guid;
            }
            else
                entity = db.ShoppingBaskets.Find(value.Guid);

            if (entity == null) throw new Exception("ShoppingBasket not found");

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
            entity.Amount = value.Items!.Sum(x => x.Amount());

        }

        public static void UpdateItems(Entities.MaxiContext db, ShoppingBasketModel value)
        {
            var lin = 1;
            foreach (var item in value.Items!)
            {
                var entity = new Entities.ShoppingBasketItem();
                entity.Parent = value.Guid;
                entity.Lin = lin;
                entity.Qty = item.Qty;
                entity.Sku = item.Sku!.Guid;
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


    }
    public class ShoppingBasketsService
    {
        public static List<ShoppingBasketModel> All(MarketPlaceModel marketplace)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.ShoppingBaskets
                    .Where(x=>x.MarketPlace == marketplace.Guid)
                    .Select(x => new ShoppingBasketModel(x.Guid)
                    {
                        Fch = x.Fch,
                        User = new UserModel((Guid)x.UserAccount!),
                        MarketPlace = new MarketPlaceModel((Guid)x.MarketPlace!),
                        OrderNum = x.OrderNum,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        LastName2 = x.LastName2,
                        Country = x.Country,
                        ZipCod = x.ZipCod,
                        Location = x.Location,
                        Amount = x.Amount
                    }).ToList();
                return retval;
            }
        }
    }
}
