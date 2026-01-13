using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class FilterService
    {

        public static ProductModel.Filter? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Filters
                    .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new ProductModel.Filter(x.Guid)
                    {
                    }).FirstOrDefault();
            }
        }

        public static bool Update(ProductModel.Filter value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.Filter? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Filter();
                    db.Filters.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.Filters.Find(value.Guid);

                if (entity == null) throw new Exception("Filter not found");


                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Filters.Remove(db.Filters.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }


    }
    public class FiltersService
    {
        public static List<ProductModel.Filter> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return GetValues(db);
            }
        }

        public static List<ProductModel.Filter> GetValues(Entities.MaxiContext db)
        {
            return db.Filters
                .Include(x => x.FilterItems)
                .ThenInclude(x => x.FilterTargets)
                .AsNoTracking()
                .OrderBy(x => x.Ord)

                .Select(x => new ProductModel.Filter(x.Guid)
                {
                    Items = x.FilterItems.Select(y => y.Guid).ToList()
                }).ToList();
        }
    }
}
