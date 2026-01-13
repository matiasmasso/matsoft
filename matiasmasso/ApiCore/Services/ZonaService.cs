using DocumentFormat.OpenXml.Vml.Office;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class ZonaService
    {
        public static ZonaModel? Find(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Zonas
                        .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new ZonaModel(x.Guid)
                    {
                        Country = x.Country,
                        ISO = x.Iso,
                        Nom = x.Nom,
                        Provincia = x.Provincia,
                        Lang = x.Lang == null ? null : new LangDTO(x.Lang),
                        ExportCod = (ZonaModel.ExportCods)x.ExportCod,
                        Mod347 = x.Mod347 ?? false

                    }).FirstOrDefault();
            }
        }


        public static bool Update(ZonaModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                return Update(db, value);
            }
        }

        public static bool Update(Entities.MaxiContext db, ZonaModel value)
        {
            var guid = value.Guid;
            Entities.Zona? entity;
            if (value.IsNew)
            {
                entity = new Entities.Zona();
                db.Zonas.Add(entity);
                entity.Guid = guid;
            }
            else
                entity = db.Zonas.Find(guid);

            if (entity == null) throw new Exception("Zona not found");

            entity.Country = value.Country;
            entity.Iso = value.ISO;
            entity.Nom = value.Nom ?? "";
            entity.Provincia = value.Provincia;
            entity.Lang = value.Lang?.Id.ToString();
            entity.ExportCod = (short)value.ExportCod;
            entity.Mod347 = value.Mod347;

            db.SaveChanges();
            return true;
        }


        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Zonas.Find(guid);
                if (entity != null)
                {
                    db.Zonas.Remove(entity);
                    db.SaveChanges();
                }
            }
            return true;
        }


    }

    public class ZonasService
    {
        public static List<ZonaModel> GetValues()
        {
            using(var db = new Entities.MaxiContext())
            {
                return GetValues(db);
            }
        }
        public static List<ZonaModel> GetValues(Entities.MaxiContext db)
        {
            return db.Zonas
                        .AsNoTracking()
                            .OrderBy(x => x.Nom)
                            .Select(x => new ZonaModel(x.Guid)
                            {
                                Country = x.Country,
                                Nom = x.Nom,
                                Provincia = x.Provincia,
                                Lang = x.Lang == null ? null : new LangDTO(x.Lang),
                                ExportCod = (ZonaModel.ExportCods)x.ExportCod
                            }).ToList();
        }
    }

}
