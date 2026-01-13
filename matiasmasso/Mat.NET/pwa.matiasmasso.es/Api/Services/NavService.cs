using DTO;

namespace Api.Services
{
    public class NavService
    {
        //langtext.src = 39

        public static NavDTO.Model? Get(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Navs
                    .Join(
                   db.VwLangTexts,
                   nav => nav.Guid,
                   langtext => langtext.Guid,
                   (nav, langtext) => new { nav, langtext }
                   )
                    .Where(x => x.nav.Guid == guid)
                    .FirstOrDefault();

                NavDTO.Model? retval = null;
                if (entity != null)
                {
                    retval = new NavDTO.Model(entity.nav.Guid)
                    {
                        Parent = entity.nav.Parent,
                        Ord = entity.nav.Ord,
                        Mode = entity.nav.Mode,
                        Action = entity.nav.Action,
                        IcoSmall = entity.nav.IcoSmall,
                        IcoBig = entity.nav.IcoBig,
                    };

                    retval.Nom.Load(entity.langtext.Esp, entity.langtext.Cat, entity.langtext.Eng, entity.langtext.Por);

                    retval.Rols = db.NavRols
                        .Where(x => x.Nav.Equals(retval.Guid))
                        .Select(y => y.Rol)
                        .ToList();
                    retval.Emps = db.NavEmps
                        .Where(x => x.Nav.Equals(retval.Guid))
                        .Select(y => y.Emp)
                        .ToList();
                }

                return retval;
            }

        }


        public static bool Update(NavDTO.Model value)
        {
            using (var db = new Entities.MaxiContext())
            {
                var guid = value.Guid;
                Entities.Nav? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Nav();
                    db.Navs.Add(entity);
                    entity.Guid = guid;
                }
                else
                    entity = db.Navs.Find(guid);

                if (entity == null) throw new Exception("Menu item not found");

                entity.Parent = value.Parent;
                entity.Ord = value.Ord;
                entity.Action = value.Action;
                entity.Mode = (int)value.Mode;
                entity.IcoSmall = value.IcoSmall;
                entity.IcoBig = value.IcoBig;

                LangTextService.Update(db, value.Nom);

                var rolsToDelete = db.NavRols.Where(x => x.Nav == guid).ToList();
                if (rolsToDelete.Count > 0)
                {
                    db.NavRols.RemoveRange(rolsToDelete);
                }

                if (value.Rols.Count > 0)
                {
                    foreach (var rol in value.Rols)
                    {
                        var r = new Entities.NavRol();
                        r.Nav = value.Guid;
                        r.Rol = (int)rol;
                        db.NavRols.Add(r);
                    }
                }

                var empsToDelete = db.NavEmps.Where(x => x.Nav == guid).ToList();
                if (empsToDelete.Count > 0)
                {
                    db.NavEmps.RemoveRange(empsToDelete);
                }

                if (value.Emps.Count > 0)
                {
                    foreach (var emp in value.Emps)
                    {
                        var e = new Entities.NavEmp();
                        e.Nav = value.Guid;
                        e.Emp = (int)emp;
                        db.NavEmps.Add(e);
                    }
                }

                db.SaveChanges();
                return true;
            }
        }


        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                db.NavEmps.RemoveRange(db.NavEmps.Where(x => x.Nav == guid));
                db.NavRols.RemoveRange(db.NavRols.Where(x => x.Nav == guid));
                db.Navs.Remove(db.Navs.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }


    }

    public class NavsService
    {
        public static NavDTO Full()
        {
            using (var db = new Entities.MaxiContext())
            {
                var entities = (from item in db.Navs
                                join langtext in db.VwLangTexts on item.Guid equals langtext.Guid
                                orderby item.Parent, item.Ord
                                select new
                                {
                                    item.Guid,
                                    item.Parent,
                                    item.Ord,
                                    item.IcoSmall,
                                    item.IcoBig,
                                    langtext.Esp,
                                    langtext.Cat,
                                    langtext.Eng,
                                    langtext.Por
                                }).ToList();

                var retval = new NavDTO();
                foreach (var entity in entities)
                {
                    var item = new NavDTO.Model()
                    {
                        Guid = entity!.Guid,
                        Parent = entity.Parent,
                        Ord = entity.Ord,
                        IcoSmall = entity.IcoSmall,
                        IcoBig = entity.IcoBig
                    };
                    item.Nom.Load(entity.Esp, entity.Cat, entity.Eng, entity.Por);
                    retval.Items.Add(item);
                }
                return retval;
            }
        }


        public static NavDTO Custom(UserModel? user, LangDTO lang, int emp)
        {
            using (var db = new Entities.MaxiContext())
            {
                return Custom(db, user, lang,emp);
            }
        }

        public static NavDTO Custom(Entities.MaxiContext db, UserModel? user, LangDTO lang, int emp)
        {
                var rol = user == null ? (int)UserModel.Rols.notSet : user.Rol ?? (int)UserModel.Rols.notSet;
                var entities = db.VwNavs
                    .Where(x => x.Emp == emp && (x.Rol == (int)rol)) // || x.Rol == (int)UserModel.Rols.notSet))
                    .OrderBy(y => y.Parent)
                    .ThenBy(z => z.Ord)
                    .ToList();
                var retval = new NavDTO();
                foreach (var entity in entities)
                {
                    var item = new NavDTO.Model()
                    {
                        Guid = entity!.Guid,
                        Parent = entity.Parent,
                        Ord = entity.Ord,
                        Mode = entity.Mode,
                        Action = entity.Action,
                        IcoSmall = entity.IcoSmall,
                        IcoBig = entity.IcoBig
                    };
                    item.Nom.Load(entity.Esp, entity.Cat, entity.Eng, entity.Por);
                    retval.Items.Add(item);
                }
                return retval;
        }

        public async static Task<bool> UpdateSortOrder(NavDTO nav)
        {
            using (var db = new Entities.MaxiContext())
            {
                foreach (var value in nav.Items)
                {
                    var entity = db.Navs.Find(value.Guid);
                    if (entity == null) throw new Exception("Menu item not found");
                    entity.Ord = value.Ord;
                }

                db.SaveChanges();
                return true;
            }
        }


    }
}
