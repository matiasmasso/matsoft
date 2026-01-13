using Api.Entities;
using DTO;
using DTO.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Api.Services
{
    public class DocService
    {
        private readonly MatGenContext _db;
        public DocService(MatGenContext db)
        {
            _db = db;
        }
        public  DocModel? Find(Guid guid)
        {
                 return _db.Docs
                    .Where(x => x.Guid == guid)
                    .Select(x => new DocModel(x.Guid)
                    {
                        Tit = x.Tit,
                        Fch = x.Fch,
                        Cod = x.Cod,
                        Src = x.Src,
                        SrcDetail = x.SrcDetail,
                        ExternalUrl = x.ExternalUrl,
                        Transcripcio = x.Transcripcio,
                        FchCreated = x.FchCreated,
                        DocFile = x.HashNavigation == null ? null : new DocfileModel
                        {
                            Hash = x.Hash,
                            Document = new Media((Media.MimeCods)x.HashNavigation.StreamMime, null),
                            Thumbnail = new Media((Media.MimeCods)x.HashNavigation.ThumbnailMime, null),
                            Pags = x.HashNavigation.Pags,
                            Size = x.HashNavigation.Size,
                            FchCreated = x.HashNavigation.FchCreated
                        }
                    }).FirstOrDefault();
        }

        public  Media? Media(Guid guid)
        {
                return _db.Docs
                    .Where(x => x.Guid == guid && x.HashNavigation != null)
                    .Select(x => new Media()
                    {
                        Data = x.HashNavigation!.Stream,
                        Mime = (Media.MimeCods)x.HashNavigation.StreamMime
                    }).FirstOrDefault();
        }
        public  Media? Thumbnail(Guid guid)
        {
                var retval = _db.Docs
                    .Where(x => x.Guid == guid && x.HashNavigation != null)
                    .Select(x => new Media()
                    {
                        Data = x.HashNavigation!.Thumbnail,
                        Mime = (Media.MimeCods)x.HashNavigation.ThumbnailMime
                    }).FirstOrDefault();
                return retval;
        }

        public bool Update(DocModel value)
        {
            bool retval = false;
                var entity = _db.Docs.Find(value.Guid);
                if (entity == null)
                {
                    entity = new Entities.Doc();
                    entity.Guid = value.Guid;
                    _db.Docs.Add(entity);
                }

                entity.Tit = value.Tit;
                entity.Fch = value.Fch;
                entity.Cod = value.Cod;
                entity.Src = value.Src;
                entity.SrcDetail = value.SrcDetail;
                entity.ExternalUrl = value.ExternalUrl;
                entity.Transcripcio = value.Transcripcio;
                entity.FchLastEdited = DateTime.Now;

                if (value.DocFile != null)
                {
                    entity.Hash = value.DocFile.Hash;
                     DocFileService.Update( _db, value.DocFile);
                }

                _db.SaveChanges();
                retval = true;
            return retval;
        }

        public  bool Delete(Guid guid)
        {
                var entity = _db.Docs.Remove(_db.Docs.Single(x => x.Guid.Equals(guid)));
                _db.SaveChanges();
            return true;
        }



    }

    public class DocsService
    {
        private readonly MatGenContext _db;
        public DocsService(MatGenContext db)
        {
            _db = db;
        }
        public  List<DocModel> All()
        {
                return _db.Docs
                    .OrderByDescending(x => x.FchLastEdited)
                    .Select(x => new DocModel(x.Guid)
                    {
                        Tit = x.Tit,
                        Fch = x.Fch,
                        Cod = x.Cod,
                        Src = x.Src,
                        SrcDetail = x.SrcDetail,
                        ExternalUrl = x.ExternalUrl,
                        Transcripcio = x.Transcripcio,
                        FchCreated = x.FchCreated,
                        DocFile = x.HashNavigation == null ? null : new DocfileModel
                        {
                            Hash = x.Hash,
                            Document = new Media((Media.MimeCods)x.HashNavigation.StreamMime, null),
                            Thumbnail = new Media((Media.MimeCods)x.HashNavigation.ThumbnailMime, null), //TO DO: create mime fields
                            Pags = x.HashNavigation.Pags,
                            Size = x.HashNavigation.Size,
                            FchCreated = x.HashNavigation.FchCreated
                        }
                    })
                    .ToList();
        }


        public int SetSha256()
        {
            List<string> hashes;

                hashes = _db.DocFiles
                    .Where(x => x.Sha256 == null)
                    .Select(x => x.Hash).ToList();


                foreach (var hash in hashes)
                {
                    SetSha256(_db, hash);
                }
            return hashes.Count;

        }



        private static void SetSha256(Entities.MatGenContext db, string hash)
        {
            var entity = db.DocFiles.Find(hash);
            if (entity!.Stream != null)
            {
                var bytes = entity!.Stream;
                var sha256 = DTO.Helpers.CryptoHelper.Sha256(bytes!);
                entity.Sha256 = sha256;
                db.SaveChanges();
            }
        }



    }
}
