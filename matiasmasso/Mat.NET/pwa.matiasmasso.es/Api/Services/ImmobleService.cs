using DTO;

namespace Api.Services
{
    public class ImmobleService
    {

        public static ImmobleModel? FromInventariItem(Guid inventariItem, LangDTO lang)
        {
            ImmobleModel? retval = null;
            Guid? immobleGuid = null;
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.InventariItems.
                    Where(x => x.Guid == inventariItem).FirstOrDefault();
                if (entity != null) immobleGuid = entity.Immoble;
            }
            if (immobleGuid != null)
                retval = Fetch((Guid)immobleGuid!, lang);
            return retval;
        }

        public static ImmobleModel? Fetch(Guid guid, LangDTO lang)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = (from x in db.Immobles
                              join y in db.VwZips on x.ZipGuid equals y.ZipGuid
                              where x.Guid == guid
                              select new ImmobleModel(guid)
                              {
                                  Emp = (EmpModel.EmpIds)x.Emp,
                                  Nom = x.Nom,
                                  FchFrom = x.FchFrom,
                                  FchTo = x.FchTo,
                                  Cadastre = x.Cadastre,
                                  Titularitat = x.Titularitat,
                                  Part = x.Part,
                                  Address = new AddressDTO()
                                  {
                                      Text = x.Adr,
                                      Zip = x.ZipGuid == null ? null : new ZipDTO((Guid)x.ZipGuid)
                                      {
                                          ZipCod = y.ZipCod,
                                          Location = new LocationDTO((Guid)y.LocationGuid!)
                                          {
                                              Nom = y.LocationNom,
                                              Zona = new ZonaDTO((Guid)y.ZonaGuid!)
                                              {
                                                  Nom = y.ZonaNom,
                                                  Country = new CountryDTO((Guid)y.CountryGuid!)
                                                  {
                                                      Nom = new LangTextDTO()
                                                      {
                                                          Esp = lang.Tradueix(y.CountryEsp, y.CountryCat, y.CountryEng, y.CountryPor)
                                                      }
                                                  },
                                                  Provincia = y.ProvinciaGuid == null ? null : new ProvinciaDTO((Guid)y.ProvinciaGuid!)
                                                  {
                                                      Nom = y.ProvinciaNom
                                                  }
                                              },
                                          }
                                      }
                                  }
                              }).FirstOrDefault();

                if (retval != null)
                {
                    retval.Docfiles = DocfilesService.FromSrc(db, retval.Guid);
                    retval.Inventari = Inventari(db, retval.Guid);
                }
                return retval;
            }

        }

        public static bool Update(ImmobleModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                var guid = value.Guid;
                Entities.Immoble? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Immoble();
                    db.Immobles.Add(entity);
                    entity.Guid = guid;
                }
                else
                    entity = db.Immobles.Find(guid);

                if (entity == null) throw new Exception("Immoble not found");

                entity.Emp = (int)value.Emp;
                entity.Nom = value.Nom;
                entity.FchFrom = value.FchFrom;
                entity.FchTo = value.FchTo;
                entity.Cadastre = value.Cadastre;
                entity.Titularitat = value.Titularitat;
                entity.Part = value.Part;
                entity.Adr = value.Address?.Text;
                entity.ZipGuid = value.Address?.Zip?.Guid;

                db.SaveChanges();
                return true;
            }
        }


        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                db.Immobles.Remove(db.Immobles.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }

        public static ImmobleModel.InventariItem? InventariItem(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.InventariItems
                    .Where(x => x.Guid == guid)
                    .Select(x => new ImmobleModel.InventariItem(x.Guid)
                    {
                        Nom = x.Nom,
                        Obs = x.Obs
                    })
                    .FirstOrDefault();

                if (retval != null)
                {
                    retval.Docfiles = DocfilesService.FromSrc(db, guid);
                }
                return retval;
            }
        }
        public static List<ImmobleModel.InventariItem> Inventari(Entities.MaxiContext db, Guid target)
        {
            return db.InventariItems.
                        Where(x => x.Immoble == target).
                        OrderBy(y => y.Nom).
                        Select(z => new ImmobleModel.InventariItem
                        {
                            Guid = z.Guid,
                            Nom = z.Nom,
                        }).ToList();
        }
    }

    public class ImmoblesService
    {

        public static List<ImmobleModel> Fetch(UserModel user, LangDTO lang)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = (from x in db.Immobles
                              select new ImmobleModel(x.Guid)
                              {
                                  Emp = (EmpModel.EmpIds)x.Emp,
                                  Nom = x.Nom,
                                  Cadastre = x.Cadastre,
                                  FchFrom = x.FchFrom,
                                  FchTo = x.FchTo,
                              }).ToList();
                return retval;
            }

        }
    }
}
