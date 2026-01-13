using Api.Entities;
using DTO;

namespace Api.Services
{
    public class RepoService
    {
        private readonly MatGenContext _db;
        public RepoService(MatGenContext db)
        {
            _db = db;
        }
        public RepoModel? Find(Guid guid)
        {
            return _db.Repos
                .Where(x => x.Guid == guid)
                .Select(x => new RepoModel(x.Guid)
                {
                    Nom = x.Nom,
                    Abr = x.Abr,
                    Adr = x.Adr,
                    Location = x.Location,
                    Zip = x.Zip,
                    Country = x.Country
                }).FirstOrDefault();
        }

        public bool Update(RepoModel value)
        {
            Entities.Repo? entity;
            if (value.IsNew)
            {
                entity = new Entities.Repo();
                _db.Repos.Add(entity);
                entity.Guid = value.Guid;
            }
            else
                entity = _db.Repos.Find(value.Guid);

            if (entity == null) throw new Exception("Repo not found");

            entity.Nom = value.Nom;
            entity.Abr = value.Abr;
            entity.Adr = value.Adr;
            entity.Location = value.Location;
            entity.Zip = value.Zip;
            entity.Country = value.Country ?? "ES";

            // Save changes in database
            _db.SaveChanges();
            return true;
        }

        public bool Delete(Guid guid)
        {
            var entity = _db.Repos.Remove(_db.Repos.Single(x => x.Guid.Equals(guid)));
            _db.SaveChanges();
            return true;

        }


    }
    public class ReposService
    {
        private readonly MatGenContext _db;
        public ReposService(MatGenContext db)
        {
            _db = db;
        }
        public List<RepoModel> All()
        {

            return _db.Repos
                .OrderBy(x => x.Nom)
                .Select(x => new RepoModel(x.Guid)
                {
                    Nom = x.Nom,
                    Abr = x.Abr,
                    Adr = x.Adr,
                    Location = x.Location,
                    Zip = x.Zip,
                    Country = x.Country
                }).ToList();
        }

    }
}
