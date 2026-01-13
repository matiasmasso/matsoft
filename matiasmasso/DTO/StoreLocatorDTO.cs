using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DTO
{
    public class StoreLocatorDTO
    {
        public OfflineClass Offline { get; set; } = new();
        public OnlineClass Online { get; set; } = new();

        public class OfflineClass
        {
            public int SelectedCountry { get; set; } = 0;
            public List<Country> Countries { get; set; } = new();
        }

        public class OnlineClass
        {
            public List<LandingPage> LandingPages { get; set; } = new();
        }

        public class LandingPage
        {
            public Guid Guid { get; set; }
            public string Nom { get; set; }
            public string Web { get; set; }
            public string Url { get; set; }
            public bool InStock { get; set; }

        }

        public class Country
        {
            public Guid Guid { get; set; }
            public string Nom { get; set; }
            public List<Zona> Zonas { get; set; } = new();
            public int SelectedZona { get; set; } = 0;
            public override string ToString() => Nom;
        }

        public class Zona
        {
            public string Nom { get; set; }
            public List<Location> Locations { get; set; } = new();
            public int SelectedLocation { get; set; } = 0;
            public override string ToString() => Nom;
        }
        public class Location
        {
            public string Nom { get; set; }
            public List<Distributor> Distributors { get; set; } = new();
            public int SelectedDistributor { get; set; } = 0;
            public override string ToString() => Nom;
        }
        public class Distributor
        {
            public Guid Guid{ get; set; }
            public string Nom { get; set; }
            public string Address { get; set; }
            public string Tel { get; set; }

            public override string ToString() => Nom;
        }
        public class FlatDistributor:Distributor
        {
            public Guid CountryGuid { get; set; }
            public string CountryEsp { get; set; }
            public string CountryCat { get; set; }
            public string CountryEng { get; set; }
            public string CountryPor { get; set; }
            public string Zona { get; set; }
            public string Location { get; set; }
            public decimal Score { get; set; }
            public int Priority { get; set; }

        }
    }
}
