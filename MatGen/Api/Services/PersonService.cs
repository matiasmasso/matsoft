using Api.Entities;
using DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static DTO.PersonModel;

namespace Api.Services
{
    public class PersonService
    {
        private readonly MatGenContext _db;
        public PersonService(MatGenContext db)
        {
            _db = db;
        }
        public PersonModel? Find(Guid guid)
        {
                var retval = _db.People
                    .Where(x => x.Guid == guid)
                    .Select(x => new PersonModel(x.Guid)
                    {
                        Nom = x.Nom,
                        Sex = (PersonModel.Sexs)x.Sex,
                        Pare = x.Pare,
                        Mare = x.Mare,
                        FchLocationFrom = new FchLocationModel(x.FchFromQualifier, x.FchFrom, x.FchFrom2, x.LocationFrom),
                        FchLocationTo = new FchLocationModel(x.FchToQualifier, x.FchTo, x.FchTo2, x.LocationTo),
                        FchLocationSepultura = new FchLocationModel(x.FchSepulturaQualifier, x.FchSepultura, x.FchSepultura2, x.Sepultura),
                        Firstnom = x.Firstnom,
                        Cognom = x.Cognom,
                        Profession = x.Profession,
                        Obs = x.Obs,
                        FchCreated = x.FchCreated,
                        FchLastEdited = x.FchLastEdited
                    }).FirstOrDefault();
                return retval;
        }

        public bool Update(PersonModel value)
        {
                 Entities.Person? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Person();
                    _db.People.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = _db.People.Find(value.Guid);

                if (entity == null) throw new Exception("Person not found");

                entity.Nom = value.Nom ?? "";
                entity.Sex = (short)value.Sex;
                entity.Pare = value.Pare;
                entity.Mare = value.Mare;

                entity.FchFromQualifier = value.FchLocationFrom?.Fch?.Qualifier?.ToString();
                entity.FchFrom = value.FchLocationFrom?.Fch?.Fch1?.ToString();
                entity.FchFrom2 = value.FchLocationFrom?.Fch?.Fch2?.ToString();
                entity.LocationFrom = value.FchLocationFrom?.Location?.Guid;

                entity.FchToQualifier = value.FchLocationTo?.Fch?.Qualifier?.ToString();
                entity.FchTo = value.FchLocationTo?.Fch?.Fch1?.ToString();
                entity.FchTo2 = value.FchLocationTo?.Fch?.Fch2?.ToString();
                entity.LocationTo = value.FchLocationTo?.Location?.Guid;

                entity.FchSepulturaQualifier = value.FchLocationSepultura?.Fch?.Qualifier?.ToString();
                entity.FchSepultura = value.FchLocationSepultura?.Fch?.Fch1?.ToString();
                entity.FchSepultura2 = value.FchLocationSepultura?.Fch?.Fch2?.ToString();
                entity.Sepultura = value.FchLocationSepultura?.Location?.Guid;

                entity.Firstnom = value.Firstnom;
                entity.Cognom = value.Cognom;
                entity.Profession = value.Profession;
                entity.Obs = value.Obs;
                entity.FchLastEdited = DateTime.Now;

                // Save changes in database
                _db.SaveChanges();
                return true;
        }

        public  bool Delete(Guid guid)
        {
                var entity = _db.People.Remove(_db.People.Single(x => x.Guid.Equals(guid)));
                _db.SaveChanges();
            return true;

        }

    }
    public class PersonsService
    {
        private readonly MatGenContext _db;
        public PersonsService(MatGenContext db)
        {
            _db = db;
        }
        public  List<PersonModel>All()
        {
            var retval = _db.People
                .OrderBy(x => x.Nom)
                .Select(x => new PersonModel(x.Guid)
                {
                    Nom = x.Nom,
                    Sex = (PersonModel.Sexs)x.Sex,
                    Pare=x.Pare,
                    Mare=x.Mare,

                    FchLocationFrom = new FchLocationModel(x.FchFromQualifier, x.FchFrom, x.FchFrom2, x.LocationFrom),
                    FchLocationTo = new FchLocationModel(x.FchToQualifier, x.FchTo, x.FchTo2, x.LocationTo),
                    FchLocationSepultura = new FchLocationModel(x.FchSepulturaQualifier, x.FchSepultura, x.FchSepultura2, x.Sepultura),

                    Firstnom=x.Firstnom,
                    Cognom=x.Cognom,
                    Profession=x.Profession,
                    Obs=x.Obs,
                    FchCreated=x.FchCreated,
                    FchLastEdited = x.FchLastEdited
                }).OrderByDescending(x=>x.FchLastEdited)
                .ToList();

                return retval;

        }

    }
}
