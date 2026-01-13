using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class CredencialService
    {

        public static CredencialModel? Find(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Credencials
                    .Where(x => x.Guid == guid)
                    .Select(x => new CredencialModel(x.Guid)
                              {
                                  Referencia = x.Referencia ?? "",
                                  Url = x.Url,
                                  Usuari = x.Usuari,
                                  Password = x.Password,
                                  Obs = x.Obs,
                                  UsrLog = new UsrLogModel()
                                  {
                                      FchCreated = x.FchCreated,
                                      FchLastEdited = x.FchLastEdited,
                                      UsrCreated = new GuidNom() { Guid = x.UsrCreated },
                                      UsrLastEdited = new GuidNom() { Guid = x.UsrLastEdited }
                                  },
                                  Rols = x.Rols.Select(x => (UserModel.Rols)x.Rol).ToList(),
                                  Owners = x.Owners.Select(y => new GuidNom()
                                  {
                                      Guid = y.Guid,
                                      Nom = UserModel.DisplayNom(y.Address, y.Nickname)
                                  }).ToList()

                              }).FirstOrDefault();
            }
        }

        public static bool Update(CredencialModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.Credencial? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Credencial();
                    db.Credencials.Add(entity);
                    entity.Guid = value.Guid;
                    entity.UsrCreated = value.UsrLog.UsrLastEdited!.Guid;
                    entity.FchCreated = value.UsrLog.FchLastEdited ?? DateTime.Now;
                }
                else
                    entity = db.Credencials.Find(value.Guid);

                if (entity == null) throw new Exception("Credencial not found");

                entity.Referencia = value.Referencia;
                entity.Usuari = value.Usuari ?? "";
                entity.Password = value.Password ?? "";
                entity.Obs = value.Obs;
                entity.UsrLastEdited = value.UsrLog.UsrLastEdited!.Guid;
                entity.FchLastEdited = value.UsrLog.FchLastEdited ?? DateTime.Now;

                entity.Rols = new List<Entities.UsrRol>();
                foreach(UserModel.Rols rol in value.Rols)
                {
                    entity.Rols.Add(new Entities.UsrRol{Rol = (int)rol, Nom = rol.ToString() });
                }

                // Save changes in database
                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Credencials.Remove(db.Credencials.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }


        private static List<MenuItemModel> MenuItems()
        {
            var retval = new List<MenuItemModel>();
            retval.Add(Navigate());
            retval.Add(CopyUser());
            retval.Add(CopyPwd());
            return retval;

        }

        private static MenuItemModel Navigate() => new MenuItemModel()
        {
            Caption = new LangTextModel("Navegar", "Navegar", "Navigate"),
            Action = "navigate",
            Ico = "fa-solid fa-users"
        };

        private static MenuItemModel CopyUser() => new MenuItemModel()
        {
            Caption = new LangTextModel("Copiar usuario", "Copiar usuari", "Copy user"),
            Action = "copyUser",
            Ico = "fa-solid fa-users"
        };

        private static MenuItemModel CopyPwd() => new MenuItemModel()
        {
            Caption = new LangTextModel("Copiar contraseña", "Copiar clau", "Copy password"),
            Action = "copyPwd",
            Ico = "fa-solid fa-users"
        };


    }
    public class CredencialsService
    {
        public static List<CredencialModel> All(UserModel user)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = (from x in db.Credencials
                                orderby x.Referencia
                                where x.Owners.Any(x => x.Guid.Equals(user.Guid))
                                select new CredencialModel(x.Guid)
                                {
                                    Referencia = x.Referencia ?? "",
                                    Url = x.Url,
                                    Usuari = x.Usuari,
                                    Password = x.Password
                                }).ToList();
                return retval;
            }
        }
    }
}
