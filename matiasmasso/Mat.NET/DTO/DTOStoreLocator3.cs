using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOStoreLocator3
    {
        public OfflineClass Offline { get; set; }
        public OnlineClass Online { get; set; }

        public DTOStoreLocator3() : base()
        {
            this.Offline = new OfflineClass();
            this.Online = new OnlineClass();
        }

        public void setDefaults(DTOLang oLang)
        {
            List<Country> countries = this.Offline.Countries.GetRange(0, this.Offline.Countries.Count);

            if (oLang.id == DTOLang.Ids.POR)
                countries = countries.Where(x => x.Guid.Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Portugal).Guid)).ToList();
            else
                countries.RemoveAll(x => x.Guid.Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Portugal).Guid));

            var distributors = countries.SelectMany(a => a.Zonas).SelectMany(b => b.Locations).SelectMany(c => c.Distributors).ToList();
            if (distributors.Count > 0)
            {
                this.Offline.DefaultDistributor = distributors.OrderByDescending(x => x.Sales).First();
                this.Offline.DefaultLocation = countries.SelectMany(a => a.Zonas).SelectMany(b => b.Locations).Where(c => c.Distributors.Any(d => d.Guid == this.Offline.DefaultDistributor.Guid)).First();
                this.Offline.DefaultZona = countries.SelectMany(a => a.Zonas).Where(c => c.Locations.Any(d => d.Guid == this.Offline.DefaultLocation.Guid)).First();
                this.Offline.DefaultCountry = countries.Where(c => c.Zonas.Any(d => d.Guid == this.Offline.DefaultZona.Guid)).First();
            }

            foreach (Country country in this.Offline.Countries)
            {
                country.SetDefaults();
                foreach (Zona zona in country.Zonas)
                {
                    zona.SetDefaults();
                }
            }

        }

        public class OfflineClass
        {
            public List<Country> Countries { get; set; }

            public OfflineClass() : base()
            {
                this.Countries = new List<Country>();
            }

            public Distributor DefaultDistributor { get; set; }
            public Location DefaultLocation { get; set; }
            public Zona DefaultZona { get; set; }
            public Country DefaultCountry { get; set; }

            //public string Serialized()
            //{
            //    var serializer = new JavaScriptSerializer();
            //    string retval = serializer.Serialize(this.Countries);
            //    return retval;
            //}

            //public string SerializedForRaffles()
            //{
            //    var serializer = new JavaScriptSerializer();
            //    string retval = serializer.Serialize(this);
            //    return retval;
            //}



        }

        public class OnlineClass
        {
            public List<LandingPage> LandingPages { get; set; }
            public OnlineClass() : base()
            {
                this.LandingPages = new List<LandingPage>();
            }

        }

        public class Country
        {
            public Guid Guid { get; set; }
            public string Nom { get; set; }
            public List<Zona> Zonas { get; set; }
            public Distributor DefaultDistributor { get; set; }
            public Location DefaultLocation { get; set; }
            public Zona DefaultZona { get; set; }


            public Country() : base()
            {
                this.Zonas = new List<Zona>();
            }

            public Country(Guid guid) : base()
            {
                this.Guid = guid;
                this.Zonas = new List<Zona>();
            }
            public Decimal Sales()
            {
                return this.Zonas.Sum(x => x.Sales());
            }
            public void SetDefaults()
            {
                var distributors = this.Zonas.SelectMany(b => b.Locations).SelectMany(c => c.Distributors).ToList();
                if (distributors.Count > 0)
                {
                    this.DefaultDistributor = distributors.OrderByDescending(x => x.Sales).First();
                    this.DefaultLocation = this.Zonas.SelectMany(b => b.Locations).Where(c => c.Distributors.Any(d => d.Guid == this.DefaultDistributor.Guid)).First();
                    this.DefaultZona = this.Zonas.Where(c => c.Locations.Any(d => d.Guid == this.DefaultLocation.Guid)).First();
                }

            }
        }

        public class Zona
        {
            public Guid Guid { get; set; }
            public string Nom { get; set; }

            public List<Location> Locations { get; set; }

            public Distributor DefaultDistributor { get; set; }
            public Location DefaultLocation { get; set; }

            public Zona() : base()
            {
                this.Locations = new List<Location>();
            }

            public Zona(Guid guid) : base()
            {
                this.Guid = guid;
                this.Locations = new List<Location>();
            }

            public bool IsExcluded()
            {
                List<Guid> excludedGuids = new List<Guid>();
                excludedGuids.Add(DTOAreaProvincia.Wellknown(DTOAreaProvincia.Wellknowns.ceuta).Guid);
                excludedGuids.Add(DTOAreaProvincia.Wellknown(DTOAreaProvincia.Wellknowns.melilla).Guid);
                excludedGuids.Add(DTOAreaProvincia.Wellknown(DTOAreaProvincia.Wellknowns.tenerife).Guid);
                excludedGuids.Add(DTOAreaProvincia.Wellknown(DTOAreaProvincia.Wellknowns.laspalmas).Guid);
                excludedGuids.Add(DTOZona.Wellknown(DTOZona.Wellknowns.Andorra).Guid);
                excludedGuids.Add(DTOZona.Wellknown(DTOZona.Wellknowns.Madeira).Guid);
                excludedGuids.Add(DTOZona.Wellknown(DTOZona.Wellknowns.Azores).Guid);
                bool retval = excludedGuids.Any(x => x.Equals(this.Guid));
                return retval;
            }


            public static Zona Factory(Guid guid, String nom)
            {
                Zona retval = new Zona(guid);
                retval.Nom = nom;
                return retval;
            }

            public Decimal Sales()
            {
                return this.Locations.Sum(x => x.Sales());
            }
            public void SetDefaults()
            {
                var distributors = this.Locations.SelectMany(c => c.Distributors).ToList();
                if (distributors.Count > 0)
                {
                    this.DefaultDistributor = distributors.OrderByDescending(x => x.Sales).First();
                    this.DefaultLocation = this.Locations.Where(c => c.Distributors.Any(d => d.Guid == this.DefaultDistributor.Guid)).First();
                }
            }
        }
        public class Location
        {
            public Guid Guid { get; set; }
            public string Nom { get; set; }

            public List<Distributor> Distributors { get; set; }
            public Location() : base()
            {
                this.Distributors = new List<Distributor>();
            }

            public Location(Guid guid) : base()
            {
                this.Guid = guid;
                this.Distributors = new List<Distributor>();
            }

            public Decimal Sales()
            {
                return this.Distributors.Sum(x => x.Sales);
            }
        }
        public class Distributor
        {
            public Guid Guid { get; set; }
            public string Nom { get; set; }

            public string Adr { get; set; }
            public string Tel { get; set; }
            public Decimal Sales { get; set; }
            public Decimal SalesHistoric { get; set; }
            public Decimal SalesCcx { get; set; }
            public DateTime LastFch { get; set; }
            public Statuses Status { get; set; }

            public enum Statuses
            {
                Success,
                Impagat,
                Blocked,
                Obsolet
            }


            public Distributor() : base()
            {

            }

            public Distributor(Guid guid) : base()
            {
                this.Guid = guid;
            }
        }

        public class LandingPage
        {
            public Guid Guid { get; set; }
            public string Nom { get; set; }
            public string Url { get; set; }

            public int CustomerStock { get; set; }
            public int MgzStock { get; set; }

            public LandingPage() : base()
            {

            }

            public LandingPage(Guid guid) : base()
            {
                this.Guid = guid;
            }



        }
    }
}
