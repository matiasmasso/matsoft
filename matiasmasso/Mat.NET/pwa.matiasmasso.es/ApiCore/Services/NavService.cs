using Api.Entities;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Vml.Office;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class NavService
    {
        //langtext.src = 39

        public static bool Update(NavDTO.MenuItem value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.Nav? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Nav();
                    db.Navs.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                     entity = db.Navs
                    .Include(x=>x.Emps)
                    .Include(x=>x.NavRols)
                    .Include(x=>x.Claims)
                    .FirstOrDefault(x => x.Guid == value.Guid);

                if (entity == null) throw new System.Exception("Menu item not found");

                entity.Parent = value.Parent;
                entity.Ord = value.Ord;
                entity.Action = value.Action;
                entity.Mode = (int)value.Mode;
                entity.IcoSmall = value.IcoSmall;
                entity.IcoBig = value.IcoBig;
                entity.Emps = value.Emps.Select(x => db.Emps.First(y=>y.Emp1 ==(int)x)).ToList();
                entity.Claims = value.Claims.Select(x => db.Claims.First(y=>y.Guid == x )).ToList();
                entity.NavRols = value.Rols.Select(x => new Entities.NavRol { Rol = (int)x }).ToList();

                LangTextService.Update(db, value.Nom);

                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                db.LangTexts.RemoveRange(db.LangTexts.Where(x => x.Guid == guid));
                db.Navs.Remove(db.Navs.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }


    }

    public class NavsService
    {
        public static List<NavDTO.MenuItem> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.Navs
                    .Include(x => x.Claims)
                    .Include(x => x.NavRols)
                    .AsNoTracking()
                    .OrderBy(x => x.Ord)
                    .Select(x => new NavDTO.MenuItem(x.Guid)
                    {
                        Parent = x.Parent,
                        Ord = x.Ord,
                        Mode = (NavDTO.MenuItem.Modes)x.Mode,
                        Action = x.Action,
                        Claims = x.Claims.Select(y => y.Guid).ToList(),
                        Rols = x.NavRols.Select(y => (UserModel.Rols)y.Rol).ToList(),
                        Emps = x.Emps.Select(y => (EmpModel.EmpIds)y.Emp1).ToList()
                    })
                    .ToList();

                return retval;
            }
        }


        public async static Task<bool> UpdateSortOrder(List<NavDTO.MenuItem> items)
        {
            using (var db = new Entities.MaxiContext())
            {
                foreach (var value in items)
                {
                    var entity = db.Navs.Find(value.Guid);
                    if (entity == null) throw new System.Exception("Menu item not found");
                    entity.Ord = value.Ord;
                }

                db.SaveChanges();
                return true;
            }
        }


    }
}
