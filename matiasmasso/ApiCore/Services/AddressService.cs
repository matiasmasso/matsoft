using Api.Entities;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class AddressService
    {
        public static AddressModel? Address(Entities.MaxiContext db, Guid srcGuid, int? srcCod = 1)
        {
            return db.VwAddresses
                    .AsNoTracking()
                        .Where(x => x.SrcGuid == srcGuid && x.Cod == srcCod)
                        .Select(x => new AddressModel()
                        {
                            Text = x.Adr,
                            Zip = x.ZipGuid == null ? null : new ZipDTO((Guid)x.ZipGuid)
                            {
                                ZipCod = x.ZipCod,
                                Location = new LocationDTO((Guid)x.LocationGuid!)
                                {
                                    Nom = x.LocationNom,
                                    Zona = new ZonaDTO((Guid)x.ZonaGuid!)
                                    {
                                        Nom = x.ZonaNom,
                                        Country = new CountryDTO((Guid)x.CountryGuid!)
                                        {
                                            Nom = new LangTextDTO()
                                            {
                                                Esp = x.CountryEsp,
                                                Cat = x.CountryCat,
                                                Eng = x.CountryEng,
                                                Por = x.CountryPor
                                            },
                                            Iso = x.CountryIso
                                        },
                                        Provincia = x.ProvinciaGuid == null ? null : new ProvinciaDTO((Guid)x.ProvinciaGuid!)
                                        {
                                            Nom = x.ProvinciaNom
                                        }
                                    },
                                }
                            }
                        }).FirstOrDefault();
        }
        public static AddressModel? Address(Entities.VwAddress address)
        {
            var retval = new AddressModel()
            {
                Text = address.Adr,
                Zip = address.ZipGuid == null ? null : new ZipDTO((Guid)address.ZipGuid)
                {
                    ZipCod = address.ZipCod,
                    Location = new LocationDTO((Guid)address.LocationGuid!)
                    {
                        Nom = address.LocationNom,
                        Zona = new ZonaDTO((Guid)address.ZonaGuid!)
                        {
                            Nom = address.ZonaNom,
                            Country = new CountryDTO((Guid)address.CountryGuid!)
                            {
                                Nom = new LangTextDTO()
                                {
                                    Esp = address.CountryEsp,
                                    Cat = address.CountryCat,
                                    Eng = address.CountryEng,
                                    Por = address.CountryPor
                                }
                            },
                            Provincia = address.ProvinciaGuid == null ? null : new ProvinciaDTO((Guid)address.ProvinciaGuid!)
                            {
                                Nom = address.ProvinciaNom
                            }
                        },
                    }
                }
            };
            return retval;
        }

        public static void Update(MaxiContext db, AddressModel value)
        {
            Entities.CliAdr? entity = db.CliAdrs
                .Where(x => x.SrcGuid == value.Contact && x.Cod == (int)value.Cod)
                .FirstOrDefault();

            if (entity == null)
            {
                entity = new Entities.CliAdr();
                db.CliAdrs.Add(entity);
                entity.SrcGuid = (Guid)value.Contact!;
                entity.Cod = (int)value.Cod;
            }

            entity.Adr = value.Text;
            entity.Zip = value.ZipGuid ?? value.Zip?.Guid;
        }

        public static bool Delete(MaxiContext db, AddressModel value)
        {
            Entities.CliAdr? entity = db.CliAdrs
                .Where(x => x.SrcGuid == value.Contact && x.Cod == (int)value.Cod)
                .FirstOrDefault();

            if (entity != null)
            {
                db.CliAdrs.Remove(entity);
                db.SaveChanges();
            }
            return true;

        }
    }





    public class AddressesService
    {
        public static List<AddressModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return GetValues(db);
            }
        }
        public static List<AddressModel> GetValues(Entities.MaxiContext db)
        {
            return db.CliAdrs
                    .AsNoTracking()
                            .Select(x => new AddressModel()
                            {
                                Contact = x.SrcGuid,
                                Cod = (AddressModel.Cods)x.Cod,
                                Text = x.Adr,
                                Zip = x.Zip == null ? null : new ZipDTO((Guid)x.Zip),
                                Longitude = (double?)x.Longitud,
                                Latitude = (double?)x.Latitud
                            }).ToList();
        }
    }
}
