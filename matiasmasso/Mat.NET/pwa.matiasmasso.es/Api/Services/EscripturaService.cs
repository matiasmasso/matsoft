using DocumentFormat.OpenXml.Office.CustomUI;
using DTO;
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
                    .Where(x => x.Guid == guid )
                    .Select(x => new EscripturaModel(guid) {
                        Emp = x.NotariNavigation == null ? (int)EmpModel.EmpIds.MatiasMasso : (int)x.NotariNavigation.Emp,
                        Nom = x.Nom,
                        Notari = x.NotariNavigation == null ? null : new GuidNom(x.NotariNavigation.Guid, x.NotariNavigation.RaoSocial),
                        FchFrom = x.FchFrom,
                        FchTo = x.FchTo,
                        NumProtocol = x.NumProtocol,
                        RegistreMercantil = x.RegistreMercantilNavigation == null ? null : new GuidNom((Guid)x.RegistreMercantil!, x.RegistreMercantilNavigation.RaoSocial),
                        Tomo = x.Tomo,
                        Folio = x.Folio,
                        Hoja = x.Hoja,
                        Inscripcio = x.Inscripcio,
                        DocFile = x.HashNavigation == null ? null : new DocFileModel {
                            Hash = x.Hash,
                            Mime = x.HashNavigation.Mime,
                            ThumbnailMime = x.HashNavigation.ThumbnailMime,
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

        public static async Task<bool> UpdateAsync(EscripturaModel value, IFormFile? file, IFormFile? thumbnail)
        {
            using (var db = new Entities.MaxiContext())
            {
                if (file != null) await DocfileService.UpdateAsync(db, value.DocFile, file, thumbnail);

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

                entity.Nom = value.Nom ?? "";
                entity.Notari = value.Notari?.Guid;
                entity.FchFrom= value.FchFrom ?? default(DateTime);
                entity.FchTo = value.FchTo;
                entity.NumProtocol = value.NumProtocol ?? 0;
                entity.RegistreMercantil = value.RegistreMercantil?.Guid;
                entity.Tomo = value.Tomo ?? 0;
                entity.Folio = value.Folio ?? 0 ;
                entity.Hoja = value.Hoja ?? "";
                entity.Inscripcio = value.Inscripcio ?? 0;
                entity.Hash = value.DocFile?.Hash;

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
                    .Where(x => x.Guid == guid && x.HashNavigation != null)
                    .Select(x => new Media(x.HashNavigation.Mime, x.HashNavigation.Doc))
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
                    .OrderByDescending(x => x.FchFrom)
                    .Select(x => new EscripturaModel(x.Guid)
                    {
                        Emp = x.NotariNavigation == null ? (int)EmpModel.EmpIds.MatiasMasso : (int)x.NotariNavigation.Emp,
                        Nom = x.Nom,
                        Notari = x.NotariNavigation == null ? null : new GuidNom(x.NotariNavigation.Guid, x.NotariNavigation.RaoSocial),
                        FchFrom = x.FchFrom,
                        FchTo = x.FchTo,
                        NumProtocol = x.NumProtocol,
                        Tomo = x.Tomo,
                        Folio = x.Folio,
                        Hoja = x.Hoja,
                        Inscripcio = x.Inscripcio,
                        DocFile = x.Hash == null ? null : new DocFileModel(x.Hash)
                    })
                    .ToList();
            }
        }
    }
}
