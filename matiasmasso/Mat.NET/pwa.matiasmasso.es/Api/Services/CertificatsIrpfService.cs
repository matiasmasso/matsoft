using DTO;
namespace Api.Services
{
    public class CertificatIrpfService
    {
        public static DocFileModel? Docfile(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.CertificatIrpfs
                   .Where(x => x.Guid == guid)
                   .Join(db.DocFiles, cert => cert.Hash, docfile => docfile.Hash, (cert, docfile) => new DocFileModel
                   {
                       StreamMime = docfile.Mime,
                       Document = docfile.Doc
                   }).FirstOrDefault();
            }
        }
    }
    public class CertificatsIrpfService
    {
        public static StaffDTO FromUser(UserModel user)
        {
            using (var db = new Entities.MaxiContext())
            {
                return FromUser(db, user);
            }
        }

        public static StaffDTO FromUser(Entities.MaxiContext db, UserModel user)
        {
            var retval = new StaffDTO();
            if (user.DefaultContact == null) throw new Exception("User with no default contact");
            retval.Properties = StaffService.Find(db, (Guid)user.DefaultContact);

            retval.CertificatsIrpf = db.EmailClis
               .Where(x => x.EmailGuid == user.Guid)
               .Join(db.CertificatIrpfs, contact => contact.ContactGuid, cert => cert.Contact, (contact, cert) => new CertificatIrpfDTO
               {
                   Guid = cert.Guid,
                   Year = cert.Year
               })
               .OrderByDescending(x => x.Year)
               .ToList();
            return retval;
        }

        public static List<CertificatIrpfDTO> FromContact(Guid contact)
        {
            using (var db = new Entities.MaxiContext())
            {
                return FromContact(db, contact);
            }
        }
        public static List<CertificatIrpfDTO> FromContact(Entities.MaxiContext db, Guid contact)
        {
            return db.CertificatIrpfs
               .Where(x => x.Contact == contact)
               .OrderByDescending(x => x.Year)
               .Select(x => new CertificatIrpfDTO
               {
                   Guid = x.Guid,
                   Year = x.Year
               }).ToList();
        }
    }
}
