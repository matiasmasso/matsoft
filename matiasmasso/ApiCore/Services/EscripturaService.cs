using DocumentFormat.OpenXml.Office.CustomUI;
using DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace Api.Services
{
    public class EscripturaService
    {
        public static EscripturaModel? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Escripturas
                    .AsNoTracking()
                    .Include(x => x.HashNavigation)
                    .Where(x => x.Guid == guid )
                    .Select(x => new EscripturaModel(guid) {
                        Emp = (EmpModel.EmpIds)x.Emp,
                        Nom = x.Nom,
                        Notari = x.Notari,  //.NotariNavigation == null ? null : new GuidNom(x.NotariNavigation.Guid, x.NotariNavigation.RaoSocial),
                        FchFrom = x.FchFrom,
                        FchTo = x.FchTo,
                        NumProtocol = x.NumProtocol,
                        RegistreMercantil = x.RegistreMercantil, //Navigation == null ? null : new GuidNom((Guid)x.RegistreMercantil!, x.RegistreMercantilNavigation.RaoSocial),
                        Tomo = x.Tomo,
                        Folio = x.Folio,
                        Hoja = x.Hoja,
                        Inscripcio = x.Inscripcio,
                        Docfile = x.HashNavigation == null ? null : new DocfileModel {
                            Hash = x.Hash,
                            Document = new DTO.Media((Media.MimeCods)x.HashNavigation.Mime, null),
                            Thumbnail = new DTO.Media((Media.MimeCods)x.HashNavigation.ThumbnailMime, null), 
                            Pags = x.HashNavigation.Pags,
                            Size = x.HashNavigation.Size,
                            Width = x.HashNavigation.Width,
                            Height = x.HashNavigation.Height,
                            FchCreated = x.FchCreated
                        }
                    })
                    .FirstOrDefault();
            }
        }

        public static bool Update(EscripturaModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                if (value.Docfile?.Hash != null) DocfileService.Update(db, value.Docfile);

                var guid = value.Guid;
                Entities.Escriptura? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Escriptura();
                    db.Escripturas.Add(entity);
                    entity.Guid = guid;
                }
                else
                    entity = db.Escripturas.Find(guid);

                if (entity == null) throw new System.Exception("escriptura not found");

                entity.Emp = (int)value.Emp;
                entity.Nom = value.Nom ?? "";
                entity.Notari = value.Notari;
                entity.FchFrom= value.FchFrom ?? default(DateOnly);
                entity.FchTo = value.FchTo;
                entity.NumProtocol = value.NumProtocol;
                entity.RegistreMercantil = value.RegistreMercantil;
                entity.Tomo = value.Tomo;
                entity.Folio = value.Folio;
                entity.Hoja = value.Hoja;
                entity.Inscripcio = value.Inscripcio;
                entity.Hash = value.Docfile?.Hash;

                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Escripturas.Find(guid);
                if (entity != null)
                {
                    db.Escripturas.Remove(entity);
                    db.SaveChanges();
                }
            }
            return true;
        }


        public static Media? Document(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Escripturas
                    .AsNoTracking()
                    .Where(x => x.Guid == guid && x.HashNavigation != null)
                    .Select(x => new Media((Media.MimeCods)x.HashNavigation!.Mime, x.HashNavigation.Doc))
                    .FirstOrDefault();
            }
        }
    }


    public class EscripturasService
    {
        public static List<EscripturaModel> Fetch()
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Escripturas
                    .AsNoTracking()
                    .OrderByDescending(x => x.FchFrom)
                    .Select(x => new EscripturaModel(x.Guid)
                    {
                        Emp = (EmpModel.EmpIds)x.Emp,
                        Nom = x.Nom,
                        Notari = x.Notari,
                        FchFrom = x.FchFrom,
                        FchTo = x.FchTo,
                        NumProtocol = x.NumProtocol,
                        Tomo = x.Tomo,
                        Folio = x.Folio,
                        Hoja = x.Hoja,
                        Inscripcio = x.Inscripcio,
                        Docfile = x.Hash == null ? null : new DocfileModel(x.Hash!)
                    })
                    .ToList();
            }
        }
    }
}
