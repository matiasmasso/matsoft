using Azure;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Presentation;
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
                    .AsNoTracking()
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
                        Owners = x.Owners.Select(y => new UserModel(y.Guid)
                        {
                            EmailAddress = y.Address,
                            Nickname = y.Nickname
                        }).ToList()

                    }).FirstOrDefault();
            }
        }

        public static bool Update(CredencialModel value, UserModel user)
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
                    entity.Owners.Add(db.Emails.Find(user.Guid)!);
                }
                else
                {
                    entity = db.Credencials.Include(x => x.Rols).FirstOrDefault(x => x.Guid == value.Guid);
                    //entity = db.Credencials.Find(value.Guid);
                    if (entity == null) throw new Exception("Credencial not found");
                    entity!.Rols.Clear(); // Clear existing roles to prevent duplicates on many-to-many relationships
                }


                entity.Referencia = value.Referencia;
                entity.Url = value.Url ?? "";
                entity.Usuari = value.Usuari ?? "";
                entity.Password = value.Password ?? "";
                entity.Obs = value.Obs;

                if (value.UsrLog?.UsrLastEdited != null)
                {
                    entity.UsrLastEdited = value.UsrLog.UsrLastEdited.Guid;
                }
                entity.FchLastEdited = value.UsrLog?.FchLastEdited ?? DateTime.Now;

                foreach (var rol in value.Rols)
                {
                    var rolEntity = db.UsrRols.Find((int)rol);
                    entity.Rols.Add(rolEntity!);
                }



                //entity.Rols.Clear(); // = new List<Entities.UsrRol>();
                //foreach (UserModel.Rols rol in value.Rols)
                //{
                //    entity.Rols.Add(new Entities.UsrRol { Rol = (int)rol, Nom = rol.ToString() });
                //}

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
            Url = new LangTextModel("navigate"),
            Ico = "fa-solid fa-users"
        };

        private static MenuItemModel CopyUser() => new MenuItemModel()
        {
            Caption = new LangTextModel("Copiar usuario", "Copiar usuari", "Copy user"),
            Url = new LangTextModel("copyUser"),
            Ico = "fa-solid fa-users"
        };

        private static MenuItemModel CopyPwd() => new MenuItemModel()
        {
            Caption = new LangTextModel("Copiar contraseña", "Copiar clau", "Copy password"),
            Url = new LangTextModel("copyPwd"),
            Ico = "fa-solid fa-users"
        };


    }
    public class CredencialsService
    {
        public static List<CredencialModel> All(Guid user)
        {
            using (var db = new Entities.MaxiContext())
            {


                return db.Credencials
                    .AsNoTracking()
                    .Where(x => x.Owners.Any(x => x.Guid.Equals(user)))
                    .OrderBy(x => x.Referencia)
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
                        Owners = x.Owners.Select(y => new UserModel(y.Guid)
                        {
                            EmailAddress = y.Address,
                            Nickname = y.Nickname
                        }).ToList()

                    }).ToList();

            }
        }
    }
}
