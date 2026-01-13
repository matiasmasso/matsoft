using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class ContactDocService
    {

        public static ContactDocModel? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.CliDocs
                    .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new ContactDocModel(x.Guid)
                    {
                        Target = x.Contact,
                        Cod = (ContactDocModel.Cods)x.Type,
                        Ref= x.Ref,
                        Fch= x.Fch,
                        Obsoleto= x.Obsoleto,
                        Docfile = x.Hash == null ? null : new DocfileModel(x.Hash)
                    }).FirstOrDefault();
            }
        }

        public static Media? Docfile(Guid guid)
        {
            Media? retval = null;
            using (var db = new Entities.MaxiContext())
            {
                retval = db.CliDocs
                    .Where(x=>x.Guid == guid && x.HashNavigation != null)?
                    .Select(x=>new Media((Media.MimeCods)x.HashNavigation!.Mime, x.HashNavigation.Doc))
                    .FirstOrDefault();
            }
            return retval;
        }

        public static bool Update(ContactDocModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                if (value.Docfile?.Hash != null) DocfileService.Update(db, value.Docfile);

                Entities.CliDoc? entity;
                var fch = value.Fch!;
                if (value.IsNew)
                {
                    entity = new Entities.CliDoc();
                    entity.Guid = value.Guid;
                    db.CliDocs.Add(entity);
                }
                else
                {
                    entity = db.CliDocs
                        .FirstOrDefault(x => x.Guid == value.Guid);

                    if (entity == null) throw new System.Exception("Doc not found");
                }

                entity.Contact = (Guid)value.Target!;
                entity.Ref = value.Ref ?? "";
                entity.Fch = (DateOnly?)value.Fch ?? DateOnly.FromDateTime(DateTime.Today);
                entity.Type = (int)value.Cod;
                entity.Obsoleto = value.Obsoleto;
                entity.Hash = value.Docfile?.Hash;

                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.CliDocs.Remove(db.CliDocs.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }


    }
    public class ContactDocsService
    {
        public static List<ContactDocModel> GetValues(Guid contact)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.CliDocs
                    .AsNoTracking()
                    .Where(x=>x.Contact == contact)
                    .Select(x => new ContactDocModel(x.Guid)
                    {
                        Target = x.Contact,
                        Cod = (ContactDocModel.Cods)x.Type,
                        Ref = x.Ref,
                        Fch = x.Fch,
                        Obsoleto = x.Obsoleto,
                        Docfile = x.Hash == null ? null : new DocfileModel(x.Hash)
                    }).ToList();
            }
        }
    }
}
