using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class ExplicitDiscountService
    {

        public static ExplicitDiscountModel? GetValue(Guid customer, Guid product)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.CliDtos
                    .AsNoTracking()
                    .Where(x => x.Customer == customer && x.Brand == product)
                    .Select(x => new ExplicitDiscountModel
                    {
                        Customer = x.Customer, Product = x.Brand, Dto= x.Dto
                    }).FirstOrDefault();
            }
        }

        public static bool Update(ExplicitDiscountModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.CliDto? entity = db.CliDtos.FirstOrDefault(x => x.Customer == value.Customer && x.Brand == value.Product);
                if (entity == null)
                {
                    entity = new Entities.CliDto();
                    db.CliDtos.Add(entity);
                    entity.Customer = value.Customer;
                    entity.Brand = value.Product;
                }

                if (entity == null) throw new Exception("ExplicitDiscount CliDto not found");

                entity.Dto = value.Dto;

                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid customer, Guid product)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.CliDtos.Remove(db.CliDtos.Single(x => x.Customer == customer && x.Brand == product));
                db.SaveChanges();
            }
            return true;
        }


    }
    public class ExplicitDiscountsService
    {
        public static List<ExplicitDiscountModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.CliDtos
                    .AsNoTracking()
                    .Select(x => new ExplicitDiscountModel
                    {
                        Customer = x.Customer,
                        Product = x.Brand,
                        Dto = x.Dto
                    }).ToList();
            }
        }
    }
}

