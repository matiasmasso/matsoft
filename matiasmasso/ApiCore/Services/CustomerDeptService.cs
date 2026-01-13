using DocumentFormat.OpenXml.Presentation;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class CustomerDeptService
    {

        public static CustomerDeptModel? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return  db.CustomerDepts
                    .Include(x=>x.CustomerDeptItems)
                    .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new CustomerDeptModel(x.Guid)
                    {
                        Customer = x.Customer,
                        Cod = x.Cod,
                        Nom = x.Nom,
                        Products = x.CustomerDeptItems.Select(x => x.Product).ToList()
                    }).FirstOrDefault();
            }
        }

        public static bool Update(CustomerDeptModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.CustomerDept? entity;
                if (value.IsNew)
                {
                    entity = new Entities.CustomerDept();
                    db.CustomerDepts.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.CustomerDepts.Find(value.Guid);

                if (entity == null) throw new Exception("CustomerDept not found");

                entity.Customer = value.Customer;
                entity.Cod = value.Cod!;
                entity.Nom = value.Nom;
                entity.CustomerDeptItems.Clear();

                var itemsToDelete = db.CustomerDeptItems.Where(x => x.Parent == value.Guid).ToList();
                db.CustomerDeptItems.RemoveRange(itemsToDelete);

                foreach (Guid item in value.Products)
                {
                    entity.CustomerDeptItems.Add(new Entities.CustomerDeptItem
                    {
                        Product = item
                    });
                }

                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var items = db.CustomerDeptItems.Where(x=>x.Parent == guid).ToList();
                if(items.Count>0) db.CustomerDeptItems.RemoveRange(items);

                var dept = db.CustomerDepts.FirstOrDefault(x => x.Guid.Equals(guid));
                if(dept != null) db.CustomerDepts.Remove(dept);
                db.SaveChanges();
            }
            return true;

        }


    }
    public class CustomerDeptsService
    {
        public static List<CustomerDeptModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.CustomerDepts
                .Include(x => x.CustomerDeptItems)
                    .AsNoTracking()
                    .Select(x => new CustomerDeptModel(x.Guid)
                    {
                        Customer = x.Customer,
                        Cod = x.Cod,
                        Nom = x.Nom,
                        Products = x.CustomerDeptItems.Select(x => x.Product).ToList()
                    }).ToList();
            }
        }
    }
}
