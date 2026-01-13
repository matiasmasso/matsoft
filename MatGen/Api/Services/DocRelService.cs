using Api.Entities;
using DTO;
using System.Security.Cryptography;
using static DTO.PersonModel;

namespace Api.Services
{
    public class DocRelService
    {
        private readonly MatGenContext _db;
        public DocRelService(MatGenContext db)
        {
            _db = db;
        }
        public  DocRelModel? Find(Guid guid)
        {
                return _db.DocRels
                    .Where(x => x.Guid == guid)
                    .Select(x => new DocRelModel(x.Guid)
                    {
                        Nom = x.Nom,
                        Sex = x.Sex,
                        SubjectePassiu = x.SubjectePassiu,
                        Ord = x.Ord,
                        Cod = x.Cod
                    }).FirstOrDefault();
        }

        public bool Update(DocRelModel value)
        {
                Entities.DocRel? entity;
                if (value.IsNew)
                {
                    entity = new Entities.DocRel();
                    _db.DocRels.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = _db.DocRels.Find(value.Guid);

                if (entity == null) throw new Exception("DocRel not found");
                entity.Nom = value.Nom ?? "";
                entity.Sex = value.Sex;
                entity.SubjectePassiu = value.SubjectePassiu;
                entity.Ord = value.Ord ?? "";
                entity.Cod = value.Cod;

                // Save changes in database
                _db.SaveChanges();
                return true;
        }

        public bool Delete(Guid guid)
        {
                 var entity = _db.DocRels.Remove(_db.DocRels.Single(x => x.Guid.Equals(guid)));
                _db.SaveChanges();
            return true;

        }

    }
    public class DocRelsService
    {
        private readonly MatGenContext _db;
        public DocRelsService(MatGenContext db)
        {
            _db = db;
        }
        public List<DocRelModel> All()
        {

                return _db.DocRels
                    .OrderBy(x => x.Nom)
                    .Select(x => new DocRelModel(x.Guid)
                    {
                        Nom = x.Nom,
                        Sex = x.Sex,
                        SubjectePassiu = x.SubjectePassiu,
                        Ord = x.Ord,
                        Cod = x.Cod
                    }).ToList();
        }

    }
}
