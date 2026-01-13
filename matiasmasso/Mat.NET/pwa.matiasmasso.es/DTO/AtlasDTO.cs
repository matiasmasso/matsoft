using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class AtlasModel
    {
        public DateTime? Fch { get; set; }
        public List<CountryModel> Countries { get; set; } = new();
        public List<RegionModel> Regions { get; set; } = new();
        public List<ProvinciaModel> Provincias { get; set; } = new();
        public List<ZonaModel> Zonas { get; set; } = new();
        public List<LocationModel> Locations { get; set; } = new();
        public List<ZipModel> Zips { get; set; } = new();

        public AtlasModel Request() => new AtlasModel { Fch = Fch };

        public CountryModel? Country(Guid guid) => Countries.FirstOrDefault(c => c.Guid == guid);
        public CountryModel? Country(LocationModel location) => Countries.FirstOrDefault(x=> Zonas.FirstOrDefault(y => y.Guid == location.Guid)?.Country == x.Guid);
        public RegionModel? Region(Guid guid) => Regions.FirstOrDefault(c => c.Guid == guid);
        public ZonaModel? Zona(Guid guid) => Zonas.FirstOrDefault(c => c.Guid == guid);
        public ProvinciaModel? Provincia(Guid guid) => Provincias.FirstOrDefault(c => c.Guid == guid);
        public LocationModel? Location(Guid guid) => Locations.FirstOrDefault(c => c.Guid == guid);
        public ZipModel? Zip(Guid guid) => Zips.FirstOrDefault(c => c.Guid == guid);

        public CountryModel? Country(CountryModel.Wellknowns id)
        {
            if (id == CountryModel.Wellknowns.Spain)
                return Country(new Guid("AEEA6300-DE1D-4983-9AA2-61B433EE4635"));
            else if (id == CountryModel.Wellknowns.Portugal)
                return Country(new Guid("631B1258-9761-4254-8ED9-25B9E42FD6D1"));
            else if (id == CountryModel.Wellknowns.Andorra)
                return Country(new Guid("AE3E6755-8FB7-40A5-A8B3-490ED2C44061"));
            else if (id == CountryModel.Wellknowns.Germany)
                return Country(new Guid("B21500BA-2891-4742-8CFF-8DD65EBB0C82"));
            else
                return null;
        }

        public List<CountryModel> FavoriteCountries()
        {
            var retval = new List<CountryModel>();
            retval.Add(Country(CountryModel.Wellknowns.Spain)!);
            retval.Add(Country(CountryModel.Wellknowns.Portugal)!);
            retval.Add(Country(CountryModel.Wellknowns.Andorra)!);
            return retval;
        }
        public LocationModel? Location(LocationModel.Wellknowns id)
        {
            if (id == LocationModel.Wellknowns.Barcelona)
                return Location(new Guid("3B94D3E8-9A82-4628-88ED-CD7BC0F6FD36"));
            else
                return null;
        }


        public CountryModel DefaultCountry() => Country(CountryModel.Wellknowns.Spain)!;

        public List<ProvinciaModel> CountryProvinces(Guid country)
        {
            var regions = Regions.Where(x=>x.Country == country).ToList();
            var retval = Provincias.Where(x => regions.Any(y => y.Guid == x.Region)).ToList();
            return retval;
        }


    }
    public class AtlasDTO
    {
        public List<Item> Items { get; set; } = new();
        public class Item
        {
            public GuidNom? Country { get; set; }
            public GuidNom? Zona { get; set; }
            public GuidNom? Location { get; set; }
            public GuidNom? Contact { get; set; }

        }

        public class DisplayItem
        {
            public Guid Guid { get; set; }
            public string? Nom { get; set; }
            public Cods Cod { get; set; }
            public Actions Action { get; set; } = Actions.Drilldown;
            public string? Value { get; set; }
            public object? Tag { get; set; }
            public enum Cods
            {
                Country,
                Zona,
                Location,
                Contact,
                MenuItem,
                ContactDetails,
                Telefon,
                Email,
                Cta
            }

            public enum Actions
            {
                Drilldown,
                Fitxa,
                Telefons,
                Emails,
                Ctas
            }
        }

        public List<AtlasDTO.DisplayItem> FilteredContacts(string searchTerm)
        {
            return Items.Select(x => x.Contact)
                .Where(x => x != null && x.Nom != null && x.Nom.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .Select(x => new DisplayItem {
                    Guid = x!.Guid,
                    Nom = x.Nom ?? "",
                    Cod = DisplayItem.Cods.Contact
                })
                .ToList();
        }

        public List<DisplayItem> Countries()
        {
            return Items.Select(x => x.Country)
                .GroupBy(x => x.Guid)
                .Select(x => x.First())
                .OrderBy(x => x.Nom)
                .Select(x => new DisplayItem
                {
                    Guid = x.Guid,
                    Nom = x.Nom ?? "",
                    Cod = DisplayItem.Cods.Country
                })
                .ToList();
        }

        public List<DisplayItem> Zonas(Guid parent)
        {
            return Items
                .Where(x => x.Country.Guid == parent)
                .Select(x => x.Zona)
                .GroupBy(x => x.Guid)
                .Select(x => x.First())
                .OrderBy(x => x.Nom)
                .Select(x => new DisplayItem
                {
                    Guid = x.Guid,
                    Nom = x.Nom ?? "",
                    Cod = DisplayItem.Cods.Zona
                })
                .ToList();
        }

        public List<DisplayItem> Locations(Guid parent)
        {
            return Items
                .Where(x => x.Zona != null && x.Zona.Guid == parent)
                .Select(x => x.Location)
                .GroupBy(x => x.Guid)
                .Select(x => x.First())
                .OrderBy(x => x.Nom)
                .Select(x => new DisplayItem
                {
                    Guid = x.Guid,
                    Nom = x.Nom ?? "",
                    Cod = DisplayItem.Cods.Location
                })
                .ToList();
        }
        public List<DisplayItem> Contacts(Guid parent)
        {
            var retval = Items
                .Where(x => x.Location != null && x.Location.Guid == parent)
                .Select(x => x.Contact)
                .GroupBy(x => x.Guid)
                .Select(x => x.First())
                .OrderBy(x => x.Nom)
                .Select(x => new DisplayItem
                {
                    Guid = x.Guid,
                    Nom = x.Nom ?? "",
                    Cod = DisplayItem.Cods.Contact
                })
                .ToList();
            return retval;
        }

        public List<DisplayItem> ParentItems(DisplayItem item)
        {
            var retval = new List<DisplayItem>();
            var src = Items.First(x => x.Contact != null && x.Contact.Guid == item.Guid);
            retval.Add(new DisplayItem
            {
                Guid = src.Country.Guid,
                Nom = src.Country.Nom ?? "",
                Cod = DisplayItem.Cods.Country
            });
            retval.Add(new DisplayItem
            {
                Guid = src.Zona.Guid,
                Nom = src.Zona.Nom ?? "",
                Cod = DisplayItem.Cods.Zona
            });
            retval.Add(new DisplayItem
            {
                Guid = src.Location.Guid,
                Nom = src.Location.Nom ?? "",
                Cod = DisplayItem.Cods.Location
            });
            return retval;
        }

        public List<DisplayItem> MenuItems(Guid parent, LangDTO lang)
        {
            var retval = new List<DisplayItem>();
            retval.Add(new DisplayItem
            {
                Guid = parent,
                Nom = lang.Tradueix("Ficha","Fitxa","Details"),
                Cod = DisplayItem.Cods.MenuItem,
                Action = DisplayItem.Actions.Fitxa
            });
            retval.Add(new DisplayItem
            {
                Guid = parent,
                Nom = lang.Tradueix("Teléfonos","Telefons","Phone numbers"),
                Cod = DisplayItem.Cods.MenuItem,
                Action = DisplayItem.Actions.Telefons
            });
            retval.Add(new DisplayItem
            {
                Guid = parent,
                Nom = "Emails",
                Cod = DisplayItem.Cods.MenuItem,
                Action = DisplayItem.Actions.Emails
            });
            retval.Add(new DisplayItem
            {
                Guid = parent,
                Nom = lang.Tradueix("Cuentas","Comptes","Accounts"),
                Cod = DisplayItem.Cods.MenuItem,
                Action = DisplayItem.Actions.Ctas
            });
            return retval;
         }
    }
}
