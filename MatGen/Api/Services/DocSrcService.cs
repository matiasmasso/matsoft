using Api.Entities;
using DTO;

namespace Api.Services
{
    public class DocSrcService
    {
        private readonly MatGenContext _db;
        public DocSrcService(MatGenContext db)
        {
            _db = db;
        }
        public DocSrcModel? Find(Guid guid)
        {
            return _db.DocSrcs
               .Where(x => x.Guid == guid)
               .Select(x => new DocSrcModel(x.Guid)
               {
                   Parent = x.Parent,
                   Nom = x.Nom,
                   NomLlarg = x.NomLlarg,
                   Ord = x.Ord,
                   Url = x.Url,
                   Repo = x.Repo,
                   Obs = x.Obs,
                   DocFile = x.HashNavigation == null ? null : new DocfileModel
                   {
                       Hash = x.Hash,
                       Size = x.HashNavigation.Size,
                       Pags = x.HashNavigation.Pags,
                       Document = new DTO.Media((Media.MimeCods)x.HashNavigation.StreamMime, null),
                       Thumbnail = new Media((Media.MimeCods)x.HashNavigation.ThumbnailMime, null),
                       FchCreated = x.FchCreated
                   }
               }).FirstOrDefault();
        }

        public Media? Media(Guid guid)
        {
            return _db.DocSrcs
               .Where(x => x.Guid == guid && x.HashNavigation != null)
               .Select(x => new Media()
               {
                   Data = x.HashNavigation!.Stream,
                   Mime = (Media.MimeCods)x.HashNavigation.StreamMime
               }).FirstOrDefault();
        }
        public Media? Thumbnail(Guid guid)
        {
            var retval = _db.DocSrcs
                .Where(x => x.Guid == guid && x.HashNavigation != null)
                .Select(x => new Media()
                {
                    Data = x.HashNavigation!.Thumbnail,
                    Mime = (Media.MimeCods)x.HashNavigation.ThumbnailMime
                }).FirstOrDefault();
            return retval;
        }



        public bool Update(DocSrcModel value)
        {
            bool retval = false;
            var entity = _db.DocSrcs.Find(value.Guid);
            if (entity == null)
            {
                entity = new Entities.DocSrc();
                entity.Guid = value.Guid;
                _db.DocSrcs.Add(entity);
            }

            entity.Nom = value.Nom ?? "";
            entity.NomLlarg = value.NomLlarg;
            entity.Parent = value.Parent;
            entity.Ord = value.Ord;
            entity.Url = value.Url;
            entity.Hash = value.DocFile == null ? null : value.DocFile.Hash;
            entity.Repo = value.Repo;
            entity.Obs = value.Obs;

            if (value.DocFile != null)
                DocFileService.Update(_db, value.DocFile);

            _db.SaveChanges();
            retval = true;
            return retval;
        }


        public bool Delete(Guid guid)
        {
            var entity = _db.DocSrcs.Remove(_db.DocSrcs.Single(x => x.Guid.Equals(guid)));
            _db.SaveChanges();
            return true;

        }


    }
    public class DocSrcsService
    {
        private readonly MatGenContext _db;
        public DocSrcsService(MatGenContext db)
        {
            _db = db;
        }
        public List<DocSrcModel> All()
        {

            return _db.DocSrcs
                .OrderBy(x => x.Nom)
                .Select(x => new DocSrcModel(x.Guid)
                {
                    Parent = x.Parent,
                    Nom = x.Nom,
                    NomLlarg = x.NomLlarg,
                    Ord = x.Ord,
                    Url = x.Url,
                    Repo = x.Repo,
                    Obs = x.Obs,
                    DocFile = x.HashNavigation == null ? null : new DocfileModel
                    {
                        Hash = x.Hash,
                        Size = x.HashNavigation.Size,
                        Pags = x.HashNavigation.Pags,
                        Document = new DTO.Media((Media.MimeCods)x.HashNavigation.StreamMime, null),
                        Thumbnail = new Media((Media.MimeCods)x.HashNavigation.ThumbnailMime, null),
                        FchCreated = x.FchCreated
                    }
                }).ToList();
        }

    }
}
