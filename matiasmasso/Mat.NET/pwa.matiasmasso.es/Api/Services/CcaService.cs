using DocumentFormat.OpenXml.Vml.Office;
using DTO;

namespace Api.Services
{
    public class CcaService
    {
        public static CcaModel? Find(Guid guid, LangDTO lang)
        {
            using (var db = new Entities.MaxiContext())
            {
                CcaModel? retval = db.Ccas
                    .Where(x => x.Guid == guid)
                    .Select(x => new CcaModel(x.Guid)
                    {
                        Id = x.Cca1,
                        Fch = x.Fch,
                        Concept = x.Txt,
                        Ccd = x.Ccd,
                        Cdn = x.Cdn,
                        Docfile = x.Hash == null ? null : new DocFileModel(x.Hash)
                    })
                    .FirstOrDefault();

                if (retval != null)
                {
                    retval.Items = db.Ccbs
                        .Where(x => x.CcaGuid == guid)
                        .Select(x => new CcaModel.Item(x.Guid)
                        {
                            Cta=x.CtaGuid,
                            Contact = x.ContactGuid,
                            Eur = x.Eur,
                            Dh = x.Dh
                        }).ToList();
                }
                
                if(retval.Docfile != null)
                {
                    var hash = retval.Docfile.Hash;
                    retval.Docfile = db.DocFiles
                        .Where(x => x.Hash == hash)
                        .Select(x => new DocFileModel(x.Hash)
                        {
                            StreamMime = x.Mime,
                            ThumbnailMime= x.ThumbnailMime,
                            Pags = x.Pags,
                            Size = x.Size,
                            Width = x.Width,
                            Height = x.Height,
                            FchCreated = x.FchCreated
                        })
                        .FirstOrDefault();
                }

                return retval;
            }
        }

        public static DocFileModel? Docfile(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.Ccas
                    .Where(x => x.Guid == guid)
                    .Join(db.DocFiles, cca => cca.Hash, docfile => docfile.Hash, (cca, docfile) => new DocFileModel
                    {
                        Document = docfile.Doc,
                        StreamMime = docfile.Mime
                    })
                    .FirstOrDefault();
                return retval;
            }
        }

        public static ImageMime? Thumbnail(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.Ccas
                    .Where(x => x.Guid == guid)
                    .Join(db.DocFiles, cca => cca.Hash, docfile => docfile.Hash, (cca, docfile) => new ImageMime
                    {
                        Image = docfile.Thumbnail,
                        Mime = MimeHelper.MimeCods.Jpg
                    })
                    .FirstOrDefault();
                return retval;
            }
        }

    }

    public class CcasService
    {
        public static List<CcaModel>GetValues(int emp, int year)
        {
            using (var db = new Entities.MaxiContext())
            {
                //return new List<CcaModel>();
                return db.VwCcasLists
                    .Where(x => x.Emp == emp & x.Yea == year)
                    .OrderByDescending(x => x.Cca)
                    .Select(x => new CcaModel(x.Guid)
                    {
                        Id = x.Cca,
                        Fch = x.Fch,
                        Concept = x.Txt,
                        Docfile = x.Hash == null ? null : new DocFileModel(x.Hash),
                        Amount = x.Eur,
                       UsrCreated = x.UsrNom == null ? null : new GuidNom(Guid.NewGuid(),x.UsrNom)
                    }).ToList();
            }
        }
    }
}
