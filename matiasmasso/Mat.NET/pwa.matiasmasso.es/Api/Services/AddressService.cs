using DTO;

namespace Api.Services
{
    public class AddressService
    {
        public static AddressDTO? Address(Entities.MaxiContext db, Guid srcGuid, int? srcCod = 1)
        {
            return db.VwAddresses
                        .Where(x => x.SrcGuid == srcGuid && x.Cod == srcCod)
                        .Select(x => new AddressDTO()
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
        public static AddressDTO? Address(Entities.VwAddress address)
        {
            var retval = new AddressDTO()
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
    }
}
