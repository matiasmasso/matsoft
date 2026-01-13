using DocumentFormat.OpenXml.Presentation;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class ImmobleService
    {

        public static ImmobleModel? FromInventariItem(Guid inventariItem)
        {
            ImmobleModel? retval = null;
            Guid? immobleGuid = null;
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.InventariItems
                    .AsNoTracking()
                    .Where(x => x.Guid == inventariItem).FirstOrDefault();
                if (entity != null) immobleGuid = entity.Immoble;
            }
            if (immobleGuid != null)
                retval = Fetch((Guid)immobleGuid!);
            return retval;
        }

        public static ImmobleModel? Fetch(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.Immobles
                    .Where(x => x.Guid == guid)
                    .Select(x => new ImmobleModel(x.Guid)
                    {
                        Emp = (EmpModel.EmpIds)x.Emp,
                        Nom = x.Nom,
                        FchFrom = x.FchFrom,
                        FchTo = x.FchTo,
                        Cadastre = x.Cadastre,
                        Registre=x.Registre,
                        Titularitat = x.Titularitat,
                        Part = x.Part,
                        Obs = x.Obs,
                        Address = new AddressModel()
                        {
                            Text = x.Adr,
                            Zip = x.ZipGuid == null ? null : new ZipDTO((Guid)x.ZipGuid)
                        }
                    }).FirstOrDefault();

                if (retval?.Address?.Zip != null)
                {
                    retval.Address.Zip = db.VwZips
                        .Where(x => x.ZipGuid == retval.Address.Zip.Guid)
                        .Select(x => new ZipDTO((Guid)x.ZipGuid!)
                        {
                            ZipCod = x.ZipCod,
                            Location = new LocationDTO((Guid)x.LocationGuid!)
                            {
                                Nom = x.LocationNom,
                                Zona = new ZonaDTO((Guid)x.ZonaGuid!)
                                {
                                    Nom = x.ZonaNom,
                                    Country = new CountryDTO((Guid)x.CountryGuid!)
                                    {
                                        Nom = new LangTextDTO(x.CountryEsp, x.CountryCat, x.CountryEng, x.CountryPor)
                                    },
                                    Provincia = x.ProvinciaGuid == null ? null : new ProvinciaDTO((Guid)x.ProvinciaGuid!)
                                    {
                                        Nom = x.ProvinciaNom
                                    }
                                },
                            }
                        }).FirstOrDefault();
                }

                if (retval != null)
                {
                    retval.Docfiles = DocfileSrcsService.GetValues(db, retval);
                    retval.Inventari = Inventari(db, retval.Guid);
                }
                return retval;
            }

        }


        public static bool Update(ImmobleModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                var guid = value.Guid;
                Entities.Immoble? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Immoble();
                    db.Immobles.Add(entity);
                    entity.Guid = guid;
                }
                else
                    entity = db.Immobles.Find(guid);

                if (entity == null) throw new Exception("Immoble not found");

                entity.Emp = (int)value.Emp;
                entity.Nom = value.Nom;
                entity.FchFrom = value.FchFrom;
                entity.FchTo = value.FchTo;
                entity.Cadastre = value.Cadastre;
                entity.Registre = value.Registre;
                entity.Titularitat = value.Titularitat;
                entity.Part = value.Part;
                entity.Adr = value.Address?.Text;
                entity.ZipGuid = value.Address?.Zip?.Guid;
                entity.Obs = value.Obs;

                db.SaveChanges();
                return true;
            }
        }


        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                db.Immobles.Remove(db.Immobles.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }

        public static ImmobleModel.InventariItem? InventariItem(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.InventariItems
                    .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new ImmobleModel.InventariItem(x.Guid)
                    {
                        Nom = x.Nom,
                        Target = x.Immoble,
                        Obs = x.Obs
                    })
                    .FirstOrDefault();

                if (retval != null)
                {
                    retval.Docfiles = DocfilesService.FromSrc(db, guid);
                }
                return retval;
            }
        }

        public static bool UpdateItem(ImmobleModel.InventariItem value)
        {
            using (var db = new Entities.MaxiContext())
            {
                var guid = value.Guid;
                Entities.InventariItem? entity;
                if (value.IsNew)
                {
                    entity = new Entities.InventariItem();
                    db.InventariItems.Add(entity);
                    entity.Guid = guid;
                }
                else
                    entity = db.InventariItems.Find(guid);

                if (entity == null) throw new Exception("partida d'inventari not found");

                entity.Immoble = value.Target;
                entity.Nom = value.Nom ?? "";
                entity.Obs = value.Obs;

                db.SaveChanges();
                return true;
            }
        }

        public static List<ImmobleModel.InventariItem> Inventari(Entities.MaxiContext db, Guid target)
        {
            return db.InventariItems
                        .AsNoTracking()
                        .Where(x => x.Immoble == target)
                        .OrderBy(y => y.Nom).
                        Select(z => new ImmobleModel.InventariItem(z.Guid)
                        {
                            Target = z.Immoble,
                            Nom = z.Nom,
                            Obs = z.Obs
                        }).ToList();
        }
    }

    public class ImmoblesService
    {

        public static List<ImmobleModel> Fetch()
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = (from x in db.Immobles
                              select new ImmobleModel(x.Guid)
                              {
                                  Emp = (EmpModel.EmpIds)x.Emp,
                                  Nom = x.Nom,
                                  Cadastre = x.Cadastre,
                                  Registre = x.Registre,
                                  FchFrom = x.FchFrom,
                                  FchTo = x.FchTo,
                                  Obs = x.Obs
                              }).ToList();
                return retval;
            }

        }
    }
}
