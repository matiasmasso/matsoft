using Api.Entities;
using DTO;
using Microsoft.EntityFrameworkCore;
using static DTO.DocTargetModel;

namespace Api.Services
{
    public class DocTargetService
    {
        private readonly MatGenContext _db;
        public DocTargetService(MatGenContext db)
        {
            _db = db;
        }
        public  DocTargetModel? Find(Guid guid)
        {
                return _db.DocTargets
                    .Where(x => x.Guid == guid)
                    .Select(x => new DocTargetModel(x.Guid)
                    {
                        Doc = x.Doc,
                        Target = x.Target,
                        TargetCod = (TargetCods)x.TargetCod,
                        Rel = x.Rel,
                        Difunt = x.Difunt,
                        SubjectePassiu = x.SubjectePassiu
                    }).FirstOrDefault();
        }

        public  bool Update(DocTargetModel value)
        {
                Entities.DocTarget? entity;
                if (value.IsNew)
                {
                    entity = new Entities.DocTarget();
                    _db.DocTargets.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = _db.DocTargets.Find(value.Guid);

                if (entity == null) throw new Exception("DocTarget not found");

                entity.Doc = (Guid)value.Doc!;
                entity.Target = (Guid)value.Target!;
                entity.TargetCod = (int)value.TargetCod;
                entity.Rel = (Guid) value.Rel!;
                entity.Difunt = value.Difunt;
                entity.SubjectePassiu = value.SubjectePassiu;

                // Save changes in database
                _db.SaveChanges();
                return true;
        }

        public  bool Delete(Guid guid)
        {
                var entity = _db.DocTargets.Remove(_db.DocTargets.Single(x => x.Guid.Equals(guid)));
                _db.SaveChanges();
            return true;

        }


    }
    public class DocTargetsService
    {
        private readonly MatGenContext _db;
        public DocTargetsService(MatGenContext db)
        {
            _db = db;
        }
        public  List<DocTargetModel> All()
        {

                return _db.DocTargets
                    .Select(x => new DocTargetModel(x.Guid)
                    {
                        Doc = x.Doc,
                        Target = x.Target,
                        TargetCod = (TargetCods)x.TargetCod,
                        Rel = x.Rel,
                        Difunt = x.Difunt,
                        SubjectePassiu = x.SubjectePassiu
                    }).ToList();

        }
        public  List<DocTargetModel> FromDoc(Guid doc)
        {

                return _db.DocTargets
                    .Where(x => x.Doc == doc)
                    .OrderByDescending(x => x.SubjectePassiu)
                    .ThenBy(x => x.RelNavigation.Ord)
                    .Select(x => new DocTargetModel(x.Guid)
                    {
                        Doc = x.Doc,
                        Target = x.Target,
                        TargetCod = (TargetCods)x.TargetCod,
                        Rel = x.Rel,
                        Difunt = x.Difunt,
                        SubjectePassiu = x.SubjectePassiu
                    }).ToList();

        }

        public List<DocTargetModel> FromTarget(Guid target)
        {

                return _db.DocTargets
                    .Where(x => x.Target == target)
                    .OrderByDescending(x => x.SubjectePassiu)
                    .ThenBy(x => x.DocNavigation.Fch)
                    .Select(x => new DocTargetModel(x.Guid)
                    {
                        Doc = x.Doc,
                        Target = x.Target,
                        TargetCod = (TargetCods)x.TargetCod,
                        Rel = x.Rel,
                        Difunt = x.Difunt,
                        SubjectePassiu = x.SubjectePassiu
                    }).ToList();

        }

    }
}
