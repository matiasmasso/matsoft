using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class CertificatIrpfService
    {
        public static DocfileModel? Docfile(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.CertificatIrpfs
                    .AsNoTracking()
                   .Where(x => x.Guid == guid)
                   .Join(db.DocFiles, cert => cert.Hash, docfile => docfile.Hash, (cert, docfile) => new DocfileModel
                   {
                       Document = new Media((Media.MimeCods)docfile.Mime, docfile.Doc)
                   }).FirstOrDefault();
            }
        }

        public static bool Update(CertificatIrpfModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                if (value.Docfile?.Hash != null) DocfileService.Update(db, value.Docfile);

                Entities.CertificatIrpf? entity;
                if (value.IsNew)
                {
                    entity = new Entities.CertificatIrpf();
                    entity.Guid = value.Guid;
                    db.CertificatIrpfs.Add(entity);
                }
                else
                {
                    entity = db.CertificatIrpfs
                        .Include(x => x.Contact)
                        .FirstOrDefault(x => x.Guid == value.Guid);

                    if (entity == null) throw new System.Exception("Doc not found");
                }

                entity.Year = value.Year;
                entity.Contact = value.Contact;
                entity.Hash = value.Docfile?.Hash;

                db.SaveChanges();
                return true;
            }
        }
    }
    public class CertificatsIrpfService
    {
        public static List<CertificatIrpfModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return GetValues(db);
            }
        }

        public static List<CertificatIrpfModel>GetValues(Entities.MaxiContext db)
        {
            return db.CertificatIrpfs
                .AsNoTracking()
                .Select(x => new CertificatIrpfModel(x.Guid)
                {
                    Year = x.Year,
                    Contact = x.Contact
                }).ToList();
        }

        public static List<CertificatIrpfModel> FromContact(Guid contact)
        {
            using (var db = new Entities.MaxiContext())
            {
                return FromContact(db, contact);
            }
        }
        public static List<CertificatIrpfModel> FromContact(Entities.MaxiContext db, Guid contact)
        {
            return db.CertificatIrpfs
               .AsNoTracking()
               .Where(x => x.Contact == contact)
               .OrderByDescending(x => x.Year)
               .Select(x => new CertificatIrpfModel
               {
                   Guid = x.Guid,
                   Year = x.Year
               }).ToList();
        }
    }
}
