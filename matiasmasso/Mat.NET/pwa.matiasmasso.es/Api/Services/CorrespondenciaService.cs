using DTO;

namespace Api.Services
{
    public class CorrespondenciaService
    {
        public static DocFileModel? Docfile(Guid guid)
        {
            DocFileModel? retval = null;
            using (var db = new Entities.MaxiContext())
            {
                retval = (from item in db.Crrs
                          join docfiles in db.DocFiles
                          on item.Hash equals docfiles.Hash
                          where item.Guid.Equals(guid)
                          select new DocFileModel()
                          {
                              Document = docfiles.Doc,
                              StreamMime = docfiles.Mime
                          }).FirstOrDefault();
            }
            return retval;
        }

    }


    public class ContactCorrespondenciaService
    {
        public static ContactCorrespondenciaDTO Fetch(Guid contact)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new ContactCorrespondenciaDTO();
                retval.Contact = ContactService.ContactModel(contact);
                retval.Items = (from item in db.Crrs
                              where item.CliGus.Any(x => x.Guid.Equals(contact))
                              orderby item.Fch descending
                              select new CorrespondenciaDTO(item.Guid)
                              {
                                  Id = item.Crr1,
                                  Subject = item.Dsc,
                                  Fch = item.Fch,
                                  HasPdf = !string.IsNullOrEmpty(item.Hash)
                              }).ToList();
                return retval;
            }
        }
    }
}
